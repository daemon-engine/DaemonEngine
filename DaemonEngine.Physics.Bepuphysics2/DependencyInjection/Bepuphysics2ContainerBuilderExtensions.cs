using Autofac;
using BepuPhysics;
using BepuUtilities;
using DaemonEngine.DependencyInjection;
using DaemonEngine.Physics.Worlds;

namespace DaemonEngine.Physics.Bepuphysics2.DependencyInjection;

public static class Bepuphysics2ContainerBuilderExtensions
{
    public static IDaemonEngineContainerBuilder RegisterBepuphysics2Physics(this IDaemonEngineContainerBuilder builder)
    {
        builder.ContainerBuilder
            .RegisterType<DefaultTimestepper>()
            .As<ITimestepper>()
            .InstancePerLifetimeScope();

        builder.ContainerBuilder
            .RegisterType<ThreadDispatcher>()
            .UsingConstructor(typeof(int))
            .WithParameter(TypedParameter.From(4))
            .As<IThreadDispatcher>()
            .InstancePerLifetimeScope();

        builder.ContainerBuilder
            .RegisterType<Bepuphysics2World>()
            .As<IWorld>()
            .InstancePerLifetimeScope();

        return builder;
    }
}
