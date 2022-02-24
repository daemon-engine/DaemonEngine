namespace DaemonEngine.OpenGL.DllImport.Enums;

public enum GLCapabilities : uint
{
    None = 0,
    Blend = GLConstants.GL_BLEND,
    CullFace = GLConstants.GL_CULL_FACE,
    ScissorTest = GLConstants.GL_SCISSOR_TEST,
    StencilTest = GLConstants.GL_STENCIL_TEST,
    DepthTest = GLConstants.GL_DEPTH_TEST,
    LineSmooth = GLConstants.GL_LINE_SMOOTH
}
