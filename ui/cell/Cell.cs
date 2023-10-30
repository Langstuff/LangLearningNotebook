using Godot;
using System;
using LuaNotebookScripting;
using System.Collections.Concurrent;
using System.Threading;

#nullable enable

public partial class Cell : VBoxContainer
{
	string results = "";
	static ConcurrentQueue<object> resultsQueue = new ConcurrentQueue<object>();
	public LuaNotebook luaNotebook;
	// private LuaNotebookScripting.LuaState lua;
	[Signal]
	public delegate void CellExecutedEventHandler();
	[Signal]
	public delegate void ThreadFinishedEventHandler();

	void CellPrint(params object[] args) {
		NLua.LuaFunction tostring;
		try {
			tostring = luaNotebook.lua.GetFunction("tostring");
			for (var i = 0; i < args.Length; i++) {
				object arg = args[i];
				try {
					var callres = tostring.Call(arg);
					results += (string)callres[0];
					if (i == args.Length - 1) {
						results += '\n';
					} else {
						results += ' ';
					}
				} catch (Exception e) {
					GD.Print(e.Message);
				}
			}
		} catch (Exception e) {
			GD.Print(e.Message);
		}
	}

	void RunLuaCode(object? codeObj)
	{
		if (codeObj is null)
		{
			return;
		}
		// Cast the object back to a string
		string code = (string)codeObj;

		try
		{

			// Lock the Lua state to ensure thread safety
			lock (luaNotebook)
			{
				results = "";
				// Execute the Lua code
				try {
					luaNotebook.Execute(code);
					resultsQueue.Enqueue(results);
				} catch (Exception e) {
					resultsQueue.Enqueue(e.Message);
				}

			}
		}
		catch (Exception ex)
		{
			GD.Print("An error occurred: " + ex.Message);
		}
		CallDeferred("emit_signal", "ThreadFinished");
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get the execute button and connect its pressed signal to emit the custom signal "CellExecuted"
		Button executeButton = GetNode<Button>("HBox/ExecuteButton");
		executeButton.Connect("pressed", Callable.From(this._OnExecuteButtonPressed));
		Connect("ThreadFinished", Callable.From(this._ThreadFinished));
		// notebook = new LuaNotebookScripting.Notebook();

		GetNode<NotebookCodeEditor>("NotebookCodeEditor").Text = @"#lua
for i = 1, 20 do
	print(i)
end
";
		if (luaNotebook is null) {
			GD.Print("initializing separate LuaNotebook");
			luaNotebook = new LuaNotebook();
		}
		// luaNotebook = GetParent<Notebook>().luaNotebook;
	}

	public void _ThreadFinished()
	{
		if (resultsQueue.TryDequeue(out object result))
		{
			GD.Print("Result: " + (string)result);
			GetNode<RichTextLabel>("OutputScrollControl/ResultsRichTextLabel").Text = (string)result;
		}
		else
		{
			GD.Print("No result?");
		}
		QueueRedraw();
		// GetNode<ScrollContainer>("OutputScrollControl").Set("custom_minimum_size", new Vector2(0, 500));

		CallDeferred("emit_signal", "CellExecuted");
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Replace with function body.
	}

	public void RunLuaChunk(string code)
	{
		//==========
		Thread luaThread = new Thread(new ParameterizedThreadStart(RunLuaCode));
		luaThread.Start(code);
	}

	public void _OnExecuteButtonPressed()
	{
		luaNotebook.lua["print"] = CellPrint;
		var code = GetNode<NotebookCodeEditor>("NotebookCodeEditor").GetCode();

		CallDeferred("RunLuaChunk", code);

	}
}
