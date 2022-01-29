using Autofac;

namespace DaemonEngine.DependencyInjection;

public class DaemonEngineContainerBuilder : IDaemonEngineContainerBuilder
{
    public DaemonEngineContainerBuilder()
    {
        ContainerBuilder ??= new ContainerBuilder();
    }

    public ContainerBuilder ContainerBuilder { get; }
}

