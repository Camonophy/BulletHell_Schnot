public sealed class TopDownCamera : Component
{
    [Property]
    public GameObject Target { get; set; }

    [Property]
    public float Height = 200;

    protected override void OnUpdate()
    {
        if ( Target == null )
            return;

        Transform.Position =
            Target.Transform.Position + Vector3.Up * Height;

        Transform.Rotation =
            Rotation.LookAt( Vector3.Down );
    }
}