using DaemonEngine.ECS;
using DaemonEngine.ECS.Components;
using DaemonEngine.Graphics;
using DaemonEngine.Mathematics;
using DaemonEngine.Scripting;

namespace Sandbox;

public class MainCamera : ICamera
{
    private Vector3 _cameraFront = new(0.0f, 0.0f, -1.0f);
    private readonly Vector3 _cameraUp = new(0.0f, 1.0f, 0.0f);

    public MainCamera(Vector3 position)
    {
        Position = position;

        ViewMatrix = Matrix4.LookAt(Position, Position + _cameraFront, _cameraUp);
        ProjectionMatrix = Matrix4.Perspective(Maths.ToRadians(60.0f), 1366.0f / 768.0f, 0.1f, 100.0f);
    }

    public Vector3 Position { get; set; }
    public Vector3 ViewPosition => Position;
    public Vector3 Front => _cameraFront;

    public Matrix4 ProjectionMatrix { get; set; }
    public Matrix4 ViewMatrix { get; set; }
}

internal class FPSCameraController : NativeScriptBase
{
    public override void Start()
    {
    }

    public override void Update(float deltaTime)
    {
    }
}

internal class FPSCameraEntity : EntityBase
{
    public FPSCameraEntity(Vector3 position) 
        : base("FPS Camera Entity")
    {
        var transform = AddComponent<Transform>();
        transform.Position = position;

        var camera = AddComponent<Camera>();
        camera.MainCamera = new MainCamera(position);

        var script = AddComponent<NativeScript>();
        script.Script = new FPSCameraController();
    }
}
