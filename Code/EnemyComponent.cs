using Sandbox;

public sealed class EnemyComponent : Component
{
	
	private enum Direction
	{
		Up,
		Down
	}

	[Property]
	//[Range( 10f, 100f, 1f )]]
	public float MoveSpeed { get; set; } = 100f;

	[Property] public int ShootSpeed { get; set; } = 10;

	[Property]
	public Component ReferenceWall { get; set; }

	[Property]
	public GameObject BulletPrefab { get; set; }

	private int _currentTime;
	private Direction _direction = Direction.Down;
	

	// Called every frame
	protected override void OnUpdate()
	{
		var pos = WorldPosition;
		var wallCollider = ReferenceWall?.GetComponent<BoxCollider>();
		var bounds = wallCollider.LocalBounds;
		var bottom = bounds.Mins.x;
		var top = bounds.Maxs.x;
		//pos2.x = MathX.Clamp( pos2.x, bottom, top );
		if ( _direction == Direction.Up ) {
			pos.x += 1;
			if ( pos.x > ReferenceWall.WorldPosition.x + 100 ) {
				_direction = Direction.Down;
			}
		} else {
			pos.x -= 1;
			if ( pos.x < ReferenceWall.WorldPosition.x - 100 ) {
				_direction = Direction.Up;
			}
		}
		
		WorldPosition = pos;
		_currentTime--;

		if ( _currentTime <= 0 )
		{
			if ( BulletPrefab != null )
			{
				var bullet = BulletPrefab.Clone( WorldPosition );
				bullet.GetComponent<BulletComponent>().BulletSpeed = 2;
			}

			_currentTime = ShootSpeed;
		}
	}

	protected override void OnStart()
	{
		WorldPosition = ReferenceWall.WorldPosition;
		var pos = WorldPosition;
		pos.z += 40f;
		WorldPosition = pos;
		_currentTime = ShootSpeed;
	}
}
