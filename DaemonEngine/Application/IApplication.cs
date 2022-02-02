using DaemonEngine.Core.Layer;

namespace DaemonEngine.Application;

public interface IApplication
{
    void Run();

    TLayer AddLayer<TLayer>(string name) where TLayer : class, ILayer;
}
