using Sandbox;

public sealed class CameraRelativeMovement : Component
{
	[Property] public PlayerController PlayerController { get; set; }
	[Property] public GameObject Camera { get; set; }

	protected override void OnFixedUpdate()
	{
		var input = Input.AnalogMove;

		// Kamera-Rotation, aber nur die Y-Achse (Blickwinkel von oben),
		// damit wir nicht versehentlich "nach unten in den Boden" gucken
		var camRotation = Rotation.FromYaw( Camera.WorldRotation.Yaw() );

		var wishDirection = camRotation * input;

		PlayerController.WishVelocity = wishDirection.Normal * PlayerController.RunSpeed;
	}
}
