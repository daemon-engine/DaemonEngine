using DaemonEngine.Core.Inputs;
using DaemonEngine.ECS;
using DaemonEngine.ECS.Components;
using DaemonEngine.EventSystem;
using DaemonEngine.EventSystem.Events.Key;
using DaemonEngine.Graphics;
using DaemonEngine.Mathematics;
using DaemonEngine.Scripting;

namespace Sandbox.Entities;

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
    public Vector3 Front => _cameraFront;

    public Matrix4 ProjectionMatrix { get; set; }
    public Matrix4 ViewMatrix { get; set; }
}

internal class FPSCameraController : NativeScriptBase
{
    private Transform? _transform;
    private Camera? _camera;

    private bool _firstMouse = true;
    private float _lastX = 0.0f;
    private float _lastY = 0.0f;

    private float _yaw = -90;
    private float _pitch;

    private const float MOVEMENT_SPEED = 2.0f;
    private const float SENSITIVITY = 2.0f;

    private Vector3 _cameraFront = new(0.0f, 0.0f, -1.0f);
    private readonly Vector3 _cameraUp = new(0.0f, 1.0f, 0.0f);

    private bool _paused = false;

    public override void Start()
    {
        _transform = Entity.GetComponent<Transform>()!;
        _camera = Entity.GetComponent<Camera>()!;
    }

    public override void Update(float deltaTime)
    {
        if (!_paused)
        {
            Move(deltaTime);
            Rotate();
        }

        _camera!.MainCamera.Position = _transform!.Position;
        _camera!.MainCamera.ViewMatrix = Matrix4.LookAt(_transform!.Position, _transform!.Position + _cameraFront, _cameraUp);
    }

    private void Rotate()
    {
        var mousePosition = Input.GetMousePosition();
        if (_firstMouse)
        {
            _lastX = mousePosition.X;
            _lastY = mousePosition.Y;
            _firstMouse = false;
        }

        float xOffset = mousePosition.X - _lastX;
        float yOffset = _lastY - mousePosition.Y;  // reversed since y-coordinates go from bottom to top
        _lastX = mousePosition.X;
        _lastY = mousePosition.Y;

        xOffset *= (SENSITIVITY / 10.0f);
        yOffset *= (SENSITIVITY / 10.0f);

        _yaw += xOffset;
        _pitch += yOffset;

        // make sure that when pitch is out of bounds, screen doesn't get flipped
        // TODO: Make a clamp method for this (propably in Maths class)
        if (_pitch > 89.0f)
            _pitch = 89.0f;
        if (_pitch < -89.0f)
            _pitch = -89.0f;

        Vector3 front_ = new()
        {
            X = Maths.Cos(_yaw * (3.14 / 180.0f)) * Maths.Cos(_pitch * (3.14 / 180.0f)),
            Y = Maths.Sin(_pitch * (3.14 / 180.0f)),
            Z = Maths.Sin(_yaw * (3.14 / 180.0f)) * Maths.Cos(_pitch * (3.14 / 180.0f))
        };
        _cameraFront = Vector3.Normalize(front_);
    }

    private void Move(float deltaTime)
    {
        if (Input.IsKeyPressed(Keycode.E))
        {
            _transform!.Position += _cameraUp * MOVEMENT_SPEED * deltaTime;
        }
        else if (Input.IsKeyPressed(Keycode.Q))
        {
            _transform!.Position -= _cameraUp * MOVEMENT_SPEED * deltaTime;
        }

        if (Input.IsKeyPressed(Keycode.W))
        {
            _transform!.Position += _cameraFront * MOVEMENT_SPEED * deltaTime;
        }
        else if (Input.IsKeyPressed(Keycode.S))
        {
            _transform!.Position -= _cameraFront * MOVEMENT_SPEED * deltaTime;
        }

        if (Input.IsKeyPressed(Keycode.A))
        {
            _transform!.Position -= Vector3.Normalize(Vector3.Cross(_cameraFront, _cameraUp)) * MOVEMENT_SPEED * deltaTime;
        }
        else if (Input.IsKeyPressed(Keycode.D))
        {
            _transform!.Position += Vector3.Normalize(Vector3.Cross(_cameraFront, _cameraUp)) * MOVEMENT_SPEED * deltaTime;
        }
    }

    public override void OnEvent(IEvent e)
    {
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<KeyPressedEvent>(OnKeyPressedEvent);
    }

    private bool OnKeyPressedEvent(KeyPressedEvent e)
    {
        if (e.KeyCode == (int)Keycode.ESCAPE)
        {
            _paused = !_paused;
        }
        return true;
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
