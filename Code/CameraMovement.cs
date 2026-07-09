using Sandbox;

public sealed class CameraMovement : Component
{
    [Property]
    public GameObject Target { get; set; }

    [Property]
    public float Height = 600;

    [Property]
    public float FollowSpeed = 8f;

    [Property]
    public float Offset = 90;

    protected override void OnUpdate()
    {
        if ( Target == null )
            return;

        Vector3 targetPosition =
            Target.Transform.Position + Vector3.Up * Height - Vector3.Backward*Offset;

        Transform.Position = Vector3.Lerp(
            Transform.Position,
            targetPosition,
            Time.Delta * FollowSpeed
        );
    }
}
