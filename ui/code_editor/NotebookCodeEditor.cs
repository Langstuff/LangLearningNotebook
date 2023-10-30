using Godot;
using System.Collections.Generic;

public partial class NotebookCodeEditor : CodeEdit
{
	public enum LANGUAGE_MODE
	{
		TEXT,
		BBCODE,
		LUA
	}

	public LANGUAGE_MODE language_mode = LANGUAGE_MODE.TEXT;

	public override void _Ready()
	{
		// Replace with function body.
	}

	public override void _Process(double delta)
	{
		// Replace with function body.
	}

	public string GetCode()
	{
		List<string> lines = new List<string>();
		for (int i = 1; i < GetLineCount(); i++)
		{
			lines.Add(GetLine(i));
		}
		return string.Join("\n", lines);
	}

	public void _OnLinesEdited(int from_line, int to_line)
	{
		bool edited_first_line = Mathf.Min(from_line, to_line) == 0;
		if (edited_first_line)
		{
			_UpdateLanguageMode();
		}
	}

	private void _UpdateEditorSettings()
	{
		if (language_mode == LANGUAGE_MODE.TEXT)
		{
			GuttersDrawLineNumbers = false;
			//DelimiterStrings = new string[] { };
			//SyntaxHighlighter = SyntaxHighlighters.Text;
		}
		else if (language_mode == LANGUAGE_MODE.BBCODE)
		{
			GuttersDrawLineNumbers = false;
			AutoBraceCompletionEnabled = true;
			AutoBraceCompletionHighlightMatching = true;
			//SyntaxHighlighter = SyntaxHighlighters.BBcode;
		}
		else if (language_mode == LANGUAGE_MODE.LUA)
		{
			GuttersDrawLineNumbers = true;
			AutoBraceCompletionEnabled = true;
			AutoBraceCompletionHighlightMatching = true;
			//SyntaxHighlighter = SyntaxHighlighters.Lua;
		}
	}

	private void _UpdateLanguageMode()
	{
		string language = GetLine(0).TrimPrefix("#");
		if (language == "bbcode")
		{
			language_mode = LANGUAGE_MODE.BBCODE;
		}
		else if (language == "lua")
		{
			language_mode = LANGUAGE_MODE.LUA;
		}
		else
		{
			language_mode = LANGUAGE_MODE.TEXT;
		}
		_UpdateEditorSettings();
	}
}
