using System.Runtime.CompilerServices;
using Sandbox;

public sealed class PlayerStats : Component
{
	// Stats
	[Property] public float Health { get; set; } = 100f;
	[Property] public float MaxHealth { get; set; } = 100f;
	[Property] public float Armor { get; set; } = 0f;
	[Property] public float MaxArmor { get; set; } = 100f;
	
	private static int Score { get; set; } = 0;
	public TimeSince TimeAlive {get; set; } = 0f;
	
	public static void AddScore (int amount)
	{
		Score += amount;
	}

	public int GetScore()
	{
		return Score;
	}

	protected override void OnStart()
	{
		base.OnStart();
		Score = 0;
	}
	protected override void OnUpdate()
	{

	}
}
