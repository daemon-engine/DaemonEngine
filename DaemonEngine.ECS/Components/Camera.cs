using DaemonEngine.Mathematics;

namespace DaemonEngine.ECS.Components;

public interface ICamera
{
    Matrix4 ProjectionMatrix { get; }
    Matrix4 ViewMatrix { get; }
}

public class Camera : ComponentBase
{
    public Camera()
    {
        Primary = true;
    }

    public bool Primary { get; set; }
    public ICamera MainCamera { get; set; }
}

