extends Control

var scene_preload = preload("res://src/ui/cell/cell.tscn")

# Called when the node enters the scene tree for the first time.
func _ready():
	$TabBar.add_tab("Hello world")
	$TabBar.add_tab("Not hello world")
	$TabBar.set_tab_close_display_policy(2)

func _on_tab_bar_tab_close_pressed(tab):
	print(tab)
