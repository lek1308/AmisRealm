using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 250.0f;
	private Node2D _aim;
	private Area2D _interactArea;
	
	private AnimatedSprite2D _animSprite; 

	// Lưu lại hướng nhân vật đang nhìn (mặc định nhìn xuống)
	private string _facing = "down"; 

	public override void _Ready()		
	{
		// 1. Lấy các Node cần thiết TRƯỚC
		Input.MouseMode = Input.MouseModeEnum.Visible;
		_aim = GetNode<Node2D>("Aim");
		_interactArea = GetNode<Area2D>("Aim/InteractArea");
		_animSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		// 2. Sau đó mới kiểm tra lệnh dịch chuyển từ cổng
		if (Global.ShouldUseSpawnPosition)
		{
			// Ép nhân vật đứng đúng chỗ đó
			GlobalPosition = Global.TargetSpawnPosition;
			
			// Tắt cờ đi để tránh bị dịch chuyển nhầm lần sau
			Global.ShouldUseSpawnPosition = false; 
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");
		
		if (direction != Vector2.Zero)
		{
			Velocity = direction * Speed;
			
			// Xác định hướng đang nhìn dựa trên nút bấm
			if (direction.X < 0) _facing = "left";
			else if (direction.X > 0) _facing = "right";
			else if (direction.Y < 0) _facing = "up";
			else if (direction.Y > 0) _facing = "down";

			// Đã đổi thành "move_" để khớp với tên trong bảng SpriteFrames
			_animSprite.Play("move_" + _facing);
		}
		else
		{
			Velocity = Vector2.Zero;
			
			_animSprite.Play("idle_" + _facing);
		}
		
		MoveAndSlide();

		Vector2 mousePos = GetGlobalMousePosition();
		_aim.LookAt(mousePos);

		if (Input.IsActionJustPressed("interact")) 
		{
			InteractWithObjects();
		}
	}

	private void InteractWithObjects()
	{
		var overlappingBodies = _interactArea.GetOverlappingBodies();
		
		foreach (var body in overlappingBodies)
		{
			if (body.HasMethod("TakeDamage"))
			{
				body.Call("TakeDamage", 1);
				GD.Print("Đã đập trúng: " + body.Name);
				break; 
			}
		}
	}
}
