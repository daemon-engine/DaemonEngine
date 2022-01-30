namespace DaemonEngine.Graphics.Renderer;

public interface ITexture
{
    void Bind(uint slot = 0);
    void Unbind();
}
