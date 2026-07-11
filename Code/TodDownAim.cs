using Sandbox;

public sealed class TodDownAim : Component
{

	private Plane plane;
	protected override void OnStart()
	{
		Mouse.Visible = true;
		plane = new Plane( WorldPosition, Vector3.Up );
	}

	protected override void OnUpdate()
	{
		// Log.Info( Mouse.Position ); <-- Zum Debugging der Maus Position

		var ray = Scene.Camera.ScreenPixelToRay( Mouse.Position );

		var hit = plane.Trace( ray, true );

		if ( hit.HasValue )
		{
			var direction = hit.Value - WorldPosition;

			// Höhe rausrechnen, damit der Player nicht "nickt"
			direction = direction.WithZ( 0 );
			direction.y = -direction.y; // <-- irgendwie sind die Achsen invertiert...

			if ( direction.Length > 0.01f )
			{
				WorldRotation = Rotation.LookAt( direction, Vector3.Up );
			}
		}
	}
}
