extends ScrollContainer


func _on_results_rich_text_label_resized():
	var label_size = $ResultsRichTextLabel.size
	var new_min_y = min(label_size.y, 200)
	set("custom_minimum_size", Vector2(0, new_min_y))
