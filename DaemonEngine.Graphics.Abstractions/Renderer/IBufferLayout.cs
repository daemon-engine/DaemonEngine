namespace DaemonEngine.Graphics.Renderer;

public interface IBufferLayout
{
    int Stride { get; }
    List<BufferElement> Elements { get; }
}
