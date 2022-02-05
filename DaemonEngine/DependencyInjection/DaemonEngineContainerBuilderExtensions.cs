using Autofac;
using Autofac.Extensions.DependencyInjection;
using DaemonEngine.Application;
using DaemonEngine.Core;
using DaemonEngine.Factories;
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
            .AutoActivate()
            .InstancePerLifetimeScope();

        builder.ContainerBuilder
            .RegisterType<Cursor>()
            .As<ICursor>()
            .InstancePerLifetimeScope();

        builder.ContainerBuilder
            .RegisterType<LayerFactory>()
            .As<ILayerFactory>()
            .InstancePerLifetimeScope();

        var windowOptions = new WindowOptions
        {
            Title = "Test Window",
            Width = 1366,
            Height = 768
        };
        builder.RegisterWindow(windowOptions);

        return builder;
    }

    public static IDaemonEngineContainerBuilder RegisterLogging(this IDaemonEngineContainerBuilder builder)
    {
        builder.ContainerBuilder
            .Register((cc) =>
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
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
        builder.ContainerBuilder.Populate(builder.ServiceCollection);
        var container = builder.ContainerBuilder.Build();

        builder.ContainerBuilder
            .Register((cc) =>
            {
                var serviceProvider = new AutofacServiceProvider(container);
                return serviceProvider;
            })
            .As<IServiceProvider>()
            .InstancePerLifetimeScope();


        using var scope = container.BeginLifetimeScope();
        try
        {
            var application = scope.Resolve<IApplication>();
            application.Run();
        }
        catch (Exception ex)
        {
            var logger = scope.Resolve<ILogger>();
            logger.Error(ex.Message);
        }
    }
}
