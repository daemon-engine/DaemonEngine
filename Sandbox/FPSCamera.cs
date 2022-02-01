using DaemonEngine.Windows.Inputs;
using System.Numerics;

namespace Sandbox;

public class FPSCamera
{
    private readonly IInput _input;

    private Vector3 _position = new Vector3(0.0f, 0.0f, 5.0f);

    private bool _firstMouse = true;
    private float _lastX = 0.0f;
    private float _lastY = 0.0f;

    private float _yaw;
    private float _pitch;

    private Vector3 _cameraFront = new(0.0f, 0.0f, -1.0f);

    private Vector3 CAMERA_UP = new(0.0f, 1.0f, 0.0f);

    private const float MOVEMENT_SPEED = 2.0f;
    private const float SENSITIVITY = 2.0f;

    public FPSCamera(float fieldOfView, float aspectRatio, IInput input, float nearClip = 0.1f, float farClip = 100.0f)
    {
        _input = input;

        FieldOfView = fieldOfView;
        AspectRatio = aspectRatio;
        NearClip = nearClip;
        FarClip = farClip;

        ViewMatrix = Matrix4x4.CreateLookAt(_position, _position + _cameraFront, CAMERA_UP);
        UpdateProjection();
    }

    public float FieldOfView { get; }
    public float FieldOfViewRadians { get { return FieldOfView * (3.14f / 180.0f); } }
    public float AspectRatio { get; private set; }
    public float NearClip { get; }
    public float FarClip { get; }

    public Matrix4x4 ViewMatrix { get; private set; }
    public Matrix4x4 ProjectionMatrix { get; private set; }

    public void Update(float deltaTime)
    {
        Move(deltaTime);
        Rotate(deltaTime);
        ViewMatrix = Matrix4x4.CreateLookAt(_position, _position + _cameraFront, CAMERA_UP);
    }

    private void Rotate(float deltaTime)
    {
        var mousePosition = _input.GetMousePosition();

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

        Vector3 front = new()
        {
            X = (float)Math.Cos(_yaw * (3.14 / 180.0f)) * (float)Math.Cos(_pitch * (3.14 / 180.0f)),
            Y = (float)Math.Sin(_pitch * (3.14 / 180.0f)),
            Z = (float)Math.Sin(_yaw * (3.14 / 180.0f)) * (float)Math.Cos(_pitch * (3.14 / 180.0f))
        };
        _cameraFront = Vector3.Normalize(front);
    }

    private void Move(float deltaTime)
    {
        if (_input.IsKeyDown(Keycode.W))
        {
            _position += _cameraFront * MOVEMENT_SPEED * deltaTime;
        }
        else if (_input.IsKeyDown(Keycode.S))
        {
            _position -= _cameraFront * MOVEMENT_SPEED * deltaTime;
        }

        if (_input.IsKeyDown(Keycode.A))
        {
            _position -= Vector3.Normalize(Vector3.Cross(_cameraFront, CAMERA_UP)) * MOVEMENT_SPEED * deltaTime;
        }
        else if (_input.IsKeyDown(Keycode.D))
        {
            _position += Vector3.Normalize(Vector3.Cross(_cameraFront, CAMERA_UP)) * MOVEMENT_SPEED * deltaTime;
        }
    }

    public void SetViewport(int width, int height)
    {
        AspectRatio = (float)width / (float)height;
        UpdateProjection();
    }

    private void UpdateProjection()
    {
        ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(FieldOfViewRadians, AspectRatio, NearClip, FarClip);
    }
}
