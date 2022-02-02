using Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace DaemonEngine.DependencyInjection;

public interface IDaemonEngineContainerBuilder
{
    ContainerBuilder ContainerBuilder { get; }
    IServiceCollection ServiceCollection { get; }
}
