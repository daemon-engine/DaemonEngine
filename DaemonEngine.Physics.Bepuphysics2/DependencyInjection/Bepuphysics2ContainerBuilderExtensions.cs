using Autofac;
using BepuPhysics;
using BepuUtilities;
using DaemonEngine.DependencyInjection;
using DaemonEngine.Graphics;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Data;
using DaemonEngine.Physics.Bepuphysics2.Shape.Factories;
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

        builder.ContainerBuilder
            .RegisterType<Bepuphysics2ColliderShapeFactory>()
            .As<IBepuphysics2ColliderShapeFactory>()
            .InstancePerLifetimeScope();

        return builder;
    }
}
