namespace DaemonEngine.Extensions.OpenGL.Enums;

public enum GLClearMask : uint
{
	None = 0,
	DepthBufferBit = GLConstants.GL_DEPTH_BUFFER_BIT,
	StencilBufferBit = GLConstants.GL_STENCIL_BUFFER_BIT,
	ColorBufferBit = GLConstants.GL_COLOR_BUFFER_BIT
}