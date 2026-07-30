using System.Runtime.CompilerServices;
using Sandbox;

public sealed class PlayerStats : Component
{
	// Stats
	[Property] public int Health { get; set; } = 100;
	[Property] public int MaxHealth { get; set; } = 100;
	[Property] public int Armor { get; set; } = 0;
	[Property] public int MaxArmor { get; set; } = 100;
	
	private static int Score { get; set; } = 0;
	public TimeSince TimeAlive {get; set; } = 0f;


	[Property] List<string> Inventory { get; set; } = new List<string>
	{
		"weapon_pistol"
	};

	public int ActiveInventorySlots = 0;

	public int MaxInventorySlots = 9;
	public static void AddScore (int amount)
	{
		Score += amount;
	}

	public int GetScore()
	{
		return Score;
	}
	protected override void OnUpdate()
	{

	}
}
