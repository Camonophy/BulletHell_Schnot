using Sandbox;

public sealed class BulletComponent : Component
{
	public int BulletSpeed { get; set; } = 10; 
	
	protected override void OnUpdate()
	{
		var pos = WorldPosition;
		pos.y += -2f * BulletSpeed;
		WorldPosition = pos;
	}
}
