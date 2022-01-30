namespace DaemonEngine.Graphics.Renderer;

public class BufferLayout : IBufferLayout
{
    public int Stride { get; private set; }
    public List<BufferElement> Elements { get; }

    public BufferLayout(List<BufferElement> elements)
    {
        Elements = elements;
        CalculateOffsetAndStride();
    }

    private void CalculateOffsetAndStride()
    {
        int offset = 0;
        Stride = 0;
        foreach (BufferElement element in Elements)
        {
            element.Offset = offset;
            offset += element.Size;
            Stride += element.Size;
        }
    }
}
