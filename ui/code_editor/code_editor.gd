extends CodeEdit
class_name NotebookCodeEditor

enum LANGUAGE_MODE {
	TEXT,
	BBCODE,
	LUA
}

var language_mode = LANGUAGE_MODE.TEXT

func _update_editor_settings():
	if language_mode == LANGUAGE_MODE.TEXT:
		self.gutters_draw_line_numbers = false
		self.delimiter_strings = []
		self.syntax_highlighter = SyntaxHighlighters.Text
	elif language_mode == LANGUAGE_MODE.BBCODE:
		self.gutters_draw_line_numbers = false
		self.auto_brace_completion_enabled = true
		self.auto_brace_completion_highlight_matching = true
		self.syntax_highlighter = SyntaxHighlighters.BBcode
	elif language_mode == LANGUAGE_MODE.LUA:
		self.gutters_draw_line_numbers = true
		self.auto_brace_completion_enabled = true
		self.auto_brace_completion_highlight_matching = true
		self.syntax_highlighter = SyntaxHighlighters.Lua


func _update_language_mode():
	var language = self.get_line(0).strip_edges(true, true).trim_prefix("#")
	if language == "bbcode":
		language_mode = LANGUAGE_MODE.BBCODE
	elif language == "lua":
		language_mode = LANGUAGE_MODE.LUA
	else:
		language_mode = LANGUAGE_MODE.TEXT
	print("language: ", language, " ", language_mode)
	_update_editor_settings()


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func GetCode():
	var lines = []
	for i in range(1, get_line_count() - 1):
		lines.push_back(get_line(i))
	return "\n".join(lines)


func _on_lines_edited_from(from_line, to_line):
	print("lines: ", from_line, " ", to_line)
	var edited_first_line = min(from_line, to_line) == 0
	print("min line: ", edited_first_line)
	if edited_first_line:
		_update_language_mode()

