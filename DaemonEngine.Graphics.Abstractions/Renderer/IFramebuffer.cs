namespace DaemonEngine.Graphics.Renderer;

public interface IFramebuffer
{
    uint GetColorAttachment();

    void Clear();

    void Bind();
    void Unbind();
}
