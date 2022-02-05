namespace DaemonEngine.Graphics.Renderer;

public interface ITexture
{
    void SetData(int size, IntPtr data);

    void Bind(uint slot = 0);
    void Unbind();

    /// Temp
    uint GetId();
}
