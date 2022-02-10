namespace DaemonEngine.Graphics.Renderer;

public interface IFramebuffer
{
    uint GetColorAttachment();

    void Bind();
    void Unbind();
}
