using Godot;

public static class Global
{
	// Lưu trữ tọa độ X, Y mà nhân vật cần dịch chuyển tới
	public static Vector2 TargetSpawnPosition = Vector2.Zero;
	
	// Cờ báo hiệu xem có cần dùng tọa độ này không (để lúc mới mở game không bị lỗi)
	public static bool ShouldUseSpawnPosition = false;
}
