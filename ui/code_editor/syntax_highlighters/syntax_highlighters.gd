extends Node
# nothing but an experiment

var Lua = CodeHighlighter.new()
var Text = CodeHighlighter.new()
var BBcode = CodeHighlighter.new()

func _ready():
	Lua.keyword_colors = {
		"if": Color.GREEN,
		"else": Color.GREEN,
		"end": Color.GREEN,
	}

	Text.keyword_colors = {}

	BBcode.keyword_colors = {}
