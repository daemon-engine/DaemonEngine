namespace DaemonEngine.ECS;

public interface IComponent
{
    string Name { get; }
}

public abstract class ComponentBase : IComponent
{
    public abstract string Name { get; }
}
