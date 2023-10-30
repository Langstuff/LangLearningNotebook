extends ScrollContainer


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass


func _on_results_rich_text_label_resized():
	var label_size = $ResultsRichTextLabel.size
	#print("size ", label_size)
	var new_min_y = min(label_size.y, 200)
	#set_size(Vector2(size.x, min(label_size.y, 400)))
	set("custom_minimum_size", Vector2(size.x, new_min_y))
	queue_redraw()
