using DaemonEngine.OpenGL.DllImport;

namespace DaemonEngine.Graphics.OpenGL.DllImport.Enums;

public enum GLPolygonFace : uint
{
    Front = GLConstants.GL_FRONT,
    Back = GLConstants.GL_BACK,
    FrontAndBack = GLConstants.GL_FRONT_AND_BACK
}
