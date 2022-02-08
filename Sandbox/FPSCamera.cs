using DaemonEngine.Core.Inputs;
using DaemonEngine.Maths;
using Math = DaemonEngine.Maths.Math;

namespace Sandbox;

public class FPSCamera
{
    private bool _firstMouse = true;
    private float _lastX = 0.0f;
    private float _lastY = 0.0f;

    private float _yaw = -90;
    private float _pitch;

    private Vector3 _cameraFront = new(0.0f, 0.0f, -1.0f);
    private readonly Vector3 _cameraUp = new(0.0f, 1.0f, 0.0f);

    private const float MOVEMENT_SPEED = 2.0f;
    private const float SENSITIVITY = 2.0f;

    public FPSCamera(float fieldOfView, float aspectRatio, float nearClip = 0.1f, float farClip = 100.0f)
    {
        FieldOfView = fieldOfView;
        AspectRatio = aspectRatio;
        NearClip = nearClip;
        FarClip = farClip;

        ViewMatrix = Matrix4.LookAt(Position, Position + _cameraFront, _cameraUp);
        UpdateProjection();
    }

    public float FieldOfView { get; }
    public float FieldOfViewRadians { get { return FieldOfView * (3.14f / 180.0f); } }
    public float AspectRatio { get; private set; }
    public float NearClip { get; }
    public float FarClip { get; }

    public Vector3 Position { get; set; } = new Vector3(0.0f, 0.0f, 5.0f);
    public Vector3 Front => _cameraFront;
    public Matrix4 ViewMatrix { get; private set; }
    public Matrix4 ProjectionMatrix { get; private set; }

    public void Update(float deltaTime)
    {
        Move(deltaTime);
        Rotate(deltaTime);
        ViewMatrix = Matrix4.LookAt(Position, Position + _cameraFront, _cameraUp);
    }

    private void Rotate(float deltaTime)
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
        if (_pitch > 89.0f)
            _pitch = 89.0f;
        if (_pitch < -89.0f)
            _pitch = -89.0f;

        Vector3 front_ = new()
        {
            X = Math.Cos(_yaw * (3.14 / 180.0f)) * Math.Cos(_pitch * (3.14 / 180.0f)),
            Y = Math.Sin(_pitch * (3.14 / 180.0f)),
            Z = Math.Sin(_yaw * (3.14 / 180.0f)) * Math.Cos(_pitch * (3.14 / 180.0f))
        };
        _cameraFront = Vector3.Normalize(front_);
    }

    private void Move(float deltaTime)
    {
        if (Input.IsKeyPressed(Keycode.E))
        {
            Position += _cameraUp * MOVEMENT_SPEED * deltaTime;
        }
        else if (Input.IsKeyPressed(Keycode.Q))
        {
            Position -= _cameraUp * MOVEMENT_SPEED * deltaTime;
        }

        if (Input.IsKeyPressed(Keycode.W))
        {
            Position += _cameraFront * MOVEMENT_SPEED * deltaTime;
        }
        else if (Input.IsKeyPressed(Keycode.S))
        {
            Position -= _cameraFront * MOVEMENT_SPEED * deltaTime;
        }

        if (Input.IsKeyPressed(Keycode.A))
        {
            Position -= Vector3.Normalize(Vector3.Cross(_cameraFront, _cameraUp)) * MOVEMENT_SPEED * deltaTime;
        }
        else if (Input.IsKeyPressed(Keycode.D))
        {
            Position += Vector3.Normalize(Vector3.Cross(_cameraFront, _cameraUp)) * MOVEMENT_SPEED * deltaTime;
        }
    }

    public void SetViewport(int width, int height)
    {
        AspectRatio = (float)width / (float)height;
        UpdateProjection();
    }

    private void UpdateProjection()
    {
        ProjectionMatrix = Matrix4.Perspective(FieldOfViewRadians, AspectRatio, NearClip, FarClip);
    }
}
