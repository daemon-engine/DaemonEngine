using DaemonEngine.Mathematics;

namespace DaemonEngine.Physics.Worlds;

public interface IWorld
{
    void AddStatic(Vector3 position);
    object AddDynamic(Vector3 position, float mass);

    object GetBodyReference(object bodyHandle);

    void Step();
}
