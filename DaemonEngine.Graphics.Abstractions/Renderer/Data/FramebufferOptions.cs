using DaemonEngine.Mathematics;

namespace DaemonEngine.Graphics.Renderer.Data;

public enum FramebufferAttachmentType
{
    None = 0,
    RGB,
    RGBA8,
    DEPTH24STENCIL8
}

public class FramebufferOptions
{
    public int Width { get; set; }
    public int Height { get; set; }
    public Vector4 ClearColor { get; set; }
    public List<FramebufferAttachmentType> Attachments { get; set; } 
}
