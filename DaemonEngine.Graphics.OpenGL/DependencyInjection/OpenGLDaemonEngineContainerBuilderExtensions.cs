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
    public static IDaemonEngineContainerBuilder RegisterOpenGLRenderer(this IDaemonEngineContainerBuilder builder, RendererApi rendererApi)
    {
        builder.ContainerBuilder
            .RegisterType<OpenGLGraphicsFactory>()
            .Keyed<IGraphicsFactory>(rendererApi)
            .As<IGraphicsFactory>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.ContainerBuilder
            .Register((cc) =>
        {
            var logger = cc.Resolve<ILogger>();
            return new OpenGLRenderer(logger);
        })
            .Keyed<IRenderer>(rendererApi)
            .As<IRenderer>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return builder;
    }
}
