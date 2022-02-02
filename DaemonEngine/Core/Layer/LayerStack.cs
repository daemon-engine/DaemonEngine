namespace DaemonEngine.Core.Layer;

public class LayerStack
{
    public List<ILayer> Layers { get; }

    public LayerStack()
    {
        Layers = new List<ILayer>();
    }

    public void AddLayer(ILayer layer)
    {
        Layers.Add(layer);
    }

    public void RemoveLayer(ILayer layer)
    {
        Layers.Remove(layer);
    }
}
