using Sandbox;

public sealed class TopDownAim : Component
{

	//Custom Enum, weil MouseVisibility noch buggy ist und nicht automatisch ein DropDown Menü im Editor erzeugt.
	public enum CursorVisibility
	{
		Auto,
		Hidden,
		Visible
	}
	[Property] public CursorVisibility VisibilityMode { get; set; } = CursorVisibility.Auto;
	private Plane plane;
	protected override void OnStart()
	{
		Mouse.Visibility = VisibilityMode switch
		{
			CursorVisibility.Hidden => MouseVisibility.Hidden,
			CursorVisibility.Visible => MouseVisibility.Visible,
			_ => MouseVisibility.Auto
		};
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
