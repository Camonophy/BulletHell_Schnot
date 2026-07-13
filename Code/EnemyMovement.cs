using Sandbox;
using Sandbox.Movement;

public sealed class EnemyMovement : Component
{
	[Property] public PlayerController Controller { get; set; }
	[Property] public GameObject Target { get; set; } // der Spieler

	[Property] public SkinnedModelRenderer Renderer { get; set; }

	[Property] public float MoveSpeed { get; set; }
	public float RotationSpeed { get; set; } = 2f;
	[Property] public float MoveDuration { get; set; } = 2f;
	[Property] public float IdleDuration { get; set; } = 1.5f;

	private bool isMoving = false;
	private TimeSince timeSinceStateChange = 0;

	protected override void OnStart()
	{
		Renderer?.Set( "Walk", isMoving );
		Renderer?.PlaybackRate = MoveSpeed/20;
		RotationSpeed = MoveSpeed/20;
	}

	protected override void OnFixedUpdate()
	{
		if ( Target == null || Controller == null )
			return;

		Renderer?.GameObject.LocalPosition = Vector3.Zero;

		float currentDuration = isMoving ? MoveDuration : IdleDuration;

		if ( timeSinceStateChange > currentDuration )
		{
			isMoving = !isMoving;
			timeSinceStateChange = 0;

			Renderer?.Set( "Walk", isMoving );
		}

		if ( isMoving )
		{
			var directionToTarget = ( Target.WorldPosition - WorldPosition ).WithZ( 0 ).Normal;

			if ( directionToTarget.Length > 0.01f )
			{
				var desiredRotation = Rotation.LookAt( directionToTarget, Vector3.Up );

				WorldRotation = Rotation.Slerp( WorldRotation, desiredRotation, Time.Delta * RotationSpeed );
			}

			Controller.WishVelocity = WorldRotation.Forward * MoveSpeed;
		}
		else
		{
			Controller.WishVelocity = Vector3.Zero;
		}
	}
}