using DaemonEngine.Application;
using DaemonEngine.Core.Layer;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DaemonEngine.Factories;

public interface ILayerFactory
{
    TLayer CreateLayer<TLayer>(string name) where TLayer : class, ILayer;
}

internal class LayerFactory : ILayerFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    public LayerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _logger = serviceProvider.GetRequiredService<ILogger>();
    }

    public TLayer CreateLayer<TLayer>(string name) 
        where TLayer : class, ILayer
    {
        var layerType = typeof(TLayer);
        if (Activator.CreateInstance(layerType, name, _serviceProvider) is not TLayer layer)
        {
            _logger.Error($"Failed to create layer - {layerType.Name}");
            throw new Exception();
        }
        return layer;
    }
}
