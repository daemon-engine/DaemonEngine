using DaemonEngine.Core.Inputs;
using DaemonEngine.ECS;
using DaemonEngine.ECS.Components;
using DaemonEngine.Graphics;
using DaemonEngine.Mathematics;
using DaemonEngine.Scripting;

namespace Sandbox;

public class MainCamera : ICamera
{
    private Vector3 _position;
    private Vector3 _cameraFront = new(0.0f, 0.0f, -1.0f);
    private readonly Vector3 _cameraUp = new(0.0f, 1.0f, 0.0f);

    public MainCamera(Vector3 position)
    {
        Position = position;

        ViewMatrix = Matrix4.LookAt(Position, Position + _cameraFront, _cameraUp);
        ProjectionMatrix = Matrix4.Perspective(Maths.ToRadians(60.0f), 1366.0f / 768.0f, 0.1f, 100.0f);
    }

    public Vector3 Position
    {
        get { return _position; }
        set
        {
            _position = value;
            ViewMatrix = Matrix4.LookAt(Position, Position + _cameraFront, _cameraUp);
        }
    }
    public Vector3 Front => _cameraFront;

    public Matrix4 ProjectionMatrix { get; set; }
    public Matrix4 ViewMatrix { get; set; }
}

internal class FPSCameraController : NativeScriptBase
{
    private Transform? _transform;
    private Camera? _camera;

    public FPSCameraController(IEntity entity)
    {
        SetEntity(entity);
    }

    public override void Start()
    {
        _transform = Entity.GetComponent<Transform>()!;
        _camera = Entity.GetComponent<Camera>()!;
    }

    public override void Update(float deltaTime)
    {
        var camPosition = _camera!.MainCamera.Position;

        if (Input.IsKeyPressed(Keycode.A))
        {
            camPosition.X -= 1.0f * deltaTime;
        }
        else if (Input.IsKeyPressed(Keycode.D))
        {
            camPosition.X += 1.0f * deltaTime;
        }

        if (Input.IsKeyPressed(Keycode.W))
        {
            camPosition.Z -= 1.0f * deltaTime;
        }
        else if (Input.IsKeyPressed(Keycode.S))
        {
            camPosition.Z += 1.0f * deltaTime;
        }

        _camera!.MainCamera.Position = camPosition;
        _transform!.Position = camPosition;
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
        script.AttachScript<FPSCameraController>(this);
    }
}
