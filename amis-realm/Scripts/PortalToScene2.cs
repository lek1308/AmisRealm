using Godot;
using System;

public partial class PortalToScene2 : Area2D
{
	// Ô chọn Map đích
	[Export(PropertyHint.File, "*.tscn")] 
	public string NextScenePath { get; set; }

	// Ô nhập tọa độ dịch chuyển mà bạn đang tìm kiếm!
	[Export] 
	public Vector2 SpawnLocation { get; set; }

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.Name == "Player" || body.Name == "player") 
		{
			if (!string.IsNullOrEmpty(NextScenePath))
			{
				// Ghi nhớ tọa độ vào Global trước khi bay sang Map khác
				Global.TargetSpawnPosition = SpawnLocation;
				Global.ShouldUseSpawnPosition = true;
				
				GetTree().ChangeSceneToFile(NextScenePath);
			}
		}
	}
}
