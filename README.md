# Lang learning notebook
Project that (supposedly) will help me learn human languages.
For now it only helps me learning C# though (not really a human language).

## Why?
Somehow I found myself learning multiple languages at the same time,
and all the flashcard apps I used felt kinda clunky, also that one owl app
didn't feel like it's useful enough. So I've decided to make app in which I can:
* Make notes on grammar
* Translate texts
* Practice flashcards and maybe other excercises
* Quickly add new flashcards
* Use AI tools for requests like "explain me this grammar" or
  "give me flashcards deck for computer components for this pair of languages"
* Use Lua scripting for... stuff. I don't know, maybe flashcard filtering,
  or scripting new exercises.

Also I want it to be cross-platform.

## Why Godot?
Good enough for UI at this stage, cross-platform, and also I somewhat know it.
Don't really want to turn this into something like "learning Flutter and losing
interest on a project" challenge.

## Why .NET?
I don't really know .NET and don't really enjoy it much. However, it
integrates in Godot without pain, it has lots of libs, and if I want to move
from Godot to a real cross-platform UI framework, there's Avalonia and others.
Application core is separated from Godot project, so should be fine.

## Notes on AI usage in code
Tools like Copilot or GPT-4 were used to generate some boilerplate (like EF Core
code), both out of curiosity and to not have to write that boilerplate.
As I'm not sure if this is going to be a legal issue in the future,
and what licenses are applicable to generated code,
I'm marking all generated files by comment, so they can be rewritten if needed.
