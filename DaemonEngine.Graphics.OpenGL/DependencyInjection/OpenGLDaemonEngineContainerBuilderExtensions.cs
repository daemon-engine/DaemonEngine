using Autofac;
using DaemonEngine.DependencyInjection;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.OpenGL.Factories;
using DaemonEngine.Graphics.OpenGL.Renderer;
using DaemonEngine.Graphics.Renderer;
using Serilog;

namespace DaemonEngine.Graphics.OpenGL.DependencyInjection;

public static class OpenGLDaemonEngineContainerBuilderExtensions
{
    public static IDaemonEngineContainerBuilder RegisterOpenGLFactory(this IDaemonEngineContainerBuilder builder)
    {
        builder.ContainerBuilder
            .RegisterType<OpenGLGraphicsFactory>()
            .As<IGraphicsFactory>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();
        builder.ContainerBuilder
            .RegisterType<OpenGLContext>()
            .As<IGraphicsContext>()
            .InstancePerLifetimeScope();

        return builder;
    }

    public static IDaemonEngineContainerBuilder RegisterOpenGLRenderer(this IDaemonEngineContainerBuilder builder)
    {
        builder.ContainerBuilder
            .RegisterType<OpenGLRenderer>()
            .Keyed<IRenderer>(RendererApi.OpenGL);

        builder.ContainerBuilder
            .Register((cc) =>
        {
            var logger = cc.Resolve<ILogger>();
            return new OpenGLRenderer(logger);
        })
            .As<IRenderer>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return builder;
    }
}
