using Sandbox;

public sealed class BulletShooter : Component
{
	[Property] public PrefabFile BulletPrefab { get; set; }

	[Property] public float SpawnDistance { get; set; } = 20f;

	protected override void OnUpdate()
	{
		if ( Input.Pressed( "attack1" ) )
		{
			Shoot();
		}
	}

	private void Shoot()
	{
		if ( BulletPrefab is null ) return;

		var ray = Scene.Camera.ScreenPixelToRay( Mouse.Position );
		var plane = new Plane( WorldPosition, Vector3.Up );
		var hit = plane.Trace( ray, true );

		if ( !hit.HasValue ) return;

		var direction = ( hit.Value - WorldPosition ).WithZ( 0 ).Normal;

		if ( direction.Length < 0.01f ) return;

		var rotation = Rotation.LookAt( direction, Vector3.Up );

		var bullet = GameObject.Clone( BulletPrefab );
		bullet.WorldPosition = WorldPosition + direction * SpawnDistance;
		bullet.WorldRotation = rotation;
	}
}
