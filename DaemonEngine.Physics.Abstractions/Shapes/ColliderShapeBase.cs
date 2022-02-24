using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Physics.Shapes;

public interface IColliderShape
{
    IMesh Mesh { get; }
}

public class ColliderShapeBase : IColliderShape
{
    public ColliderShapeBase(IMesh mesh)
    {
        Mesh = mesh;
    }

    public IMesh Mesh { get; }
}
