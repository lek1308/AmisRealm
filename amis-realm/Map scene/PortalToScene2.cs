using Godot;
using System;

public partial class PortalToScene2 : Area2D
{
	// Tạo ô chọn file Scene ở ngoài Inspector
	[Export(PropertyHint.File, "*.tscn")] 
	public string NextScenePath { get; set; }

	public override void _Ready()
	{
		// Lắng nghe sự kiện khi có vật thể chạm vào cổng
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		// Kiểm tra xem thứ dẫm vào có đúng là Player không (đề phòng chó mèo dẫm nhầm)
		if (body.Name == "Player" || body.Name == "player") 
		{
			if (!string.IsNullOrEmpty(NextScenePath))
			{
				GetTree().ChangeSceneToFile(NextScenePath);
			}
			else
			{
				GD.PrintErr("LỖI: Bạn chưa chọn map để chuyển tới ở bảng Inspector!");
			}
		}
	}
}
