using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	[Export] public float Speed = 200.0f;
	//public const float JumpVelocity = 00.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			//velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			//velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

// direction sẽ tự động có cả X và Y, ta chỉ cần nhân với Speed
if (direction != Vector2.Zero)
{
	velocity = direction * Speed; 
}
else
{
	// Giảm tốc độ cả 2 trục về 0 khi thả tay
	velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
	velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
}

		Velocity = velocity;
		MoveAndSlide();
	}
}
