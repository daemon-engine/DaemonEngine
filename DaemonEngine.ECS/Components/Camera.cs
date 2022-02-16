using DaemonEngine.Graphics;

namespace DaemonEngine.ECS.Components;

public class Camera : ComponentBase
{
    public Camera()
    {
        Primary = true;
    }

    public override string Name => $"{nameof(Camera)} ({(Primary ? "Primary" : "Not-Primary")})";

    public bool Primary { get; set; }
    public ICamera MainCamera { get; set; }
}

