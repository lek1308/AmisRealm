using Godot;
using System;

public partial class MinimapCamera : Camera2D
{
	private CharacterBody2D _player;

	public override void _Ready()
	{
		// --- DÒNG PHÉP THUẬT MỚI THÊM ---
		// Bắt cái SubViewport này phải dùng chung "thế giới" với màn hình game chính
		SubViewport myViewport = GetViewport() as SubViewport;
		if (myViewport != null)
		{
			myViewport.World2D = GetWindow().World2D;
		}
		// --------------------------------

		// Tìm nhân vật Player
		_player = GetTree().CurrentScene.GetNodeOrNull<CharacterBody2D>("Player");
	}

	public override void _Process(double delta)
	{
		// Camera vệ tinh bay theo Player
		if (_player != null)
		{
			GlobalPosition = _player.GlobalPosition;
		}
	}
}
