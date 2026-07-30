using Sandbox;

public sealed class BulletHit : Component, Component.ITriggerListener
{
    public void OnTriggerEnter(Collider other)
    {
        Log.Info($"Trigger: {other.GameObject.Name}");

        if (other.Tags.Has( "mob" ) )
        {
            SimpleScore.AddScore(1);

            other.GameObject.Parent?.Destroy();
            other.GameObject.Destroy();
            this.GameObject.Destroy();
        }

        return;
    }
}
