using Sandbox;

public sealed class Bullet : Component
{
	[Property] public float Speed { get; set; } = 800f;
	[Property] public float Lifetime { get; set; } = 3f;

	private TimeSince timeSinceSpawn = 0;

	protected override void OnFixedUpdate()
	{
		WorldPosition += WorldRotation.Forward * Speed * Time.Delta;

		if ( timeSinceSpawn > Lifetime )
		{
			GameObject.Destroy();
		}
	}
}
