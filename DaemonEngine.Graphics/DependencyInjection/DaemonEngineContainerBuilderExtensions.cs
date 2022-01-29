using Autofac;
using DaemonEngine.DependencyInjection;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Factories.Windows;
using DaemonEngine.Graphics.Windows;
using DaemonEngine.Windows;

namespace DaemonEngine.Graphics.DependencyInjection;

public static class DaemonEngineContainerBuilderExtensions
{
    public static IDaemonEngineContainerBuilder RegisterGLFactory(this IDaemonEngineContainerBuilder builder)
    {
        builder.ContainerBuilder
            .RegisterType<GLFactory>()
            .As<IGLFactory>()
            .AsImplementedInterfaces();

        return builder;
    }

    public static IDaemonEngineContainerBuilder RegisterWindow(this IDaemonEngineContainerBuilder builder, WindowOptions windowOptions)
    {
        builder.RegisterWindowFactory<GLWindow>();
        builder.RegisterGLFactory();

        builder.ContainerBuilder
            .Register((cc) =>
        {
            var windowFactory = cc.Resolve<IWindowFactory<GLWindow>>();
            return windowFactory.CreateWindow(windowOptions);
        })
            .As<IWindow>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return builder;
    }

    public static IDaemonEngineContainerBuilder RegisterWindowFactory<TWindow>(this IDaemonEngineContainerBuilder builder)
        where TWindow : class, IWindow
    {
        builder.ContainerBuilder
            .RegisterType<WindowFactory<TWindow>>()
            .As<IWindowFactory<TWindow>>()
            .AsImplementedInterfaces();

        return builder;
    }
}
