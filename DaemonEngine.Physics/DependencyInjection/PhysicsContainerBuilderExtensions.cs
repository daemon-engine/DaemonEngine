using Autofac;
using DaemonEngine.DependencyInjection;
using DaemonEngine.Physics.Bepuphysics2.DependencyInjection;

namespace DaemonEngine.Physics.DependencyInjection;

public static class PhysicsContainerBuilderExtensions
{
    public static IDaemonEngineContainerBuilder RegisterPhysics(this IDaemonEngineContainerBuilder builder)
    {
        builder.RegisterBepuphysics2Physics();

        builder.ContainerBuilder
            .RegisterType<Physics>()
            .As<IPhysics>()
            .InstancePerLifetimeScope();

        return builder;
    }
}
