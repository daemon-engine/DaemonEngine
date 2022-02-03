namespace DaemonEngine.Graphics.Renderer.Enums;

public enum ClearMask : uint
{
    None = 0,
    DepthBufferBit = 0x00000100,
    StencilBufferBit = 0x00000400,
    ColorBufferBit = 0x00004000
}

