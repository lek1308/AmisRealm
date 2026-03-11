using Godot;
using System;

public partial class BerryBush : StaticBody2D
{
	private AnimatedSprite2D _bushSprite;
	private bool _isPlayerNear = false;
	private bool _hasBerries = true; 

	public override void _Ready()
	{
		_bushSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_bushSprite.Play("idle_berries");

		Area2D harvestZone = GetNode<Area2D>("HarvestZone");
		harvestZone.BodyEntered += OnBodyEntered;
		harvestZone.BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.Name == "player") 
		{
			_isPlayerNear = true;
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if (body.Name == "player")
		{
			_isPlayerNear = false;
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (_isPlayerNear && _hasBerries && @event.IsActionPressed("ui_accept"))
		{
			Harvest();
		}
	}

	// Thêm chữ 'async' vào trước void để báo cho Godot biết hàm này có sử dụng đồng hồ chờ
	private async void Harvest()
	{
		_hasBerries = false;
		GD.Print("Đã thu hoạch quả mọng! Đợi 60 giây để mọc lại...");
		
		// Cây mất quả và đung đưa
		_bushSprite.Play("idle_empty"); 

		// Lệnh này tạo ra một đồng hồ đếm ngược 60 giây ngầm.
		// Chữ 'await' sẽ giữ cho các dòng code bên dưới không chạy cho đến khi đếm xong.
		await ToSignal(GetTree().CreateTimer(60.0), SceneTreeTimer.SignalName.Timeout);

		// --- MỌI THỨ DƯỚI DÒNG NÀY SẼ CHẠY SAU KHI ĐÚNG 60 GIÂY TRÔI QUA ---
		Regrow();
	}

	// Hàm phụ để cây mọc lại
	private void Regrow()
	{
		_hasBerries = true;
		GD.Print("Quả mọng đã mọc lại!");
		_bushSprite.Play("idle_berries"); // Trở lại animation đung đưa có quả
	}
}
