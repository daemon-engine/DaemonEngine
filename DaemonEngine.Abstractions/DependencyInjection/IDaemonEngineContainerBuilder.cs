using Autofac;

namespace DaemonEngine.DependencyInjection;

public interface IDaemonEngineContainerBuilder
{
    ContainerBuilder ContainerBuilder { get; }
}
