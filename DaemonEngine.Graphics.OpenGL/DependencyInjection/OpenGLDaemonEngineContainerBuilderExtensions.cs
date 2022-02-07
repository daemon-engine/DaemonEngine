using Autofac;
using DaemonEngine.DependencyInjection;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.OpenGL.Factories;
using DaemonEngine.Graphics.OpenGL.Renderer;
using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.OpenGL.DependencyInjection;

public static class OpenGLDaemonEngineContainerBuilderExtensions
{
    public static IDaemonEngineContainerBuilder RegisterOpenGL(this IDaemonEngineContainerBuilder builder)
    {
        builder.ContainerBuilder
            .RegisterType<OpenGLGraphicsFactory>()
            .As<IGraphicsFactory>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        builder.ContainerBuilder
            .RegisterType<OpenGLRenderer>()
            .As<IRenderer>()
            .InstancePerLifetimeScope();

        return builder;
    }
}
