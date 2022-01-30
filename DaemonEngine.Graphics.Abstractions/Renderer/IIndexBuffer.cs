namespace DaemonEngine.Graphics.Renderer;

public interface IIndexBuffer
{
    int Count { get; }

    void Bind();
    void Unbind();
}

