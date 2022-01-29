namespace DaemonEngine.Extensions.OpenGL.Enums;

public enum GLShaderType : uint
{
    None = 0,
    VertexShader = GLConstants.GL_VERTEX_SHADER,
    FragmentShader = GLConstants.GL_FRAGMENT_SHADER,
    ComputeShader = GLConstants.GL_COMPUTE_SHADER
}