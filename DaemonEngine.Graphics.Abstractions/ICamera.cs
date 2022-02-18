using DaemonEngine.Mathematics;

namespace DaemonEngine.Graphics;

public interface ICamera
{
    Matrix4 ProjectionMatrix { get; set; }
    Matrix4 ViewMatrix { get; set; }
    Vector3 Position { get; set; }
}
