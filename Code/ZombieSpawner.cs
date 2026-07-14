using Sandbox;
using System;

public sealed class ZombieSpawner : Component
{
	[Property] public PrefabFile ZombiePrefab { get; set; }
	[Property] public GameObject Player { get; set; }

	[Property] public Vector2 MapMin { get; set; } = new Vector2( -2000, -2000 );
	[Property] public Vector2 MapMax { get; set; } = new Vector2( 2000, 2000 );

	[Property] public int MaxZombies { get; set; } = 15;

	[Property] public float MinSpawnDistanceFromPlayer { get; set; } = 800f;
	[Property] public float MaxSpawnDistanceFromPlayer { get; set; } = 1500f;

	[Property] public float SpawnInterval { get; set; } = 2f;

	private TimeSince timeSinceLastSpawn = 0;

	protected override void OnFixedUpdate()
	{
		if ( ZombiePrefab is null || Player is null ) return;
		if ( timeSinceLastSpawn < SpawnInterval ) return;

		int currentZombieCount = Scene.GetAllComponents<EnemyMovement>().Count();
		if ( currentZombieCount >= MaxZombies ) return;

		timeSinceLastSpawn = 0;
		SpawnZombie();
	}

	private void SpawnZombie()
	{
		// Zufälliger Winkel rundherum (0 bis 360°, in Radiant: 0 bis 2π)
		float angle = Game.Random.Float( 0f, MathF.Tau );

		float distance = Game.Random.Float( MinSpawnDistanceFromPlayer, MaxSpawnDistanceFromPlayer );

		var offset = new Vector2( MathF.Cos( angle ), MathF.Sin( angle ) ) * distance;
		var spawnPos = new Vector2( Player.WorldPosition.x, Player.WorldPosition.y ) + offset;

		spawnPos.x = spawnPos.x.Clamp( MapMin.x, MapMax.x );
		spawnPos.y = spawnPos.y.Clamp( MapMin.y, MapMax.y );

		var zombie = GameObject.Clone( ZombiePrefab );
		zombie.WorldPosition = new Vector3( spawnPos.x, spawnPos.y, Player.WorldPosition.z );

		var enemyMovement = zombie.Components.Get<EnemyMovement>(FindMode.EnabledInSelfAndDescendants);
		enemyMovement?.Target = Player; // ???? Woher weiss er hier, welches GameObjekt genau der Player ist, wenn ich doch eigentlich an dieser Stelle noch nicht von diesem weiss.
	}
}
