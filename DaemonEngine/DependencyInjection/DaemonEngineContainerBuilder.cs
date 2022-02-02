using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace DaemonEngine.DependencyInjection;

public class DaemonEngineContainerBuilder : IDaemonEngineContainerBuilder
{
    public DaemonEngineContainerBuilder()
    {
        ServiceCollection ??= new ServiceCollection();
        ContainerBuilder ??= new ContainerBuilder();
    }

    public ContainerBuilder ContainerBuilder { get; }
    public IServiceCollection ServiceCollection { get; }
}

