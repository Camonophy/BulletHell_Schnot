using Sandbox;
using Sandbox.Movement;

public sealed class EnemyMovement : Component
{
	[Property] public PlayerController Controller { get; set; }
	[Property] public GameObject Target { get; set; } // der Spieler

	[Property] public SkinnedModelRenderer Renderer { get; set; }

	[Property] public float MoveSpeed { get; set; } = 100f;

	// Wie lange sich der Gegner jeweils bewegt bzw. stehen bleibt (in Sekunden)
	[Property] public float MoveDuration { get; set; } = 2f;
	[Property] public float IdleDuration { get; set; } = 1.5f;

	private bool isMoving = true;
	private TimeSince timeSinceStateChange = 0;

	protected override void OnStart()
	{
		Renderer?.PlaybackRate = MoveSpeed / 20;
	}

	protected override void OnFixedUpdate()
	{
		if ( Target == null || Controller == null )
			return;

		// Prüfen, ob der aktuelle Zustand (Bewegen/Stehen) abgelaufen ist
		float currentDuration = isMoving ? MoveDuration : IdleDuration;

		if ( timeSinceStateChange > currentDuration )
		{
			isMoving = !isMoving;
			timeSinceStateChange = 0;

			Renderer?.Set( "Walk", isMoving );
		}

		if ( isMoving )
		{
			var direction = ( Target.WorldPosition - WorldPosition ).WithZ( 0 ).Normal;
			Controller.WishVelocity = direction * MoveSpeed;

			if ( direction.Length > 0.01f )
			{
				WorldRotation = Rotation.LookAt( direction, Vector3.Up );
			}
		}
		else
		{
			Controller.WishVelocity = Vector3.Zero;
		}
	}
}