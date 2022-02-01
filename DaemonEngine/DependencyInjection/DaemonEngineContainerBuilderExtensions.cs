using Autofac;
using DaemonEngine.Application;
using DaemonEngine.Windows;
using DaemonEngine.Windows.DependencyInjection;
using Serilog;

namespace DaemonEngine.DependencyInjection;

public static class DaemonEngineContainerBuilderExtensions
{
    public static IDaemonEngineContainerBuilder RegisterApplication<TApplication>(this IDaemonEngineContainerBuilder builder)
        where TApplication : class, IApplication
    {
        builder.ContainerBuilder
            .RegisterType<TApplication>()
            .As<IApplication>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        var windowOptions = new WindowOptions
        {
            Title = "Test Window",
            Width = 1024,
            Height = 768
        };
        builder.RegisterWindow(windowOptions);

        return builder;
    }

    //public static IDaemonEngineContainerBuilder RegisterWindowOld(this IDaemonEngineContainerBuilder builder)
    //{
    //    builder.ContainerBuilder
    //        .RegisterType<WindowFactory>()
    //        .As<IWindowFactory>()
    //        .AsImplementedInterfaces()
    //        .InstancePerLifetimeScope();

    //    builder.ContainerBuilder
    //        .Register((cc) =>
    //        {
    //            var windowOptions = new WindowOptions
    //            {
    //                Title = "Test Window",
    //                Width = 1024,
    //                Height = 768
    //            };

    //            var windowFactory = cc.Resolve<IWindowFactory>();
    //            return windowFactory.CreateWindow(windowOptions);
    //        })
    //        .As<IWindow>()
    //        .AsImplementedInterfaces()
    //        .InstancePerLifetimeScope();

    //    builder.ContainerBuilder
    //        .RegisterType<Input>()
    //        .As<IInput>()
    //        .AsImplementedInterfaces()
    //        .InstancePerLifetimeScope();

    //    return builder;
    //}

    public static IDaemonEngineContainerBuilder RegisterLogging(this IDaemonEngineContainerBuilder builder)
    {
        builder.ContainerBuilder
            .Register((cc) =>
        {
            return new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        })
            .As<ILogger>()
            .AsImplementedInterfaces()
            .InstancePerLifetimeScope();

        return builder;
    }

    public static void BuildAndRun(this IDaemonEngineContainerBuilder builder)
    {
        var container = builder.ContainerBuilder.Build();

        using var scope = container.BeginLifetimeScope();
        var application = scope.Resolve<IApplication>();
        application.Run();
    }
}
