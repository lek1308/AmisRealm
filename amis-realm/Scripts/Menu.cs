using Godot;
using System;

public partial class Menu : Node
{
	private string Scene1 = "res://Map scene/scene_1.tscn";
	
	private void _on_play_btn_pressed()
	{
		GetTree().ChangeSceneToFile(Scene1);
	}

	private void _on_quit_btn_pressed()
	{
		GetTree().Quit();
	}
}
