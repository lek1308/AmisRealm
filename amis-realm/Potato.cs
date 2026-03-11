using Godot;
using System;

public partial class Potato : Area2D
{
	// This will act as our sensor switch
	private bool _isPlayerNear = false;

	public override void _Ready()
	{
		// Connect the Area2D signals to our custom methods
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node2D body)
	{
		// Check if the thing that stepped on the potato is the player.
		// Looking at your screenshot, your player node is named "player" (lowercase).
		if (body.Name == "player") 
		{
			_isPlayerNear = true;
			GD.Print("Player is standing on the potato!");
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if (body.Name == "player")
		{
			_isPlayerNear = false;
			GD.Print("Player walked away.");
		}
	}

	public override void _Input(InputEvent @event)
	{
		// If the player is near AND they press the Spacebar (ui_accept)
		if (_isPlayerNear && @event.IsActionPressed("ui_accept"))
		{
			GD.Print("Potato Harvested!");
			
			// Remove the potato from the game world
			QueueFree(); 
		}
	}
}
