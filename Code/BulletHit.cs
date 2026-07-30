using Sandbox;

public sealed class BulletHit : Component, Component.ITriggerListener
{
    public void OnTriggerEnter(Collider other)
    {
        Log.Info($"Trigger: {other.GameObject.Name}");

        if (other.Tags.Has( "mob" ) )
        {
            Log.Info("Objekt war ein mob");

		    Log.Info("1");
            SimpleScore.AddScore(1);

            Log.Info("2");
            other.GameObject.Parent?.Destroy();

            Log.Info("3");
            other.GameObject.Destroy();

            Log.Info("4");
            this.GameObject.Destroy();
        } else
        {
            return;
        }
    }
}
