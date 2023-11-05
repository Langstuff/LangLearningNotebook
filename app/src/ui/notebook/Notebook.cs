using Godot;

using NotebookLua;
using NotebookDatabase;
using Microsoft.EntityFrameworkCore;

public partial class Notebook : ScrollContainer
{
	private PackedScene cellScene = GD.Load<PackedScene>("res://src/ui/cell/cell.tscn");
	public LuaNotebook luaNotebook;

	// As autoload C# scripts don't work, put this here for now
	private void InitializeDatabase() {
		GD.Print("Initialize Database");
		using (var context = new NotebookContext())
		{
			context.Database.Migrate();
		}

		using (var context = new FlashcardContext())
		{
			context.Database.Migrate();
		}
	}

	public override void _Ready()
	{
		InitializeDatabase();
		luaNotebook = new LuaNotebook("test");
		AddCell();
	}

	public override void _Process(double delta)
	{
	}

	private void CellExecuted()
	{
		AddCell();
	}

	private void AddCell()
	{
		var cell = (Cell)cellScene.Instantiate();
		cell.luaNotebook = luaNotebook;
		cell.Connect("CellExecuted", Callable.From(CellExecuted));
		var cellBackground = new PanelContainer();
		cellBackground.AddChild(cell);
		GetNode("MarginContainer/CellContainer").AddChild(cellBackground);
	}
}
