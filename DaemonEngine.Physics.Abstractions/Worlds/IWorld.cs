namespace DaemonEngine.Physics.Worlds;

public interface IWorld
{
    void AddStatic();
    object AddDynamic();

    object GetBodyReference(object bodyHandle);

    void Step();
}
