using DaemonEngine.Mathematics;

namespace DaemonEngine.Graphics;

public interface ICamera
{
    Matrix4 ProjectionMatrix { get; }
    Matrix4 ViewMatrix { get; }
    Vector3 Position { get; set; }
}
