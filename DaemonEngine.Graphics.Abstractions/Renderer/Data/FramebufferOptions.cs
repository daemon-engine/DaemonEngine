using DaemonEngine.Mathematics;
using System.Collections.Generic;

namespace DaemonEngine.Graphics.Renderer.Data;

public enum FramebufferAttachment
{
    None = 0,
    RGBA8,
    Depth
}

public class FramebufferOptions
{
    public int Width { get; set; }
    public int Height { get; set; }
    public Vector4 ClearColor { get; set; }
    public List<FramebufferAttachment> Attachments { get; set; } 
}
