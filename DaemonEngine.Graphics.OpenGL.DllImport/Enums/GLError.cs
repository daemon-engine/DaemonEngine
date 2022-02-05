using DaemonEngine.OpenGL.DllImport;

namespace DaemonEngine.Graphics.OpenGL.DllImport.Enums;

public enum GLError : uint
{
    NoError = GLConstants.GL_NO_ERROR,
    InvalidEnum = GLConstants.GL_INVALID_ENUM,
    InvalidValue = GLConstants.GL_INVALID_VALUE,
    InvalidOperation = GLConstants.GL_INVALID_OPERATION,
    InvalidFramebufferOperation = GLConstants.GL_INVALID_FRAMEBUFFER_OPERATION,
    OutOfMemory = GLConstants.GL_OUT_OF_MEMORY,
    StackUnderflow = GLConstants.GL_STACK_UNDERFLOW,
    StackOverflow = GLConstants.GL_STACK_OVERFLOW
}
