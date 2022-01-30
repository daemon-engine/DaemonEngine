namespace DaemonEngine.Graphics.Renderer;

public interface ITexture
{
    void Bind(int slot = 0);
    void Unbind();
}
