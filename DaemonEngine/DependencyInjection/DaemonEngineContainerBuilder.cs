using Autofac;

namespace DaemonEngine.DependencyInjection;

public interface IDaemonEngineContainerBuilder
{
    ContainerBuilder ContainerBuilder { get; }
}

public class DaemonEngineContainerBuilder : IDaemonEngineContainerBuilder
{
    public DaemonEngineContainerBuilder()
    {
        ContainerBuilder ??= new ContainerBuilder();
    }

    public ContainerBuilder ContainerBuilder { get; }
}

