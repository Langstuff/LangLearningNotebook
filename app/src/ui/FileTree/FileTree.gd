extends VBoxContainer

var FilenameEditScene = preload("res://src/ui/FilenameEdit/FilenameEdit.tscn")

var paths: Array[String] = []

func add_file_path(path):
	var ed = FilenameEditScene.instantiate()
	ed.text = path
	$VBox.add_child(ed)

func get_filter():
	return $FilterInput.text

func show_paths(paths: Array[String], filter=null):
	paths.sort()
	self.paths = paths
	var paths_to_show = paths.duplicate()
	if !filter:
		filter = get_filter()
	if filter and filter != "":
		paths_to_show = paths_to_show.filter(func(s: String): return s.find(filter) >= 0)
	for child in $VBox.get_children():
		child.queue_free()
	for path in paths_to_show:
		add_file_path(path)

func _ready():
	show_paths([
		"hello/",
		"hello/world 1",
		"hello/world 2",
		"hello/world 3",
	])


func _on_filter_input_text_changed(new_text):
	var filter = new_text
	show_paths(self.paths, filter)
