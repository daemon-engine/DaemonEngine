using DaemonEngine.Core.Layer;

namespace DaemonEngine.Application;

public interface IApplication
{
    void Run();
    void Stop();

    TLayer AddLayer<TLayer>(string name) where TLayer : class, ILayer;
}
