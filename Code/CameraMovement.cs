using Sandbox;

public sealed class CameraMovement : Component
{
    [Property]
    public GameObject Target { get; set; }

    [Property]
    public float Height = 600;

    [Property]
    public float FollowSpeed = 8f;

    // Position der Kamera in Hoch-Runter Richtung
    [Property]
    public float Offset = 90;

    // Wie stark der Mauszeiger die Kamera "zieht" (0 = kein Effekt, 1 = volle Distanz)
	[Property]
	public float MouseOffsetStrength = 0.3f;

	[Property]
	public float MaxMouseOffset = 300f;

    [Property]
	public float MinMouseOffset = 50f;

    protected override void OnUpdate()
    {
        if ( Target == null )
            return;
        
        Vector3 mouseOffset = Vector3.Zero;

        //Trick um die Verzerrung durch die Kameraperspektive auszugleichen
		var ray = Scene.Camera.ScreenPixelToRay(Mouse.Position);
		var plane = new Plane(Target.WorldPosition, Vector3.Up);
		var hit = plane.Trace(ray, true);

        if (hit.HasValue)
		{
			var diff = (hit.Value - Target.WorldPosition).WithZ(0);
        
			// Auf die maximale/minimale begrenzen, bevor wir die Stärke anwenden
			if (diff.Length > MaxMouseOffset)
			{
				diff = diff.Normal * MaxMouseOffset;
        
			} else if (diff.Length < MinMouseOffset)
            {
                diff = diff.Normal;
            }
        
			mouseOffset = diff * MouseOffsetStrength;
		}

        Vector3 targetPosition =
			Target.WorldPosition + mouseOffset + Vector3.Up*Height - Vector3.Left*Offset;

        WorldPosition = Vector3.Lerp(
            WorldPosition,
            targetPosition,
            Time.Delta * FollowSpeed
        );
    }
}
