using Autofac;
using DaemonEngine.DependencyInjection;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Windows.Inputs;
using Serilog;

namespace DaemonEngine.Windows.GraphicsLibraryFramework.DependencyInjection;

public static class GlfwDaemonEngineContainerBuilderExtensions
{
    public static IDaemonEngineContainerBuilder RegisterGlfwWindow(this IDaemonEngineContainerBuilder builder, WindowOptions windowOptions)
    {
        builder.ContainerBuilder
            .Register((cc) =>
            {
                var logger = cc.Resolve<ILogger>();
                return new GlfwWindow(logger, windowOptions);
            })
            .As<IWindow>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return builder;
    }

    public static IDaemonEngineContainerBuilder RegisterGlfwInput(this IDaemonEngineContainerBuilder builder)
    {
        builder.ContainerBuilder
            .RegisterType<GlfwInput>()
            .As<IInput>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return builder;
    }
}
