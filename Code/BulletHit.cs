using Sandbox;

public sealed class BulletHit : Component, Component.ICollisionListener
{
    public void OnCollisionStart( Collision collision )
    {
        var other = collision.Other.GameObject;

        if ( !other.Tags.Has( "Bullet" ) )
            return;

		SimpleScore.AddScore(1);

        other.Destroy();

		GameObject.Parent?.Destroy();
        GameObject.Destroy();
    }
}
