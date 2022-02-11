namespace DaemonEngine.Graphics.Renderer;

public interface IFramebuffer
{
    uint GetColorAttachment(int index);

    void Resize(int width, int height);
    void Clear();

    void Bind();
    void Unbind();
}
