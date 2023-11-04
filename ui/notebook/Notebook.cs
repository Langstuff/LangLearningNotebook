using Godot;

using NotebookLua;

public partial class Notebook : ScrollContainer
{
	private PackedScene cellScene = GD.Load<PackedScene>("res://ui/cell/cell.tscn");
	public LuaNotebook luaNotebook;

	public override void _Ready()
	{
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
