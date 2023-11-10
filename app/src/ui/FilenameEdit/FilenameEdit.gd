extends Control

var text: String:
	set(value):
		$HBoxContainer/Control/Input.text = value
		$HBoxContainer/Control/Label.text = value

var is_editable: bool:
	set(editable):
		if editable:
			$HBoxContainer/Control/Input.show()
			$HBoxContainer/Control/Label.hide()
		else:
			$HBoxContainer/Control/Input.hide()
			$HBoxContainer/Control/Label.show()

# Called when the node enters the scene tree for the first time.
func _ready():
	is_editable = false


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func _set_input_caret_position_from_event(event: InputEventMouseButton):
	var new_event = event.duplicate()
	new_event.double_click = false
	new_event.position += $HBoxContainer/Control/Input.global_position
	Input.parse_input_event(new_event)

func _on_label_gui_input(event: InputEvent):
	if event is InputEventMouseButton and event.double_click:
		self.is_editable = true
		$HBoxContainer/Control/Input.grab_focus()
		_set_input_caret_position_from_event(event)


func _on_input_focus_exited():
	self.is_editable = false
