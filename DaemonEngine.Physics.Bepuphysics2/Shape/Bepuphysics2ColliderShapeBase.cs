using BepuPhysics;
using BepuPhysics.Collidables;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Physics.Shapes;

namespace DaemonEngine.Physics.Bepuphysics2.Shape;

public class Bepuphysics2ColliderShapeBase : ColliderShapeBase
{
    internal Bepuphysics2ColliderShapeBase(IMesh mesh, CollidableDescription collidableDescription, BodyActivityDescription bodyActivityDescription, BodyInertia bodyInertia)
        : base(mesh)
    {
        CollidableDescription = collidableDescription;
        BodyActivityDescription = bodyActivityDescription;
        BodyInertia = bodyInertia;
    }

    public CollidableDescription CollidableDescription { get; }
    public BodyActivityDescription BodyActivityDescription { get; }
    public BodyInertia BodyInertia { get; }
}
