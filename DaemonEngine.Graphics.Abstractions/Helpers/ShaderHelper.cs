using DaemonEngine.Extensions.OpenGL;
using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.Helpers;

public static class ShaderHelper
{
    public static uint ShaderDataTypeToOpenGLBaseType(ShaderDataType type)
    {
        return type switch
        {
            ShaderDataType.Float => GLConstants.GL_FLOAT,
            ShaderDataType.Float2 => GLConstants.GL_FLOAT,
            ShaderDataType.Float3 => GLConstants.GL_FLOAT,
            ShaderDataType.Float4 => GLConstants.GL_FLOAT,
            ShaderDataType.Int => GLConstants.GL_INT,
            ShaderDataType.Int2 => GLConstants.GL_INT,
            ShaderDataType.Int3 => GLConstants.GL_INT,
            ShaderDataType.Int4 => GLConstants.GL_INT,
            ShaderDataType.Mat3 => GLConstants.GL_FLOAT,
            ShaderDataType.Mat4 => GLConstants.GL_FLOAT,
            ShaderDataType.Bool => GLConstants.GL_BOOL,
            _ => 0,
        };
    }

    public static int ShaderDataTypeSize(ShaderDataType type)
    {
        return type switch
        {
            ShaderDataType.Float => 4,
            ShaderDataType.Float2 => 4 * 2,
            ShaderDataType.Float3 => 4 * 3,
            ShaderDataType.Float4 => 4 * 4,
            ShaderDataType.Mat3 => 4 * 3 * 3,
            ShaderDataType.Mat4 => 4 * 4 * 4,
            ShaderDataType.Int => 4,
            ShaderDataType.Int2 => 4 * 2,
            ShaderDataType.Int3 => 4 * 3,
            ShaderDataType.Int4 => 4 * 4,
            ShaderDataType.Bool => 1,
            _ => 0,
        };
    }
}
