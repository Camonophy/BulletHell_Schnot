using Sandbox;

public sealed class SimpleScore : Component
{

	private static int Score {get; set;}

	protected override void OnStart()
	{
		Score = 0;
	}

	public static void AddScore (int amount)
	{
		Score += amount;
	}
	protected override void OnUpdate()
	{
		Gizmo.Draw.ScreenText(
			$"Score: {Score}",
			new Vector2(10, 10)
		);
	}
}
