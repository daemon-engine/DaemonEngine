using Autofac;
using DaemonEngine.DependencyInjection;

namespace DaemonEngine.Windows.GraphicsLibraryFramework.DependencyInjection;

public static class GlfwDaemonEngineContainerBuilderExtensions
{
    public static IDaemonEngineContainerBuilder RegisterGlfwWindow(this IDaemonEngineContainerBuilder builder, WindowOptions windowOptions)
    {
        builder.ContainerBuilder
            .RegisterType<GlfwWindow>()
            .As<IWindow>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return builder;
    }
}
