namespace DaemonEngine.ECS;

public interface IEntity
{
    Guid UUID { get; }
    string Name { get; set; }

    TComponent? GetComponent<TComponent>() where TComponent : class, IComponent;
    TComponent AddComponent<TComponent>() where TComponent : class, IComponent;
    void RemoveComponent<TComponent>() where TComponent : class, IComponent;
    bool HasComponent<TComponent>() where TComponent : class, IComponent;

    IEnumerable<IComponent> GetComponents();
}

public class EntityBase : IEntity
{
    public EntityBase(string name)
    {
        UUID = Guid.NewGuid();
        Name = name;

        Components = new List<IComponent>();
    }

    public Guid UUID { get; }
    public string Name { get; set; }

    protected List<IComponent> Components { get; }

    public TComponent? GetComponent<TComponent>()
        where TComponent : class, IComponent
    {
        if (!HasComponent<TComponent>())
        {
            return null;
        }

        var component = Components.OfType<TComponent>().SingleOrDefault();
        return component;
    }

    public TComponent AddComponent<TComponent>()
        where TComponent : class, IComponent
    {
        if (HasComponent<TComponent>())
        {
            return GetComponent<TComponent>()!;
        }

        var component = Activator.CreateInstance<TComponent>();

        Components.Add(component);

        return component;
    }

    public void RemoveComponent<TComponent>()
        where TComponent : class, IComponent
    {
        if (!HasComponent<TComponent>())
        {
            return;
        }

        var component = Components.OfType<TComponent>().SingleOrDefault();
        Components.Remove(component!);
    }

    public bool HasComponent<TComponent>()
        where TComponent : class, IComponent
    {
        return Components.OfType<TComponent>().Any();
    }

    public IEnumerable<IComponent> GetComponents()
    {
        return Components;
    }
}
