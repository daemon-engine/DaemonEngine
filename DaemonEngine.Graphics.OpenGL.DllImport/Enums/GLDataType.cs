using DaemonEngine.OpenGL.DllImport;

namespace DaemonEngine.Graphics.OpenGL.DllImport.Enums;

public enum GLDataType : uint
{
	Byte			= GLConstants.GL_BYTE,
	UnsignedByte	= GLConstants.GL_UNSIGNED_BYTE,
	Short			= GLConstants.GL_SHORT,
	UnsignedShort	= GLConstants.GL_UNSIGNED_SHORT,
	Int				= GLConstants.GL_INT,
	UnsignedInt		= GLConstants.GL_UNSIGNED_INT,
	Float			= GLConstants.GL_FLOAT,
	Double			= GLConstants.GL_DOUBLE
}
