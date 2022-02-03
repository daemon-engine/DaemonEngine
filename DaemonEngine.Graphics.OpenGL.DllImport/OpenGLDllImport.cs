using System.Runtime.InteropServices;

namespace DaemonEngine.OpenGL.DllImport;

internal class OpenGLDllImport
{
	#region Delegates

	private delegate void glDrawRangeElementsDelegate(uint mode, uint start, uint end, int count, uint type, IntPtr indices);
	private delegate void glTexImage3DDelegate(uint target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint type, IntPtr pixels);
	private delegate void glTexSubImage3DDelegate(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, IntPtr pixels);
	private delegate void glCopyTexSubImage3DDelegate(uint target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height);
	private delegate void glActiveTextureDelegate(uint texture);
	private delegate void glSampleCoverageDelegate(float value, bool invert);
	private delegate void glCompressedTexImage3DDelegate(uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, IntPtr data);
	private delegate void glCompressedTexImage2DDelegate(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, IntPtr data);
	private delegate void glCompressedTexImage1DDelegate(uint target, int level, uint internalformat, int width, int border, int imageSize, IntPtr data);
	private delegate void glCompressedTexSubImage3DDelegate(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, IntPtr data);
	private delegate void glCompressedTexSubImage2DDelegate(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, IntPtr data);
	private delegate void glCompressedTexSubImage1DDelegate(uint target, int level, int xoffset, int width, uint format, int imageSize, IntPtr data);
	private delegate void glGetCompressedTexImageDelegate(uint target, int level, IntPtr img);
	private delegate void glClientActiveTextureDelegate(uint texture);
	private delegate void glMultiTexCoord1dDelegate(uint target, double s);
	private delegate void glMultiTexCoord1dvDelegate(uint target, double[] v);
	private delegate void glMultiTexCoord1fDelegate(uint target, float s);
	private delegate void glMultiTexCoord1fvDelegate(uint target, float[] v);
	private delegate void glMultiTexCoord1iDelegate(uint target, int s);
	private delegate void glMultiTexCoord1ivDelegate(uint target, int[] v);
	private delegate void glMultiTexCoord1sDelegate(uint target, short s);
	private delegate void glMultiTexCoord1svDelegate(uint target, short[] v);
	private delegate void glMultiTexCoord2dDelegate(uint target, double s, double t);
	private delegate void glMultiTexCoord2dvDelegate(uint target, double[] v);
	private delegate void glMultiTexCoord2fDelegate(uint target, float s, float t);
	private delegate void glMultiTexCoord2fvDelegate(uint target, float[] v);
	private delegate void glMultiTexCoord2iDelegate(uint target, int s, int t);
	private delegate void glMultiTexCoord2ivDelegate(uint target, int[] v);
	private delegate void glMultiTexCoord2sDelegate(uint target, short s, short t);
	private delegate void glMultiTexCoord2svDelegate(uint target, short[] v);
	private delegate void glMultiTexCoord3dDelegate(uint target, double s, double t, double r);
	private delegate void glMultiTexCoord3dvDelegate(uint target, double[] v);
	private delegate void glMultiTexCoord3fDelegate(uint target, float s, float t, float r);
	private delegate void glMultiTexCoord3fvDelegate(uint target, float[] v);
	private delegate void glMultiTexCoord3iDelegate(uint target, int s, int t, int r);
	private delegate void glMultiTexCoord3ivDelegate(uint target, int[] v);
	private delegate void glMultiTexCoord3sDelegate(uint target, short s, short t, short r);
	private delegate void glMultiTexCoord3svDelegate(uint target, short[] v);
	private delegate void glMultiTexCoord4dDelegate(uint target, double s, double t, double r, double q);
	private delegate void glMultiTexCoord4dvDelegate(uint target, double[] v);
	private delegate void glMultiTexCoord4fDelegate(uint target, float s, float t, float r, float q);
	private delegate void glMultiTexCoord4fvDelegate(uint target, float[] v);
	private delegate void glMultiTexCoord4iDelegate(uint target, int s, int t, int r, int q);
	private delegate void glMultiTexCoord4ivDelegate(uint target, int[] v);
	private delegate void glMultiTexCoord4sDelegate(uint target, short s, short t, short r, short q);
	private delegate void glMultiTexCoord4svDelegate(uint target, short[] v);
	private delegate void glLoadTransposeMatrixfDelegate(float[] m);
	private delegate void glLoadTransposeMatrixdDelegate(double[] m);
	private delegate void glMultTransposeMatrixfDelegate(float[] m);
	private delegate void glMultTransposeMatrixdDelegate(double[] m);
	private delegate void glBlendFuncSeparateDelegate(uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha);
	private delegate void glMultiDrawArraysDelegate(uint mode, int[] first, int[] count, int drawcount);
	private delegate void glMultiDrawElementsDelegate(uint mode, int[] count, uint type, IntPtr indices, int drawcount);
	private delegate void glPointParameterfDelegate(uint pname, float param);
	private delegate void glPointParameterfvDelegate(uint pname, float[] @params);
	private delegate void glPointParameteriDelegate(uint pname, int param);
	private delegate void glPointParameterivDelegate(uint pname, int[] @params);
	private delegate void glFogCoordfDelegate(float coord);
	private delegate void glFogCoordfvDelegate(float[] coord);
	private delegate void glFogCoorddDelegate(double coord);
	private delegate void glFogCoorddvDelegate(double[] coord);
	private delegate void glFogCoordPointerDelegate(uint type, int stride, IntPtr pointer);
	private delegate void glSecondaryColor3bDelegate(byte red, byte green, byte blue);
	private delegate void glSecondaryColor3bvDelegate(byte[] v);
	private delegate void glSecondaryColor3dDelegate(double red, double green, double blue);
	private delegate void glSecondaryColor3dvDelegate(double[] v);
	private delegate void glSecondaryColor3fDelegate(float red, float green, float blue);
	private delegate void glSecondaryColor3fvDelegate(float[] v);
	private delegate void glSecondaryColor3iDelegate(int red, int green, int blue);
	private delegate void glSecondaryColor3ivDelegate(int[] v);
	private delegate void glSecondaryColor3sDelegate(short red, short green, short blue);
	private delegate void glSecondaryColor3svDelegate(short[] v);
	private delegate void glSecondaryColor3ubDelegate(byte red, byte green, byte blue);
	private delegate void glSecondaryColor3ubvDelegate(byte[] v);
	private delegate void glSecondaryColor3uiDelegate(uint red, uint green, uint blue);
	private delegate void glSecondaryColor3uivDelegate(uint[] v);
	private delegate void glSecondaryColor3usDelegate(ushort red, ushort green, ushort blue);
	private delegate void glSecondaryColor3usvDelegate(ushort[] v);
	private delegate void glSecondaryColorPointerDelegate(int size, uint type, int stride, IntPtr pointer);
	private delegate void glWindowPos2dDelegate(double x, double y);
	private delegate void glWindowPos2dvDelegate(double[] v);
	private delegate void glWindowPos2fDelegate(float x, float y);
	private delegate void glWindowPos2fvDelegate(float[] v);
	private delegate void glWindowPos2iDelegate(int x, int y);
	private delegate void glWindowPos2ivDelegate(int[] v);
	private delegate void glWindowPos2sDelegate(short x, short y);
	private delegate void glWindowPos2svDelegate(short[] v);
	private delegate void glWindowPos3dDelegate(double x, double y, double z);
	private delegate void glWindowPos3dvDelegate(double[] v);
	private delegate void glWindowPos3fDelegate(float x, float y, float z);
	private delegate void glWindowPos3fvDelegate(float[] v);
	private delegate void glWindowPos3iDelegate(int x, int y, int z);
	private delegate void glWindowPos3ivDelegate(int[] v);
	private delegate void glWindowPos3sDelegate(short x, short y, short z);
	private delegate void glWindowPos3svDelegate(short[] v);
	private delegate void glBlendColorDelegate(float red, float green, float blue, float alpha);
	private delegate void glBlendEquationDelegate(uint mode);
	private delegate void glGenQueriesDelegate(int n, uint[] ids);
	private delegate void glDeleteQueriesDelegate(int n, uint[] ids);
	private delegate bool glIsQueryDelegate(uint id);
	private delegate void glBeginQueryDelegate(uint target, uint id);
	private delegate void glEndQueryDelegate(uint target);
	private delegate void glGetQueryivDelegate(uint target, uint pname, int[] @params);
	private delegate void glGetQueryObjectivDelegate(uint id, uint pname, int[] @params);
	private delegate void glGetQueryObjectuivDelegate(uint id, uint pname, uint[] @params);
	private delegate void glBindBufferDelegate(uint target, uint buffer);
	private delegate void glDeleteBuffersDelegate(int n, uint[] buffers);
	private delegate void glGenBuffersDelegate(int n, uint[] buffers);
	private delegate bool glIsBufferDelegate(uint buffer);
	private delegate void glBufferDataFloatDelegate(uint target, int size, float[] data, uint usage);
	private delegate void glBufferDataUintDelegate(uint target, int size, uint[] data, uint usage);
	private delegate void glBufferDataIntDelegate(uint target, int size, int[] data, uint usage);
	private delegate void glBufferDataDoubleDelegate(uint target, int size, double[] data, uint usage);
	private delegate void glBufferSubDataDelegate(uint target, int offset, int size, float[] data);
	private delegate void glGetBufferSubDataDelegate(uint target, IntPtr offset, IntPtr size, IntPtr data);
	private delegate void glMapBufferDelegate(uint target, uint access);
	private delegate bool glUnmapBufferDelegate(uint target);
	private delegate void glGetBufferParameterivDelegate(uint target, uint pname, int[] @params);
	private delegate void glGetBufferPointervDelegate(uint target, uint pname, IntPtr @params);
	private delegate void glBlendEquationSeparateDelegate(uint modeRGB, uint modeAlpha);
	private delegate void glDrawBuffersDelegate(int n, uint[] bufs);
	private delegate void glStencilOpSeparateDelegate(uint face, uint sfail, uint dpfail, uint dppass);
	private delegate void glStencilFuncSeparateDelegate(uint face, uint func, int @ref, uint mask);
	private delegate void glStencilMaskSeparateDelegate(uint face, uint mask);
	private delegate void glAttachShaderDelegate(uint program, uint shader);
	private delegate void glBindAttribLocationDelegate(uint program, uint index, string[] name);
	private delegate void glCompileShaderDelegate(uint shader);
	private delegate uint glCreateProgramDelegate();
	private delegate uint glCreateShaderDelegate(uint type);
	private delegate void glDeleteProgramDelegate(uint program);
	private delegate void glDeleteShaderDelegate(uint shader);
	private delegate void glDetachShaderDelegate(uint program, uint shader);
	private delegate void glDisableVertexAttribArrayDelegate(uint index);
	private delegate void glEnableVertexAttribArrayDelegate(uint index);
	private delegate void glGetActiveAttribDelegate(uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string[] name);
	private delegate void glGetActiveUniformDelegate(uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string[] name);
	private delegate void glGetAttachedShadersDelegate(uint program, int maxCount, int[] count, uint[] shaders);
	private delegate uint glGetAttribLocationDelegate(uint program, string name);
	private delegate void glGetProgramivDelegate(uint program, uint pname, int[] @params);
	private delegate void glGetProgramInfoLogDelegate(uint program, int bufSize, ref int length, byte[] infoLog);
	private delegate void glGetShaderivDelegate(uint shader, uint pname, ref int @params);
	private delegate void glGetShaderInfoLogDelegate(uint shader, int bufSize, ref int length, byte[] infoLog);
	private delegate void glGetShaderSourceDelegate(uint shader, int bufSize, int[] length, char[] source);
	private delegate uint glGetUniformLocationDelegate(uint program, string name);
	private delegate void glGetUniformfvDelegate(uint program, uint location, float[] @params);
	private delegate void glGetUniformivDelegate(uint program, uint location, int[] @params);
	private delegate void glGetVertexAttribdvDelegate(uint index, uint pname, double[] @params);
	private delegate void glGetVertexAttribfvDelegate(uint index, uint pname, float[] @params);
	private delegate void glGetVertexAttribivDelegate(uint index, uint pname, int[] @params);
	private delegate void glGetVertexAttribPointervDelegate(uint index, uint pname, IntPtr pointer);
	private delegate bool glIsProgramDelegate(uint program);
	private delegate bool glIsShaderDelegate(uint shader);
	private delegate void glLinkProgramDelegate(uint program);
	[UnmanagedFunctionPointer(CallingConvention.StdCall, ThrowOnUnmappableChar = true)]
	private delegate void glShaderSourceDelegate(uint shader, int count, string[] @string, int[] length);
	private delegate void glUseProgramDelegate(uint program);
	private delegate void glUniform1fDelegate(uint location, float v0);
	private delegate void glUniform2fDelegate(uint location, float v0, float v1);
	private delegate void glUniform3fDelegate(uint location, float v0, float v1, float v2);
	private delegate void glUniform4fDelegate(uint location, float v0, float v1, float v2, float v3);
	private delegate void glUniform1iDelegate(uint location, int v0);
	private delegate void glUniform2iDelegate(uint location, int v0, int v1);
	private delegate void glUniform3iDelegate(uint location, int v0, int v1, int v2);
	private delegate void glUniform4iDelegate(uint location, int v0, int v1, int v2, int v3);
	private delegate void glUniform1fvDelegate(uint location, int count, float[] value);
	private delegate void glUniform2fvDelegate(uint location, int count, float[] value);
	private delegate void glUniform3fvDelegate(uint location, int count, float[] value);
	private delegate void glUniform4fvDelegate(uint location, int count, float[] value);
	private delegate void glUniform1ivDelegate(uint location, int count, int[] value);
	private delegate void glUniform2ivDelegate(uint location, int count, int[] value);
	private delegate void glUniform3ivDelegate(uint location, int count, int[] value);
	private delegate void glUniform4ivDelegate(uint location, int count, int[] value);
	private delegate void glUniformMatrix2fvDelegate(uint location, int count, bool transpose, float[] value);
	private delegate void glUniformMatrix3fvDelegate(uint location, int count, bool transpose, float[] value);
	private delegate void glUniformMatrix4fvDelegate(uint location, int count, bool transpose, float[] value);
	private delegate void glValidateProgramDelegate(uint program);
	private delegate void glVertexAttrib1dDelegate(uint index, double x);
	private delegate void glVertexAttrib1dvDelegate(uint index, double[] v);
	private delegate void glVertexAttrib1fDelegate(uint index, float x);
	private delegate void glVertexAttrib1fvDelegate(uint index, float[] v);
	private delegate void glVertexAttrib1sDelegate(uint index, short x);
	private delegate void glVertexAttrib1svDelegate(uint index, short[] v);
	private delegate void glVertexAttrib2dDelegate(uint index, double x, double y);
	private delegate void glVertexAttrib2dvDelegate(uint index, double[] v);
	private delegate void glVertexAttrib2fDelegate(uint index, float x, float y);
	private delegate void glVertexAttrib2fvDelegate(uint index, float[] v);
	private delegate void glVertexAttrib2sDelegate(uint index, short x, short y);
	private delegate void glVertexAttrib2svDelegate(uint index, short[] v);
	private delegate void glVertexAttrib3dDelegate(uint index, double x, double y, double z);
	private delegate void glVertexAttrib3dvDelegate(uint index, double[] v);
	private delegate void glVertexAttrib3fDelegate(uint index, float x, float y, float z);
	private delegate void glVertexAttrib3fvDelegate(uint index, float[] v);
	private delegate void glVertexAttrib3sDelegate(uint index, short x, short y, short z);
	private delegate void glVertexAttrib3svDelegate(uint index, short[] v);
	private delegate void glVertexAttrib4NbvDelegate(uint index, byte[] v);
	private delegate void glVertexAttrib4NivDelegate(uint index, int[] v);
	private delegate void glVertexAttrib4NsvDelegate(uint index, short[] v);
	private delegate void glVertexAttrib4NubDelegate(uint index, byte x, byte y, byte z, byte w);
	private delegate void glVertexAttrib4NubvDelegate(uint index, byte[] v);
	private delegate void glVertexAttrib4NuivDelegate(uint index, uint[] v);
	private delegate void glVertexAttrib4NusvDelegate(uint index, ushort[] v);
	private delegate void glVertexAttrib4bvDelegate(uint index, byte[] v);
	private delegate void glVertexAttrib4dDelegate(uint index, double x, double y, double z, double w);
	private delegate void glVertexAttrib4dvDelegate(uint index, double[] v);
	private delegate void glVertexAttrib4fDelegate(uint index, float x, float y, float z, float w);
	private delegate void glVertexAttrib4fvDelegate(uint index, float[] v);
	private delegate void glVertexAttrib4ivDelegate(uint index, int[] v);
	private delegate void glVertexAttrib4sDelegate(uint index, short x, short y, short z, short w);
	private delegate void glVertexAttrib4svDelegate(uint index, short[] v);
	private delegate void glVertexAttrib4ubvDelegate(uint index, byte[] v);
	private delegate void glVertexAttrib4uivDelegate(uint index, uint[] v);
	private delegate void glVertexAttrib4usvDelegate(uint index, ushort[] v);
	private delegate void glVertexAttribPointerDelegate(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer);
	private delegate void glReadPixelsDelegate(float x, float y, float width, float height, uint format, uint type, IntPtr pixels);
	private delegate void glUniformMatrix2x3fvDelegate(uint location, int count, bool transpose, float[] value);
	private delegate void glUniformMatrix3x2fvDelegate(uint location, int count, bool transpose, float[] value);
	private delegate void glUniformMatrix2x4fvDelegate(uint location, int count, bool transpose, float[] value);
	private delegate void glUniformMatrix4x2fvDelegate(uint location, int count, bool transpose, float[] value);
	private delegate void glUniformMatrix3x4fvDelegate(uint location, int count, bool transpose, float[] value);
	private delegate void glUniformMatrix4x3fvDelegate(uint location, int count, bool transpose, float[] value);
	private delegate void glColorMaskiDelegate(uint index, bool r, bool g, bool b, bool a);
	private delegate void glGetBooleani_vDelegate(uint target, uint index, bool[] data);
	private delegate void glGetIntegeri_vDelegate(uint target, uint index, int[] data);
	private delegate void glEnableiDelegate(uint target, uint index);
	private delegate void glDisableiDelegate(uint target, uint index);
	private delegate bool glIsEnablediDelegate(uint target, uint index);
	private delegate void glBeginTransformFeedbackDelegate(uint primitiveMode);
	private delegate void glEndTransformFeedbackDelegate();
	private delegate void glBindBufferRangeDelegate(uint target, uint index, uint buffer, IntPtr offset, IntPtr size);
	private delegate void glBindBufferBaseDelegate(uint target, uint index, uint buffer);
	private delegate void glTransformFeedbackVaryingsDelegate(uint program, int count, char[] varyings, uint bufferMode);
	private delegate void glGetTransformFeedbackVaryingDelegate(uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, char[] name);
	private delegate void glClampColorDelegate(uint target, uint clamp);
	private delegate void glBeginConditionalRenderDelegate(uint id, uint mode);
	private delegate void glEndConditionalRenderDelegate();
	private delegate void glVertexAttribIPointerDelegate(uint index, int size, uint type, int stride, IntPtr pointer);
	private delegate void glGetVertexAttribIivDelegate(uint index, uint pname, int[] @params);
	private delegate void glGetVertexAttribIuivDelegate(uint index, uint pname, uint[] @params);
	private delegate void glVertexAttribI1iDelegate(uint index, int x);
	private delegate void glVertexAttribI2iDelegate(uint index, int x, int y);
	private delegate void glVertexAttribI3iDelegate(uint index, int x, int y, int z);
	private delegate void glVertexAttribI4iDelegate(uint index, int x, int y, int z, int w);
	private delegate void glVertexAttribI1uiDelegate(uint index, uint x);
	private delegate void glVertexAttribI2uiDelegate(uint index, uint x, uint y);
	private delegate void glVertexAttribI3uiDelegate(uint index, uint x, uint y, uint z);
	private delegate void glVertexAttribI4uiDelegate(uint index, uint x, uint y, uint z, uint w);
	private delegate void glVertexAttribI1ivDelegate(uint index, int[] v);
	private delegate void glVertexAttribI2ivDelegate(uint index, int[] v);
	private delegate void glVertexAttribI3ivDelegate(uint index, int[] v);
	private delegate void glVertexAttribI4ivDelegate(uint index, int[] v);
	private delegate void glVertexAttribI1uivDelegate(uint index, uint[] v);
	private delegate void glVertexAttribI2uivDelegate(uint index, uint[] v);
	private delegate void glVertexAttribI3uivDelegate(uint index, uint[] v);
	private delegate void glVertexAttribI4uivDelegate(uint index, uint[] v);
	private delegate void glVertexAttribI4bvDelegate(uint index, byte[] v);
	private delegate void glVertexAttribI4svDelegate(uint index, short[] v);
	private delegate void glVertexAttribI4ubvDelegate(uint index, byte[] v);
	private delegate void glVertexAttribI4usvDelegate(uint index, ushort[] v);
	private delegate void glGetUniformuivDelegate(uint program, uint location, uint[] @params);
	private delegate void glBindFragDataLocationDelegate(uint program, uint color, char[] name);
	private delegate int glGetFragDataLocationDelegate(uint program, char[] name);
	private delegate void glUniform1uiDelegate(uint location, uint v0);
	private delegate void glUniform2uiDelegate(uint location, uint v0, uint v1);
	private delegate void glUniform3uiDelegate(uint location, uint v0, uint v1, uint v2);
	private delegate void glUniform4uiDelegate(uint location, uint v0, uint v1, uint v2, uint v3);
	private delegate void glUniform1uivDelegate(uint location, int count, uint[] value);
	private delegate void glUniform2uivDelegate(uint location, int count, uint[] value);
	private delegate void glUniform3uivDelegate(uint location, int count, uint[] value);
	private delegate void glUniform4uivDelegate(uint location, int count, uint[] value);
	private delegate void glTexParameterIivDelegate(uint target, uint pname, int[] @params);
	private delegate void glTexParameterIuivDelegate(uint target, uint pname, uint[] @params);
	private delegate void glGetTexParameterIivDelegate(uint target, uint pname, int[] @params);
	private delegate void glGetTexParameterIuivDelegate(uint target, uint pname, uint[] @params);
	private delegate void glClearBufferivDelegate(uint buffer, int drawbuffer, int[] value);
	private delegate void glClearBufferuivDelegate(uint buffer, int drawbuffer, uint[] value);
	private delegate void glClearBufferfvDelegate(uint buffer, int drawbuffer, float[] value);
	private delegate void glClearBufferfiDelegate(uint buffer, int drawbuffer, float depth, int stencil);
	private delegate bool glIsRenderbufferDelegate(uint renderbuffer);
	private delegate void glBindRenderbufferDelegate(uint target, uint renderbuffer);
	private delegate void glDeleteRenderbuffersDelegate(int n, uint[] renderbuffers);
	private delegate void glGenRenderbuffersDelegate(int n, uint[] renderbuffers);
	private delegate void glRenderbufferStorageDelegate(uint target, uint internalformat, int width, int height);
	private delegate void glGetRenderbufferParameterivDelegate(uint target, uint pname, int[] @params);
	private delegate bool glIsFramebufferDelegate(uint framebuffer);
	private delegate void glBindFramebufferDelegate(uint target, uint framebuffer);
	private delegate void glDeleteFramebuffersDelegate(int n, uint[] framebuffers);
	private delegate void glGenFramebuffersDelegate(int n, uint[] framebuffers);
	private delegate uint glCheckFramebufferStatusDelegate(uint target);
	private delegate void glFramebufferTexture1DDelegate(uint target, uint attachment, uint textarget, uint texture, int level);
	private delegate void glFramebufferTexture2DDelegate(uint target, uint attachment, uint textarget, uint texture, int level);
	private delegate void glFramebufferTexture3DDelegate(uint target, uint attachment, uint textarget, uint texture, int level, int zoffset);
	private delegate void glFramebufferRenderbufferDelegate(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer);
	private delegate void glGetFramebufferAttachmentParameterivDelegate(uint target, uint attachment, uint pname, int[] @params);
	private delegate void glGenerateMipmapDelegate(uint target);
	private delegate void glBlitFramebufferDelegate(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter);
	private delegate void glRenderbufferStorageMultisampleDelegate(uint target, int samples, uint internalformat, int width, int height);
	private delegate void glFramebufferTextureLayerDelegate(uint target, uint attachment, uint texture, int level, int layer);
	private delegate void glMapBufferRangeDelegate(uint target, IntPtr offset, IntPtr length, uint access);
	private delegate void glFlushMappedBufferRangeDelegate(uint target, IntPtr offset, IntPtr length);
	private delegate void glBindVertexArrayDelegate(uint array);
	private delegate void glDeleteVertexArraysDelegate(int n, uint[] arrays);
	private delegate void glGenVertexArraysDelegate(int n, uint[] arrays);
	private delegate bool glIsVertexArrayDelegate(uint array);
	private delegate void glDrawElementsBaseVertexDelegate(uint mode, int count, uint type, IntPtr indices, int basevertex);
	private delegate void glDrawRangeElementsBaseVertexDelegate(uint mode, uint start, uint end, int count, uint type, IntPtr indices, int basevertex);
	private delegate void glDrawElementsInstancedBaseVertexDelegate(uint mode, int count, uint type, IntPtr indices, int instancecount, int basevertex);
	private delegate void glMultiDrawElementsBaseVertexDelegate(uint mode, int[] count, uint type, IntPtr indices, int drawcount, int[] basevertex);
	private delegate void glProvokingVertexDelegate(uint mode);
	private delegate IntPtr glFenceSyncDelegate(uint condition, uint flags);
	private delegate bool glIsSyncDelegate(IntPtr sync);
	private delegate void glDeleteSyncDelegate(IntPtr sync);
	private delegate uint glClientWaitSyncDelegate(IntPtr sync, uint flags, UInt64 timeout);
	private delegate void glWaitSyncDelegate(IntPtr sync, uint flags, UInt64 timeout);
	private delegate void glGetInteger64vDelegate(uint pname, Int64[] data);
	private delegate void glGetSyncivDelegate(IntPtr sync, uint pname, int bufSize, int[] length, int[] values);
	private delegate void glGetInteger64i_vDelegate(uint target, uint index, Int64[] data);
	private delegate void glGetBufferParameteri64vDelegate(uint target, uint pname, Int64[] @params);
	private delegate void glFramebufferTextureDelegate(uint target, uint attachment, uint texture, int level);
	private delegate void glTexImage2DMultisampleDelegate(uint target, int samples, uint internalformat, int width, int height, bool fixedsamplelocations);
	private delegate void glTexImage3DMultisampleDelegate(uint target, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations);
	private delegate void glGetMultisamplefvDelegate(uint pname, uint index, float[] val);
	private delegate void glSampleMaskiDelegate(uint maskNumber, uint mask);
	private delegate void glDrawArraysInstancedDelegate(uint mode, int first, int count, int instancecount);
	private delegate void glDrawElementsInstancedDelegate(uint mode, int count, uint type, IntPtr indices, int instancecount);
	private delegate void glTexBufferDelegate(uint target, uint internalformat, uint buffer);
	private delegate void glPrimitiveRestartIndexDelegate(uint index);
	private delegate void glCopyBufferSubDataDelegate(uint readTarget, uint writeTarget, IntPtr readOffset, IntPtr writeOffset, IntPtr size);
	private delegate void glGetUniformIndicesDelegate(uint program, int uniformCount, char[] uniformNames, uint[] uniformIndices);
	private delegate void glGetActiveUniformsivDelegate(uint program, int uniformCount, uint[] uniformIndices, uint pname, int[] @params);
	private delegate void glGetActiveUniformNameDelegate(uint program, uint uniformIndex, int bufSize, int[] length, char[] uniformName);
	private delegate uint glGetUniformBlockIndexDelegate(uint program, char[] uniformBlockName);
	private delegate void glGetActiveUniformBlockivDelegate(uint program, uint uniformBlockIndex, uint pname, int[] @params);
	private delegate void glGetActiveUniformBlockNameDelegate(uint program, uint uniformBlockIndex, int bufSize, int[] length, char[] uniformBlockName);
	private delegate void glUniformBlockBindingDelegate(uint program, uint uniformBlockIndex, uint uniformBlockBinding);
	private delegate void glBindFragDataLocationIndexedDelegate(uint program, uint colorNumber, uint index, char[] name);
	private delegate int glGetFragDataIndexDelegate(uint program, char[] name);
	private delegate void glGenSamplersDelegate(int count, uint[] samplers);
	private delegate void glDeleteSamplersDelegate(int count, uint[] samplers);
	private delegate bool glIsSamplerDelegate(uint sampler);
	private delegate void glBindSamplerDelegate(uint unit, uint sampler);
	private delegate void glSamplerParameteriDelegate(uint sampler, uint pname, int param);
	private delegate void glSamplerParameterivDelegate(uint sampler, uint pname, int[] param);
	private delegate void glSamplerParameterfDelegate(uint sampler, uint pname, float param);
	private delegate void glSamplerParameterfvDelegate(uint sampler, uint pname, float[] param);
	private delegate void glSamplerParameterIivDelegate(uint sampler, uint pname, int[] param);
	private delegate void glSamplerParameterIuivDelegate(uint sampler, uint pname, uint[] param);
	private delegate void glGetSamplerParameterivDelegate(uint sampler, uint pname, int[] @params);
	private delegate void glGetSamplerParameterIivDelegate(uint sampler, uint pname, int[] @params);
	private delegate void glGetSamplerParameterfvDelegate(uint sampler, uint pname, float[] @params);
	private delegate void glGetSamplerParameterIuivDelegate(uint sampler, uint pname, uint[] @params);
	private delegate void glQueryCounterDelegate(uint id, uint target);
	private delegate void glGetQueryObjecti64vDelegate(uint id, uint pname, Int64[] @params);
	private delegate void glGetQueryObjectui64vDelegate(uint id, uint pname, UInt64[] @params);
	private delegate void glVertexAttribDivisorDelegate(uint index, uint divisor);
	private delegate void glVertexAttribP1uiDelegate(uint index, uint type, bool normalized, uint value);
	private delegate void glVertexAttribP1uivDelegate(uint index, uint type, bool normalized, uint[] value);
	private delegate void glVertexAttribP2uiDelegate(uint index, uint type, bool normalized, uint value);
	private delegate void glVertexAttribP2uivDelegate(uint index, uint type, bool normalized, uint[] value);
	private delegate void glVertexAttribP3uiDelegate(uint index, uint type, bool normalized, uint value);
	private delegate void glVertexAttribP3uivDelegate(uint index, uint type, bool normalized, uint[] value);
	private delegate void glVertexAttribP4uiDelegate(uint index, uint type, bool normalized, uint value);
	private delegate void glVertexAttribP4uivDelegate(uint index, uint type, bool normalized, uint[] value);
	private delegate void glVertexP2uiDelegate(uint type, uint value);
	private delegate void glVertexP2uivDelegate(uint type, uint[] value);
	private delegate void glVertexP3uiDelegate(uint type, uint value);
	private delegate void glVertexP3uivDelegate(uint type, uint[] value);
	private delegate void glVertexP4uiDelegate(uint type, uint value);
	private delegate void glVertexP4uivDelegate(uint type, uint[] value);
	private delegate void glTexCoordP1uiDelegate(uint type, uint coords);
	private delegate void glTexCoordP1uivDelegate(uint type, uint[] coords);
	private delegate void glTexCoordP2uiDelegate(uint type, uint coords);
	private delegate void glTexCoordP2uivDelegate(uint type, uint[] coords);
	private delegate void glTexCoordP3uiDelegate(uint type, uint coords);
	private delegate void glTexCoordP3uivDelegate(uint type, uint[] coords);
	private delegate void glTexCoordP4uiDelegate(uint type, uint coords);
	private delegate void glTexCoordP4uivDelegate(uint type, uint[] coords);
	private delegate void glMultiTexCoordP1uiDelegate(uint texture, uint type, uint coords);
	private delegate void glMultiTexCoordP1uivDelegate(uint texture, uint type, uint[] coords);
	private delegate void glMultiTexCoordP2uiDelegate(uint texture, uint type, uint coords);
	private delegate void glMultiTexCoordP2uivDelegate(uint texture, uint type, uint[] coords);
	private delegate void glMultiTexCoordP3uiDelegate(uint texture, uint type, uint coords);
	private delegate void glMultiTexCoordP3uivDelegate(uint texture, uint type, uint[] coords);
	private delegate void glMultiTexCoordP4uiDelegate(uint texture, uint type, uint coords);
	private delegate void glMultiTexCoordP4uivDelegate(uint texture, uint type, uint[] coords);
	private delegate void glNormalP3uiDelegate(uint type, uint coords);
	private delegate void glNormalP3uivDelegate(uint type, uint[] coords);
	private delegate void glColorP3uiDelegate(uint type, uint color);
	private delegate void glColorP3uivDelegate(uint type, uint[] color);
	private delegate void glColorP4uiDelegate(uint type, uint color);
	private delegate void glColorP4uivDelegate(uint type, uint[] color);
	private delegate void glSecondaryColorP3uiDelegate(uint type, uint color);
	private delegate void glSecondaryColorP3uivDelegate(uint type, uint[] color);
	private delegate void glMinSampleShadingDelegate(float value);
	private delegate void glBlendEquationiDelegate(uint buf, uint mode);
	private delegate void glBlendEquationSeparateiDelegate(uint buf, uint modeRGB, uint modeAlpha);
	private delegate void glBlendFunciDelegate(uint buf, uint src, uint dst);
	private delegate void glBlendFuncSeparateiDelegate(uint buf, uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha);
	private delegate void glDrawArraysIndirectDelegate(uint mode, IntPtr indirect);
	private delegate void glDrawElementsIndirectDelegate(uint mode, uint type, IntPtr indirect);
	private delegate void glUniform1dDelegate(uint location, double x);
	private delegate void glUniform2dDelegate(uint location, double x, double y);
	private delegate void glUniform3dDelegate(uint location, double x, double y, double z);
	private delegate void glUniform4dDelegate(uint location, double x, double y, double z, double w);
	private delegate void glUniform1dvDelegate(uint location, int count, double[] value);
	private delegate void glUniform2dvDelegate(uint location, int count, double[] value);
	private delegate void glUniform3dvDelegate(uint location, int count, double[] value);
	private delegate void glUniform4dvDelegate(uint location, int count, double[] value);
	private delegate void glUniformMatrix2dvDelegate(uint location, int count, bool transpose, double[] value);
	private delegate void glUniformMatrix3dvDelegate(uint location, int count, bool transpose, double[] value);
	private delegate void glUniformMatrix4dvDelegate(uint location, int count, bool transpose, double[] value);
	private delegate void glUniformMatrix2x3dvDelegate(uint location, int count, bool transpose, double[] value);
	private delegate void glUniformMatrix2x4dvDelegate(uint location, int count, bool transpose, double[] value);
	private delegate void glUniformMatrix3x2dvDelegate(uint location, int count, bool transpose, double[] value);
	private delegate void glUniformMatrix3x4dvDelegate(uint location, int count, bool transpose, double[] value);
	private delegate void glUniformMatrix4x2dvDelegate(uint location, int count, bool transpose, double[] value);
	private delegate void glUniformMatrix4x3dvDelegate(uint location, int count, bool transpose, double[] value);
	private delegate void glGetUniformdvDelegate(uint program, uint location, double[] @params);
	private delegate int glGetSubroutineUniformLocationDelegate(uint program, uint shadertype, char[] name);
	private delegate uint glGetSubroutineIndexDelegate(uint program, uint shadertype, char[] name);
	private delegate void glGetActiveSubroutineUniformivDelegate(uint program, uint shadertype, uint index, uint pname, int[] values);
	private delegate void glGetActiveSubroutineUniformNameDelegate(uint program, uint shadertype, uint index, int bufsize, int[] length, char[] name);
	private delegate void glGetActiveSubroutineNameDelegate(uint program, uint shadertype, uint index, int bufsize, int[] length, char[] name);
	private delegate void glUniformSubroutinesuivDelegate(uint shadertype, int count, uint[] indices);
	private delegate void glGetUniformSubroutineuivDelegate(uint shadertype, uint location, uint[] @params);
	private delegate void glGetProgramStageivDelegate(uint program, uint shadertype, uint pname, int[] values);
	private delegate void glPatchParameteriDelegate(uint pname, int value);
	private delegate void glPatchParameterfvDelegate(uint pname, float[] values);
	private delegate void glBindTransformFeedbackDelegate(uint target, uint id);
	private delegate void glDeleteTransformFeedbacksDelegate(int n, uint[] ids);
	private delegate void glGenTransformFeedbacksDelegate(int n, uint[] ids);
	private delegate bool glIsTransformFeedbackDelegate(uint id);
	private delegate void glPauseTransformFeedbackDelegate();
	private delegate void glResumeTransformFeedbackDelegate();
	private delegate void glDrawTransformFeedbackDelegate(uint mode, uint id);
	private delegate void glDrawTransformFeedbackStreamDelegate(uint mode, uint id, uint stream);
	private delegate void glBeginQueryIndexedDelegate(uint target, uint index, uint id);
	private delegate void glEndQueryIndexedDelegate(uint target, uint index);
	private delegate void glGetQueryIndexedivDelegate(uint target, uint index, uint pname, int[] @params);
	private delegate void glReleaseShaderCompilerDelegate();
	private delegate void glShaderBinaryDelegate(int count, uint[] shaders, uint binaryformat, IntPtr binary, int length);
	private delegate void glGetShaderPrecisionFormatDelegate(uint shadertype, uint precisiontype, int[] range, int[] precision);
	private delegate void glDepthRangefDelegate(float n, float f);
	private delegate void glClearDepthfDelegate(float d);
	private delegate void glGetProgramBinaryDelegate(uint program, int bufSize, int[] length, uint[] binaryFormat, IntPtr binary);
	private delegate void glProgramBinaryDelegate(uint program, uint binaryFormat, IntPtr binary, int length);
	private delegate void glProgramParameteriDelegate(uint program, uint pname, int value);
	private delegate void glUseProgramStagesDelegate(uint pipeline, uint stages, uint program);
	private delegate void glActiveShaderProgramDelegate(uint pipeline, uint program);
	private delegate uint glCreateShaderProgramvDelegate(uint type, int count, char[] strings);
	private delegate void glBindProgramPipelineDelegate(uint pipeline);
	private delegate void glDeleteProgramPipelinesDelegate(int n, uint[] pipelines);
	private delegate void glGenProgramPipelinesDelegate(int n, uint[] pipelines);
	private delegate bool glIsProgramPipelineDelegate(uint pipeline);
	private delegate void glGetProgramPipelineivDelegate(uint pipeline, uint pname, int[] @params);
	private delegate void glProgramUniform1iDelegate(uint program, uint location, int v0);
	private delegate void glProgramUniform1ivDelegate(uint program, uint location, int count, int[] value);
	private delegate void glProgramUniform1fDelegate(uint program, uint location, float v0);
	private delegate void glProgramUniform1fvDelegate(uint program, uint location, int count, float[] value);
	private delegate void glProgramUniform1dDelegate(uint program, uint location, double v0);
	private delegate void glProgramUniform1dvDelegate(uint program, uint location, int count, double[] value);
	private delegate void glProgramUniform1uiDelegate(uint program, uint location, uint v0);
	private delegate void glProgramUniform1uivDelegate(uint program, uint location, int count, uint[] value);
	private delegate void glProgramUniform2iDelegate(uint program, uint location, int v0, int v1);
	private delegate void glProgramUniform2ivDelegate(uint program, uint location, int count, int[] value);
	private delegate void glProgramUniform2fDelegate(uint program, uint location, float v0, float v1);
	private delegate void glProgramUniform2fvDelegate(uint program, uint location, int count, float[] value);
	private delegate void glProgramUniform2dDelegate(uint program, uint location, double v0, double v1);
	private delegate void glProgramUniform2dvDelegate(uint program, uint location, int count, double[] value);
	private delegate void glProgramUniform2uiDelegate(uint program, uint location, uint v0, uint v1);
	private delegate void glProgramUniform2uivDelegate(uint program, uint location, int count, uint[] value);
	private delegate void glProgramUniform3iDelegate(uint program, uint location, int v0, int v1, int v2);
	private delegate void glProgramUniform3ivDelegate(uint program, uint location, int count, int[] value);
	private delegate void glProgramUniform3fDelegate(uint program, uint location, float v0, float v1, float v2);
	private delegate void glProgramUniform3fvDelegate(uint program, uint location, int count, float[] value);
	private delegate void glProgramUniform3dDelegate(uint program, uint location, double v0, double v1, double v2);
	private delegate void glProgramUniform3dvDelegate(uint program, uint location, int count, double[] value);
	private delegate void glProgramUniform3uiDelegate(uint program, uint location, uint v0, uint v1, uint v2);
	private delegate void glProgramUniform3uivDelegate(uint program, uint location, int count, uint[] value);
	private delegate void glProgramUniform4iDelegate(uint program, uint location, int v0, int v1, int v2, int v3);
	private delegate void glProgramUniform4ivDelegate(uint program, uint location, int count, int[] value);
	private delegate void glProgramUniform4fDelegate(uint program, uint location, float v0, float v1, float v2, float v3);
	private delegate void glProgramUniform4fvDelegate(uint program, uint location, int count, float[] value);
	private delegate void glProgramUniform4dDelegate(uint program, uint location, double v0, double v1, double v2, double v3);
	private delegate void glProgramUniform4dvDelegate(uint program, uint location, int count, double[] value);
	private delegate void glProgramUniform4uiDelegate(uint program, uint location, uint v0, uint v1, uint v2, uint v3);
	private delegate void glProgramUniform4uivDelegate(uint program, uint location, int count, uint[] value);
	private delegate void glProgramUniformMatrix2fvDelegate(uint program, uint location, int count, bool transpose, float[] value);
	private delegate void glProgramUniformMatrix3fvDelegate(uint program, uint location, int count, bool transpose, float[] value);
	private delegate void glProgramUniformMatrix4fvDelegate(uint program, uint location, int count, bool transpose, float[] value);
	private delegate void glProgramUniformMatrix2dvDelegate(uint program, uint location, int count, bool transpose, double[] value);
	private delegate void glProgramUniformMatrix3dvDelegate(uint program, uint location, int count, bool transpose, double[] value);
	private delegate void glProgramUniformMatrix4dvDelegate(uint program, uint location, int count, bool transpose, double[] value);
	private delegate void glProgramUniformMatrix2x3fvDelegate(uint program, uint location, int count, bool transpose, float[] value);
	private delegate void glProgramUniformMatrix3x2fvDelegate(uint program, uint location, int count, bool transpose, float[] value);
	private delegate void glProgramUniformMatrix2x4fvDelegate(uint program, uint location, int count, bool transpose, float[] value);
	private delegate void glProgramUniformMatrix4x2fvDelegate(uint program, uint location, int count, bool transpose, float[] value);
	private delegate void glProgramUniformMatrix3x4fvDelegate(uint program, uint location, int count, bool transpose, float[] value);
	private delegate void glProgramUniformMatrix4x3fvDelegate(uint program, uint location, int count, bool transpose, float[] value);
	private delegate void glProgramUniformMatrix2x3dvDelegate(uint program, uint location, int count, bool transpose, double[] value);
	private delegate void glProgramUniformMatrix3x2dvDelegate(uint program, uint location, int count, bool transpose, double[] value);
	private delegate void glProgramUniformMatrix2x4dvDelegate(uint program, uint location, int count, bool transpose, double[] value);
	private delegate void glProgramUniformMatrix4x2dvDelegate(uint program, uint location, int count, bool transpose, double[] value);
	private delegate void glProgramUniformMatrix3x4dvDelegate(uint program, uint location, int count, bool transpose, double[] value);
	private delegate void glProgramUniformMatrix4x3dvDelegate(uint program, uint location, int count, bool transpose, double[] value);
	private delegate void glValidateProgramPipelineDelegate(uint pipeline);
	private delegate void glGetProgramPipelineInfoLogDelegate(uint pipeline, int bufSize, int[] length, char[] infoLog);
	private delegate void glVertexAttribL1dDelegate(uint index, double x);
	private delegate void glVertexAttribL2dDelegate(uint index, double x, double y);
	private delegate void glVertexAttribL3dDelegate(uint index, double x, double y, double z);
	private delegate void glVertexAttribL4dDelegate(uint index, double x, double y, double z, double w);
	private delegate void glVertexAttribL1dvDelegate(uint index, double[] v);
	private delegate void glVertexAttribL2dvDelegate(uint index, double[] v);
	private delegate void glVertexAttribL3dvDelegate(uint index, double[] v);
	private delegate void glVertexAttribL4dvDelegate(uint index, double[] v);
	private delegate void glVertexAttribLPointerDelegate(uint index, int size, uint type, int stride, IntPtr pointer);
	private delegate void glGetVertexAttribLdvDelegate(uint index, uint pname, double[] @params);
	private delegate void glViewportArrayvDelegate(uint first, int count, float[] v);
	private delegate void glViewportIndexedfDelegate(uint index, float x, float y, float w, float h);
	private delegate void glViewportIndexedfvDelegate(uint index, float[] v);
	private delegate void glScissorArrayvDelegate(uint first, int count, int[] v);
	private delegate void glScissorIndexedDelegate(uint index, int left, int bottom, int width, int height);
	private delegate void glScissorIndexedvDelegate(uint index, int[] v);
	private delegate void glDepthRangeArrayvDelegate(uint first, int count, double[] v);
	private delegate void glDepthRangeIndexedDelegate(uint index, double n, double f);
	private delegate void glGetFloati_vDelegate(uint target, uint index, float[] data);
	private delegate void glGetDoublei_vDelegate(uint target, uint index, double[] data);
	private delegate void glDrawArraysInstancedBaseInstanceDelegate(uint mode, int first, int count, int instancecount, uint baseinstance);
	private delegate void glDrawElementsInstancedBaseInstanceDelegate(uint mode, int count, uint type, IntPtr indices, int instancecount, uint baseinstance);
	private delegate void glDrawElementsInstancedBaseVertexBaseInstanceDelegate(uint mode, int count, uint type, IntPtr indices, int instancecount, int basevertex, uint baseinstance);
	private delegate void glGetInternalformativDelegate(uint target, uint internalformat, uint pname, int bufSize, int[] @params);
	private delegate void glGetActiveAtomicCounterBufferivDelegate(uint program, uint bufferIndex, uint pname, int[] @params);
	private delegate void glBindImageTextureDelegate(uint unit, uint texture, int level, bool layered, int layer, uint access, uint format);
	private delegate void glMemoryBarrierDelegate(uint barriers);
	private delegate void glTexStorage1DDelegate(uint target, int levels, uint internalformat, int width);
	private delegate void glTexStorage2DDelegate(uint target, int levels, uint internalformat, int width, int height);
	private delegate void glTexStorage3DDelegate(uint target, int levels, uint internalformat, int width, int height, int depth);
	private delegate void glDrawTransformFeedbackInstancedDelegate(uint mode, uint id, int instancecount);
	private delegate void glDrawTransformFeedbackStreamInstancedDelegate(uint mode, uint id, uint stream, int instancecount);
	private delegate void glClearBufferDataDelegate(uint target, uint internalformat, uint format, uint type, IntPtr data);
	private delegate void glClearBufferSubDataDelegate(uint target, uint internalformat, IntPtr offset, IntPtr size, uint format, uint type, IntPtr data);
	private delegate void glDispatchComputeDelegate(uint num_groups_x, uint num_groups_y, uint num_groups_z);
	private delegate void glDispatchComputeIndirectDelegate(IntPtr indirect);
	private delegate void glCopyImageSubDataDelegate(uint srcName, uint srcTarget, int srcLevel, int srcX, int srcY, int srcZ, uint dstName, uint dstTarget, int dstLevel, int dstX, int dstY, int dstZ, int srcWidth, int srcHeight, int srcDepth);
	private delegate void glFramebufferParameteriDelegate(uint target, uint pname, int param);
	private delegate void glGetFramebufferParameterivDelegate(uint target, uint pname, int[] @params);
	private delegate void glGetInternalformati64vDelegate(uint target, uint internalformat, uint pname, int bufSize, Int64[] @params);
	private delegate void glInvalidateTexSubImageDelegate(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth);
	private delegate void glInvalidateTexImageDelegate(uint texture, int level);
	private delegate void glInvalidateBufferSubDataDelegate(uint buffer, IntPtr offset, IntPtr length);
	private delegate void glInvalidateBufferDataDelegate(uint buffer);
	private delegate void glInvalidateFramebufferDelegate(uint target, int numAttachments, uint[] attachments);
	private delegate void glInvalidateSubFramebufferDelegate(uint target, int numAttachments, uint[] attachments, int x, int y, int width, int height);
	private delegate void glMultiDrawArraysIndirectDelegate(uint mode, IntPtr indirect, int drawcount, int stride);
	private delegate void glMultiDrawElementsIndirectDelegate(uint mode, uint type, IntPtr indirect, int drawcount, int stride);
	private delegate void glGetProgramInterfaceivDelegate(uint program, uint programInterface, uint pname, int[] @params);
	private delegate uint glGetProgramResourceIndexDelegate(uint program, uint programInterface, char[] name);
	private delegate void glGetProgramResourceNameDelegate(uint program, uint programInterface, uint index, int bufSize, int[] length, char[] name);
	private delegate void glGetProgramResourceivDelegate(uint program, uint programInterface, uint index, int propCount, uint[] props, int bufSize, int[] length, int[] @params);
	private delegate int glGetProgramResourceLocationDelegate(uint program, uint programInterface, char[] name);
	private delegate int glGetProgramResourceLocationIndexDelegate(uint program, uint programInterface, char[] name);
	private delegate void glShaderStorageBlockBindingDelegate(uint program, uint storageBlockIndex, uint storageBlockBinding);
	private delegate void glTexBufferRangeDelegate(uint target, uint internalformat, uint buffer, IntPtr offset, IntPtr size);
	private delegate void glTexStorage2DMultisampleDelegate(uint target, int samples, uint internalformat, int width, int height, bool fixedsamplelocations);
	private delegate void glTexStorage3DMultisampleDelegate(uint target, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations);
	private delegate void glTextureViewDelegate(uint texture, uint target, uint origtexture, uint internalformat, uint minlevel, uint numlevels, uint minlayer, uint numlayers);
	private delegate void glBindVertexBufferDelegate(uint bindingindex, uint buffer, IntPtr offset, int stride);
	private delegate void glVertexAttribFormatDelegate(uint attribindex, int size, uint type, bool normalized, uint relativeoffset);
	private delegate void glVertexAttribIFormatDelegate(uint attribindex, int size, uint type, uint relativeoffset);
	private delegate void glVertexAttribLFormatDelegate(uint attribindex, int size, uint type, uint relativeoffset);
	private delegate void glVertexAttribBindingDelegate(uint attribindex, uint bindingindex);
	private delegate void glVertexBindingDivisorDelegate(uint bindingindex, uint divisor);
	private delegate void glDebugMessageControlDelegate(uint source, uint type, uint severity, int count, uint[] ids, bool enabled);
	private delegate void glDebugMessageInsertDelegate(uint source, uint type, uint id, uint severity, int length, char[] buf);
	private delegate void glDebugMessageCallbackDelegate(IntPtr callback, IntPtr userParam);
	private delegate uint glGetDebugMessageLogDelegate(uint count, int bufSize, uint[] sources, uint[] types, uint[] ids, uint[] severities, int[] lengths, char[] messageLog);
	private delegate void glPushDebugGroupDelegate(uint source, uint id, int length, char[] message);
	private delegate void glPopDebugGroupDelegate();
	private delegate void glObjectLabelDelegate(uint identifier, uint name, int length, char[] label);
	private delegate void glGetObjectLabelDelegate(uint identifier, uint name, int bufSize, int[] length, char[] label);
	private delegate void glObjectPtrLabelDelegate(IntPtr ptr, int length, char[] label);
	private delegate void glGetObjectPtrLabelDelegate(IntPtr ptr, int bufSize, int[] length, char[] label);
	private delegate void glGetPointervDelegate(uint pname, IntPtr @params);
	private delegate void glBufferStorageDelegate(uint target, IntPtr size, IntPtr data, uint flags);
	private delegate void glClearTexImageDelegate(uint texture, int level, uint format, uint type, IntPtr data);
	private delegate void glClearTexSubImageDelegate(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, IntPtr data);
	private delegate void glBindBuffersBaseDelegate(uint target, uint first, int count, uint[] buffers);
	private delegate void glBindBuffersRangeDelegate(uint target, uint first, int count, uint[] buffers, IntPtr offsets, IntPtr sizes);
	private delegate void glBindTexturesDelegate(uint first, int count, uint[] textures);
	private delegate void glBindSamplersDelegate(uint first, int count, uint[] samplers);
	private delegate void glBindImageTexturesDelegate(uint first, int count, uint[] textures);
	private delegate void glBindVertexBuffersDelegate(uint first, int count, uint[] buffers, IntPtr offsets, int[] strides);
	private delegate void glClipControlDelegate(uint origin, uint depth);
	private delegate void glCreateTransformFeedbacksDelegate(int n, uint[] ids);
	private delegate void glTransformFeedbackBufferBaseDelegate(uint xfb, uint index, uint buffer);
	private delegate void glTransformFeedbackBufferRangeDelegate(uint xfb, uint index, uint buffer, IntPtr offset, IntPtr size);
	private delegate void glGetTransformFeedbackivDelegate(uint xfb, uint pname, int[] param);
	private delegate void glGetTransformFeedbacki_vDelegate(uint xfb, uint pname, uint index, int[] param);
	private delegate void glGetTransformFeedbacki64_vDelegate(uint xfb, uint pname, uint index, Int64[] param);
	private delegate void glCreateBuffersDelegate(int n, uint[] buffers);
	private delegate void glNamedBufferStorageDelegate(uint buffer, IntPtr size, IntPtr data, uint flags);
	private delegate void glNamedBufferDataDelegate(uint buffer, IntPtr size, IntPtr data, uint usage);
	private delegate void glNamedBufferSubDataDelegate(uint buffer, IntPtr offset, IntPtr size, IntPtr data);
	private delegate void glCopyNamedBufferSubDataDelegate(uint readBuffer, uint writeBuffer, IntPtr readOffset, IntPtr writeOffset, IntPtr size);
	private delegate void glClearNamedBufferDataDelegate(uint buffer, uint internalformat, uint format, uint type, IntPtr data);
	private delegate void glClearNamedBufferSubDataDelegate(uint buffer, uint internalformat, IntPtr offset, IntPtr size, uint format, uint type, IntPtr data);
	private delegate void glMapNamedBufferDelegate(uint buffer, uint access);
	private delegate void glMapNamedBufferRangeDelegate(uint buffer, IntPtr offset, IntPtr length, uint access);
	private delegate bool glUnmapNamedBufferDelegate(uint buffer);
	private delegate void glFlushMappedNamedBufferRangeDelegate(uint buffer, IntPtr offset, IntPtr length);
	private delegate void glGetNamedBufferParameterivDelegate(uint buffer, uint pname, int[] @params);
	private delegate void glGetNamedBufferParameteri64vDelegate(uint buffer, uint pname, Int64[] @params);
	private delegate void glGetNamedBufferPointervDelegate(uint buffer, uint pname, IntPtr @params);
	private delegate void glGetNamedBufferSubDataDelegate(uint buffer, IntPtr offset, IntPtr size, IntPtr data);
	private delegate void glCreateFramebuffersDelegate(int n, uint[] framebuffers);
	private delegate void glNamedFramebufferRenderbufferDelegate(uint framebuffer, uint attachment, uint renderbuffertarget, uint renderbuffer);
	private delegate void glNamedFramebufferParameteriDelegate(uint framebuffer, uint pname, int param);
	private delegate void glNamedFramebufferTextureDelegate(uint framebuffer, uint attachment, uint texture, int level);
	private delegate void glNamedFramebufferTextureLayerDelegate(uint framebuffer, uint attachment, uint texture, int level, int layer);
	private delegate void glNamedFramebufferDrawBufferDelegate(uint framebuffer, uint buf);
	private delegate void glNamedFramebufferDrawBuffersDelegate(uint framebuffer, int n, uint[] bufs);
	private delegate void glNamedFramebufferReadBufferDelegate(uint framebuffer, uint src);
	private delegate void glInvalidateNamedFramebufferDataDelegate(uint framebuffer, int numAttachments, uint[] attachments);
	private delegate void glInvalidateNamedFramebufferSubDataDelegate(uint framebuffer, int numAttachments, uint[] attachments, int x, int y, int width, int height);
	private delegate void glClearNamedFramebufferivDelegate(uint framebuffer, uint buffer, int drawbuffer, int[] value);
	private delegate void glClearNamedFramebufferuivDelegate(uint framebuffer, uint buffer, int drawbuffer, uint[] value);
	private delegate void glClearNamedFramebufferfvDelegate(uint framebuffer, uint buffer, int drawbuffer, float[] value);
	private delegate void glClearNamedFramebufferfiDelegate(uint framebuffer, uint buffer, int drawbuffer, float depth, int stencil);
	private delegate void glBlitNamedFramebufferDelegate(uint readFramebuffer, uint drawFramebuffer, int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter);
	private delegate uint glCheckNamedFramebufferStatusDelegate(uint framebuffer, uint target);
	private delegate void glGetNamedFramebufferParameterivDelegate(uint framebuffer, uint pname, int[] param);
	private delegate void glGetNamedFramebufferAttachmentParameterivDelegate(uint framebuffer, uint attachment, uint pname, int[] @params);
	private delegate void glCreateRenderbuffersDelegate(int n, uint[] renderbuffers);
	private delegate void glNamedRenderbufferStorageDelegate(uint renderbuffer, uint internalformat, int width, int height);
	private delegate void glNamedRenderbufferStorageMultisampleDelegate(uint renderbuffer, int samples, uint internalformat, int width, int height);
	private delegate void glGetNamedRenderbufferParameterivDelegate(uint renderbuffer, uint pname, int[] @params);
	private delegate void glCreateTexturesDelegate(uint target, int n, uint[] textures);
	private delegate void glTextureBufferDelegate(uint texture, uint internalformat, uint buffer);
	private delegate void glTextureBufferRangeDelegate(uint texture, uint internalformat, uint buffer, IntPtr offset, IntPtr size);
	private delegate void glTextureStorage1DDelegate(uint texture, int levels, uint internalformat, int width);
	private delegate void glTextureStorage2DDelegate(uint texture, int levels, uint internalformat, int width, int height);
	private delegate void glTextureStorage3DDelegate(uint texture, int levels, uint internalformat, int width, int height, int depth);
	private delegate void glTextureStorage2DMultisampleDelegate(uint texture, int samples, uint internalformat, int width, int height, bool fixedsamplelocations);
	private delegate void glTextureStorage3DMultisampleDelegate(uint texture, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations);
	private delegate void glTextureSubImage1DDelegate(uint texture, int level, int xoffset, int width, uint format, uint type, IntPtr pixels);
	private delegate void glTextureSubImage2DDelegate(uint texture, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, IntPtr pixels);
	private delegate void glTextureSubImage3DDelegate(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, IntPtr pixels);
	private delegate void glCompressedTextureSubImage1DDelegate(uint texture, int level, int xoffset, int width, uint format, int imageSize, IntPtr data);
	private delegate void glCompressedTextureSubImage2DDelegate(uint texture, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, IntPtr data);
	private delegate void glCompressedTextureSubImage3DDelegate(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, IntPtr data);
	private delegate void glCopyTextureSubImage1DDelegate(uint texture, int level, int xoffset, int x, int y, int width);
	private delegate void glCopyTextureSubImage2DDelegate(uint texture, int level, int xoffset, int yoffset, int x, int y, int width, int height);
	private delegate void glCopyTextureSubImage3DDelegate(uint texture, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height);
	private delegate void glTextureParameterfDelegate(uint texture, uint pname, float param);
	private delegate void glTextureParameterfvDelegate(uint texture, uint pname, float[] param);
	private delegate void glTextureParameteriDelegate(uint texture, uint pname, int param);
	private delegate void glTextureParameterIivDelegate(uint texture, uint pname, int[] @params);
	private delegate void glTextureParameterIuivDelegate(uint texture, uint pname, uint[] @params);
	private delegate void glTextureParameterivDelegate(uint texture, uint pname, int[] param);
	private delegate void glGenerateTextureMipmapDelegate(uint texture);
	private delegate void glBindTextureUnitDelegate(uint unit, uint texture);
	private delegate void glGetTextureImageDelegate(uint texture, int level, uint format, uint type, int bufSize, IntPtr pixels);
	private delegate void glGetCompressedTextureImageDelegate(uint texture, int level, int bufSize, IntPtr pixels);
	private delegate void glGetTextureLevelParameterfvDelegate(uint texture, int level, uint pname, float[] @params);
	private delegate void glGetTextureLevelParameterivDelegate(uint texture, int level, uint pname, int[] @params);
	private delegate void glGetTextureParameterfvDelegate(uint texture, uint pname, float[] @params);
	private delegate void glGetTextureParameterIivDelegate(uint texture, uint pname, int[] @params);
	private delegate void glGetTextureParameterIuivDelegate(uint texture, uint pname, uint[] @params);
	private delegate void glGetTextureParameterivDelegate(uint texture, uint pname, int[] @params);
	private delegate void glCreateVertexArraysDelegate(int n, uint[] arrays);
	private delegate void glDisableVertexArrayAttribDelegate(uint vaobj, uint index);
	private delegate void glEnableVertexArrayAttribDelegate(uint vaobj, uint index);
	private delegate void glVertexArrayElementBufferDelegate(uint vaobj, uint buffer);
	private delegate void glVertexArrayVertexBufferDelegate(uint vaobj, uint bindingindex, uint buffer, IntPtr offset, int stride);
	private delegate void glVertexArrayVertexBuffersDelegate(uint vaobj, uint first, int count, uint[] buffers, IntPtr offsets, int[] strides);
	private delegate void glVertexArrayAttribBindingDelegate(uint vaobj, uint attribindex, uint bindingindex);
	private delegate void glVertexArrayAttribFormatDelegate(uint vaobj, uint attribindex, int size, uint type, bool normalized, uint relativeoffset);
	private delegate void glVertexArrayAttribIFormatDelegate(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset);
	private delegate void glVertexArrayAttribLFormatDelegate(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset);
	private delegate void glVertexArrayBindingDivisorDelegate(uint vaobj, uint bindingindex, uint divisor);
	private delegate void glGetVertexArrayivDelegate(uint vaobj, uint pname, int[] param);
	private delegate void glGetVertexArrayIndexedivDelegate(uint vaobj, uint index, uint pname, int[] param);
	private delegate void glGetVertexArrayIndexed64ivDelegate(uint vaobj, uint index, uint pname, Int64[] param);
	private delegate void glCreateSamplersDelegate(int n, uint[] samplers);
	private delegate void glCreateProgramPipelinesDelegate(int n, uint[] pipelines);
	private delegate void glCreateQueriesDelegate(uint target, int n, uint[] ids);
	private delegate void glGetQueryBufferObjecti64vDelegate(uint id, uint buffer, uint pname, IntPtr offset);
	private delegate void glGetQueryBufferObjectivDelegate(uint id, uint buffer, uint pname, IntPtr offset);
	private delegate void glGetQueryBufferObjectui64vDelegate(uint id, uint buffer, uint pname, IntPtr offset);
	private delegate void glGetQueryBufferObjectuivDelegate(uint id, uint buffer, uint pname, IntPtr offset);
	private delegate void glMemoryBarrierByRegionDelegate(uint barriers);
	private delegate void glGetTextureSubImageDelegate(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, int bufSize, IntPtr pixels);
	private delegate void glGetCompressedTextureSubImageDelegate(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int bufSize, IntPtr pixels);
	private delegate uint glGetGraphicsResetStatusDelegate();
	private delegate void glGetnCompressedTexImageDelegate(uint target, int lod, int bufSize, IntPtr pixels);
	private delegate void glGetnTexImageDelegate(uint target, int level, uint format, uint type, int bufSize, IntPtr pixels);
	private delegate void glGetnUniformdvDelegate(uint program, uint location, int bufSize, double[] @params);
	private delegate void glGetnUniformfvDelegate(uint program, uint location, int bufSize, float[] @params);
	private delegate void glGetnUniformivDelegate(uint program, uint location, int bufSize, int[] @params);
	private delegate void glGetnUniformuivDelegate(uint program, uint location, int bufSize, uint[] @params);
	private delegate void glReadnPixelsDelegate(int x, int y, int width, int height, uint format, uint type, int bufSize, IntPtr data);
	private delegate void glGetnMapdvDelegate(uint target, uint query, int bufSize, double[] v);
	private delegate void glGetnMapfvDelegate(uint target, uint query, int bufSize, float[] v);
	private delegate void glGetnMapivDelegate(uint target, uint query, int bufSize, int[] v);
	private delegate void glGetnPixelMapfvDelegate(uint map, int bufSize, float[] values);
	private delegate void glGetnPixelMapuivDelegate(uint map, int bufSize, uint[] values);
	private delegate void glGetnPixelMapusvDelegate(uint map, int bufSize, ushort[] values);
	private delegate void glGetnPolygonStippleDelegate(int bufSize, byte[] pattern);
	private delegate void glGetnColorTableDelegate(uint target, uint format, uint type, int bufSize, IntPtr table);
	private delegate void glGetnConvolutionFilterDelegate(uint target, uint format, uint type, int bufSize, IntPtr image);
	private delegate void glGetnSeparableFilterDelegate(uint target, uint format, uint type, int rowBufSize, IntPtr row, int columnBufSize, IntPtr column, IntPtr span);
	private delegate void glGetnHistogramDelegate(uint target, bool reset, uint format, uint type, int bufSize, IntPtr values);
	private delegate void glGetnMinmaxDelegate(uint target, bool reset, uint format, uint type, int bufSize, IntPtr values);
	private delegate void glTextureBarrierDelegate();

	#endregion

	#region Commands

	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern unsafe sbyte* glGetString(uint name);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glCullFace(uint mode);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glFrontFace(uint mode);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glHint(uint target, uint mode);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLineWidth(float width);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPointSize(float size);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPolygonMode(uint face, uint mode);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glScissor(int x, int y, int width, int height);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexParameterf(uint target, uint pname, float param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexParameterfv(uint target, uint pname, float[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexParameteri(uint target, uint pname, uint param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexParameteriv(uint target, uint pname, uint[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexImage1D(uint target, int level, uint internalformat, int width, int border, uint format, uint type, IntPtr pixels);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, uint type, IntPtr pixels);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glDrawBuffer(uint buf);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glClear(uint mask);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glClearColor(float red, float green, float blue, float alpha);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glClearStencil(int s);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glClearDepth(double depth);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glStencilMask(uint mask);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColorMask(bool red, bool green, bool blue, bool alpha);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glDepthMask(bool flag);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glDisable(uint cap);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEnable(uint cap);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glFinish();
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glFlush();
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glBlendFunc(uint sfactor, uint dfactor);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLogicOp(uint opcode);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glStencilFunc(uint func, int @ref, uint mask);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glStencilOp(uint fail, uint zfail, uint zpass);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glDepthFunc(uint func);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPixelStoref(uint pname, float param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPixelStorei(uint pname, int param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glReadBuffer(uint src);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glReadPixels(float x, float y, float width, float height, uint format, uint type, IntPtr pixels);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetBooleanv(uint pname, bool[] data);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetDoublev(uint pname, double[] data);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern uint glGetError();
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetFloatv(uint pname, float[] data);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetIntegerv(uint pname, int[] data);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetTexImage(uint target, int level, uint format, uint type, IntPtr pixels);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetTexParameterfv(uint target, uint pname, float[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetTexParameteriv(uint target, uint pname, int[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetTexLevelParameterfv(uint target, int level, uint pname, float[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetTexLevelParameteriv(uint target, int level, uint pname, int[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern bool glIsEnabled(uint cap);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glDepthRange(double near, double far);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glViewport(int x, int y, int width, int height);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glNewList(uint list, uint mode);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEndList();
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glCallList(uint list);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glCallLists(int n, uint type, IntPtr lists);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glDeleteLists(uint list, int range);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern uint glGenLists(int range);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glListBase(uint @base);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glBegin(uint mode);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glBitmap(int width, int height, float xorig, float yorig, float xmove, float ymove, byte[] bitmap);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3b(byte red, byte green, byte blue);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3bv(byte[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3d(double red, double green, double blue);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3dv(double[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3f(float red, float green, float blue);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3fv(float[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3i(int red, int green, int blue);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3iv(int[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3s(short red, short green, short blue);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3sv(short[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3ub(byte red, byte green, byte blue);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3ubv(byte[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3ui(uint red, uint green, uint blue);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3uiv(uint[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3us(ushort red, ushort green, ushort blue);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor3usv(ushort[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4b(byte red, byte green, byte blue, byte alpha);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4bv(byte[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4d(double red, double green, double blue, double alpha);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4dv(double[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4f(float red, float green, float blue, float alpha);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4fv(float[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4i(int red, int green, int blue, int alpha);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4iv(int[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4s(short red, short green, short blue, short alpha);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4sv(short[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4ub(byte red, byte green, byte blue, byte alpha);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4ubv(byte[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4ui(uint red, uint green, uint blue, uint alpha);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4uiv(uint[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4us(ushort red, ushort green, ushort blue, ushort alpha);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColor4usv(ushort[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEdgeFlag(bool flag);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEdgeFlagv(bool[] flag);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEnd();
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glIndexd(double c);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glIndexdv(double[] c);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glIndexf(float c);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glIndexfv(float[] c);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glIndexi(int c);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glIndexiv(int[] c);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glIndexs(short c);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glIndexsv(short[] c);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glNormal3b(byte nx, byte ny, byte nz);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glNormal3bv(byte[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glNormal3d(double nx, double ny, double nz);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glNormal3dv(double[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glNormal3f(float nx, float ny, float nz);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glNormal3fv(float[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glNormal3i(int nx, int ny, int nz);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glNormal3iv(int[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glNormal3s(short nx, short ny, short nz);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glNormal3sv(short[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos2d(double x, double y);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos2dv(double[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos2f(float x, float y);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos2fv(float[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos2i(int x, int y);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos2iv(int[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos2s(short x, short y);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos2sv(short[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos3d(double x, double y, double z);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos3dv(double[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos3f(float x, float y, float z);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos3fv(float[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos3i(int x, int y, int z);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos3iv(int[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos3s(short x, short y, short z);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos3sv(short[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos4d(double x, double y, double z, double w);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos4dv(double[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos4f(float x, float y, float z, float w);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos4fv(float[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos4i(int x, int y, int z, int w);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos4iv(int[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos4s(short x, short y, short z, short w);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRasterPos4sv(short[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRectd(double x1, double y1, double x2, double y2);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRectdv(double[] v1, double[] v2);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRectf(float x1, float y1, float x2, float y2);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRectfv(float[] v1, float[] v2);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRecti(int x1, int y1, int x2, int y2);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRectiv(int[] v1, int[] v2);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRects(short x1, short y1, short x2, short y2);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRectsv(short[] v1, short[] v2);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord1d(double s);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord1dv(double[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord1f(float s);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord1fv(float[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord1i(int s);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord1iv(int[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord1s(short s);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord1sv(short[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord2d(double s, double t);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord2dv(double[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord2f(float s, float t);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord2fv(float[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord2i(int s, int t);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord2iv(int[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord2s(short s, short t);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord2sv(short[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord3d(double s, double t, double r);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord3dv(double[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord3f(float s, float t, float r);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord3fv(float[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord3i(int s, int t, int r);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord3iv(int[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord3s(short s, short t, short r);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord3sv(short[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord4d(double s, double t, double r, double q);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord4dv(double[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord4f(float s, float t, float r, float q);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord4fv(float[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord4i(int s, int t, int r, int q);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord4iv(int[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord4s(short s, short t, short r, short q);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoord4sv(short[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex2d(double x, double y);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex2dv(double[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex2f(float x, float y);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex2fv(float[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex2i(int x, int y);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex2iv(int[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex2s(short x, short y);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex2sv(short[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex3d(double x, double y, double z);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex3dv(double[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex3f(float x, float y, float z);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex3fv(float[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex3i(int x, int y, int z);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex3iv(int[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex3s(short x, short y, short z);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex3sv(short[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex4d(double x, double y, double z, double w);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex4dv(double[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex4f(float x, float y, float z, float w);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex4fv(float[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex4i(int x, int y, int z, int w);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex4iv(int[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex4s(short x, short y, short z, short w);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertex4sv(short[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glClipPlane(uint plane, double[] equation);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColorMaterial(uint face, uint mode);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glFogf(uint pname, float param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glFogfv(uint pname, float[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glFogi(uint pname, int param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glFogiv(uint pname, int[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLightf(uint light, uint pname, float param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLightfv(uint light, uint pname, float[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLighti(uint light, uint pname, int param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLightiv(uint light, uint pname, int[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLightModelf(uint pname, float param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLightModelfv(uint pname, float[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLightModeli(uint pname, int param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLightModeliv(uint pname, int[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLineStipple(int factor, ushort pattern);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMaterialf(uint face, uint pname, float param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMaterialfv(uint face, uint pname, float[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMateriali(uint face, uint pname, int param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMaterialiv(uint face, uint pname, int[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPolygonStipple(byte[] mask);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glShadeModel(uint mode);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexEnvf(uint target, uint pname, float param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexEnvfv(uint target, uint pname, float[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexEnvi(uint target, uint pname, int param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexEnviv(uint target, uint pname, int[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexGend(uint coord, uint pname, double param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexGendv(uint coord, uint pname, double[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexGenf(uint coord, uint pname, float param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexGenfv(uint coord, uint pname, float[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexGeni(uint coord, uint pname, int param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexGeniv(uint coord, uint pname, int[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glFeedbackBuffer(int size, uint type, float[] buffer);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glSelectBuffer(int size, uint[] buffer);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern int glRenderMode(uint mode);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glInitNames();
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLoadName(uint name);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPassThrough(float token);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPopName();
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPushName(uint name);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glClearAccum(float red, float green, float blue, float alpha);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glClearIndex(float c);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glIndexMask(uint mask);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glAccum(uint op, float value);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPopAttrib();
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPushAttrib(uint mask);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMap1d(uint target, double u1, double u2, int stride, int order, double[] points);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMap1f(uint target, float u1, float u2, int stride, int order, float[] points);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMap2d(uint target, double u1, double u2, int ustride, int uorder, double v1, double v2, int vstride, int vorder, double[] points);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMap2f(uint target, float u1, float u2, int ustride, int uorder, float v1, float v2, int vstride, int vorder, float[] points);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMapGrid1d(int un, double u1, double u2);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMapGrid1f(int un, float u1, float u2);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMapGrid2d(int un, double u1, double u2, int vn, double v1, double v2);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMapGrid2f(int un, float u1, float u2, int vn, float v1, float v2);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEvalCoord1d(double u);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEvalCoord1dv(double[] u);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEvalCoord1f(float u);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEvalCoord1fv(float[] u);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEvalCoord2d(double u, double v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEvalCoord2dv(double[] u);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEvalCoord2f(float u, float v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEvalCoord2fv(float[] u);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEvalMesh1(uint mode, int i1, int i2);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEvalPoint1(int i);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEvalMesh2(uint mode, int i1, int i2, int j1, int j2);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEvalPoint2(int i, int j);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glAlphaFunc(uint func, float @ref);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPixelZoom(float xfactor, float yfactor);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPixelTransferf(uint pname, float param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPixelTransferi(uint pname, int param);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPixelMapfv(uint map, int mapsize, float[] values);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPixelMapuiv(uint map, int mapsize, uint[] values);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPixelMapusv(uint map, int mapsize, ushort[] values);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glCopyPixels(int x, int y, int width, int height, uint type);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glDrawPixels(int width, int height, uint format, uint type, IntPtr pixels);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetClipPlane(uint plane, double[] equation);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetLightfv(uint light, uint pname, float[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetLightiv(uint light, uint pname, int[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetMapdv(uint target, uint query, double[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetMapfv(uint target, uint query, float[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetMapiv(uint target, uint query, int[] v);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetMaterialfv(uint face, uint pname, float[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetMaterialiv(uint face, uint pname, int[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetPixelMapfv(uint map, float[] values);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetPixelMapuiv(uint map, uint[] values);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetPixelMapusv(uint map, ushort[] values);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetPolygonStipple(byte[] mask);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetTexEnvfv(uint target, uint pname, float[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetTexEnviv(uint target, uint pname, int[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetTexGendv(uint coord, uint pname, double[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetTexGenfv(uint coord, uint pname, float[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetTexGeniv(uint coord, uint pname, int[] @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern bool glIsList(uint list);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glFrustum(double left, double right, double bottom, double top, double zNear, double zFar);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLoadIdentity();
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLoadMatrixf(float[] m);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glLoadMatrixd(double[] m);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMatrixMode(uint mode);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMultMatrixf(float[] m);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glMultMatrixd(double[] m);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glOrtho(double left, double right, double bottom, double top, double zNear, double zFar);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPopMatrix();
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPushMatrix();
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRotated(double angle, double x, double y, double z);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glRotatef(float angle, float x, float y, float z);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glScaled(double x, double y, double z);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glScalef(float x, float y, float z);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTranslated(double x, double y, double z);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTranslatef(float x, float y, float z);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glDrawArrays(uint mode, int first, int count);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glDrawElements(uint mode, int count, uint type, IntPtr indices);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGetPointerv(uint pname, IntPtr @params);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPolygonOffset(float factor, float units);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glCopyTexImage1D(uint target, int level, uint internalformat, int x, int y, int width, int border);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glCopyTexImage2D(uint target, int level, uint internalformat, int x, int y, int width, int height, int border);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glCopyTexSubImage1D(uint target, int level, int xoffset, int x, int y, int width);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glCopyTexSubImage2D(uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexSubImage1D(uint target, int level, int xoffset, int width, uint format, uint type, IntPtr pixels);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, IntPtr pixels);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glBindTexture(uint target, uint texture);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glDeleteTextures(int n, uint[] textures);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glGenTextures(int n, uint[] textures);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern bool glIsTexture(uint texture);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glArrayElement(int i);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glColorPointer(int size, uint type, int stride, IntPtr pointer);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glDisableClientState(uint array);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEdgeFlagPointer(int stride, IntPtr pointer);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glEnableClientState(uint array);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glIndexPointer(uint type, int stride, IntPtr pointer);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glInterleavedArrays(uint format, int stride, IntPtr pointer);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glNormalPointer(uint type, int stride, IntPtr pointer);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glTexCoordPointer(int size, uint type, int stride, IntPtr pointer);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glVertexPointer(int size, uint type, int stride, IntPtr pointer);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern bool glAreTexturesResident(int n, uint[] textures, bool[] residences);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPrioritizeTextures(int n, uint[] textures, float[] priorities);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glIndexub(byte c);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glIndexubv(byte[] c);
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPopClientAttrib();
	[DllImport(OpenGLDllImportBase.LIB_OPENGL_LIBRARY, SetLastError = true)]
	public static extern void glPushClientAttrib(uint mask);

	public static void glDrawRangeElements(uint mode, uint start, uint end, int count, uint type, IntPtr indices)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawRangeElementsDelegate>()(mode, start, end, count, type, indices);
	}

	public static void glTexImage3D(uint target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint type, IntPtr pixels)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexImage3DDelegate>()(target, level, internalformat, width, height, depth, border, format, type, pixels);
	}

	public static void glTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, IntPtr pixels)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexSubImage3DDelegate>()(target, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels);
	}

	public static void glCopyTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height)
	{
		OpenGLDllImportBase.GetDelegateFor<glCopyTexSubImage3DDelegate>()(target, level, xoffset, yoffset, zoffset, x, y, width, height);
	}

	public static void glActiveTexture(uint texture)
	{
		OpenGLDllImportBase.GetDelegateFor<glActiveTextureDelegate>()(texture);
	}

	public static void glSampleCoverage(float value, bool invert)
	{
		OpenGLDllImportBase.GetDelegateFor<glSampleCoverageDelegate>()(value, invert);
	}

	public static void glCompressedTexImage3D(uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glCompressedTexImage3DDelegate>()(target, level, internalformat, width, height, depth, border, imageSize, data);
	}

	public static void glCompressedTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glCompressedTexImage2DDelegate>()(target, level, internalformat, width, height, border, imageSize, data);
	}

	public static void glCompressedTexImage1D(uint target, int level, uint internalformat, int width, int border, int imageSize, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glCompressedTexImage1DDelegate>()(target, level, internalformat, width, border, imageSize, data);
	}

	public static void glCompressedTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glCompressedTexSubImage3DDelegate>()(target, level, xoffset, yoffset, zoffset, width, height, depth, format, imageSize, data);
	}

	public static void glCompressedTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glCompressedTexSubImage2DDelegate>()(target, level, xoffset, yoffset, width, height, format, imageSize, data);
	}

	public static void glCompressedTexSubImage1D(uint target, int level, int xoffset, int width, uint format, int imageSize, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glCompressedTexSubImage1DDelegate>()(target, level, xoffset, width, format, imageSize, data);
	}

	public static void glGetCompressedTexImage(uint target, int level, IntPtr img)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetCompressedTexImageDelegate>()(target, level, img);
	}

	public static void glClientActiveTexture(uint texture)
	{
		OpenGLDllImportBase.GetDelegateFor<glClientActiveTextureDelegate>()(texture);
	}

	public static void glMultiTexCoord1d(uint target, double s)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord1dDelegate>()(target, s);
	}

	public static void glMultiTexCoord1dv(uint target, double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord1dvDelegate>()(target, v);
	}

	public static void glMultiTexCoord1f(uint target, float s)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord1fDelegate>()(target, s);
	}

	public static void glMultiTexCoord1fv(uint target, float[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord1fvDelegate>()(target, v);
	}

	public static void glMultiTexCoord1i(uint target, int s)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord1iDelegate>()(target, s);
	}

	public static void glMultiTexCoord1iv(uint target, int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord1ivDelegate>()(target, v);
	}

	public static void glMultiTexCoord1s(uint target, short s)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord1sDelegate>()(target, s);
	}

	public static void glMultiTexCoord1sv(uint target, short[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord1svDelegate>()(target, v);
	}

	public static void glMultiTexCoord2d(uint target, double s, double t)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord2dDelegate>()(target, s, t);
	}

	public static void glMultiTexCoord2dv(uint target, double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord2dvDelegate>()(target, v);
	}

	public static void glMultiTexCoord2f(uint target, float s, float t)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord2fDelegate>()(target, s, t);
	}

	public static void glMultiTexCoord2fv(uint target, float[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord2fvDelegate>()(target, v);
	}

	public static void glMultiTexCoord2i(uint target, int s, int t)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord2iDelegate>()(target, s, t);
	}

	public static void glMultiTexCoord2iv(uint target, int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord2ivDelegate>()(target, v);
	}

	public static void glMultiTexCoord2s(uint target, short s, short t)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord2sDelegate>()(target, s, t);
	}

	public static void glMultiTexCoord2sv(uint target, short[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord2svDelegate>()(target, v);
	}

	public static void glMultiTexCoord3d(uint target, double s, double t, double r)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord3dDelegate>()(target, s, t, r);
	}

	public static void glMultiTexCoord3dv(uint target, double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord3dvDelegate>()(target, v);
	}

	public static void glMultiTexCoord3f(uint target, float s, float t, float r)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord3fDelegate>()(target, s, t, r);
	}

	public static void glMultiTexCoord3fv(uint target, float[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord3fvDelegate>()(target, v);
	}

	public static void glMultiTexCoord3i(uint target, int s, int t, int r)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord3iDelegate>()(target, s, t, r);
	}

	public static void glMultiTexCoord3iv(uint target, int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord3ivDelegate>()(target, v);
	}

	public static void glMultiTexCoord3s(uint target, short s, short t, short r)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord3sDelegate>()(target, s, t, r);
	}

	public static void glMultiTexCoord3sv(uint target, short[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord3svDelegate>()(target, v);
	}

	public static void glMultiTexCoord4d(uint target, double s, double t, double r, double q)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord4dDelegate>()(target, s, t, r, q);
	}

	public static void glMultiTexCoord4dv(uint target, double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord4dvDelegate>()(target, v);
	}

	public static void glMultiTexCoord4f(uint target, float s, float t, float r, float q)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord4fDelegate>()(target, s, t, r, q);
	}

	public static void glMultiTexCoord4fv(uint target, float[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord4fvDelegate>()(target, v);
	}

	public static void glMultiTexCoord4i(uint target, int s, int t, int r, int q)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord4iDelegate>()(target, s, t, r, q);
	}

	public static void glMultiTexCoord4iv(uint target, int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord4ivDelegate>()(target, v);
	}

	public static void glMultiTexCoord4s(uint target, short s, short t, short r, short q)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord4sDelegate>()(target, s, t, r, q);
	}

	public static void glMultiTexCoord4sv(uint target, short[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoord4svDelegate>()(target, v);
	}

	public static void glLoadTransposeMatrixf(float[] m)
	{
		OpenGLDllImportBase.GetDelegateFor<glLoadTransposeMatrixfDelegate>()(m);
	}

	public static void glLoadTransposeMatrixd(double[] m)
	{
		OpenGLDllImportBase.GetDelegateFor<glLoadTransposeMatrixdDelegate>()(m);
	}

	public static void glMultTransposeMatrixf(float[] m)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultTransposeMatrixfDelegate>()(m);
	}

	public static void glMultTransposeMatrixd(double[] m)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultTransposeMatrixdDelegate>()(m);
	}

	public static void glBlendFuncSeparate(uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha)
	{
		OpenGLDllImportBase.GetDelegateFor<glBlendFuncSeparateDelegate>()(sfactorRGB, dfactorRGB, sfactorAlpha, dfactorAlpha);
	}

	public static void glMultiDrawArrays(uint mode, int[] first, int[] count, int drawcount)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiDrawArraysDelegate>()(mode, first, count, drawcount);
	}

	public static void glMultiDrawElements(uint mode, int[] count, uint type, IntPtr indices, int drawcount)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiDrawElementsDelegate>()(mode, count, type, indices, drawcount);
	}

	public static void glPointParameterf(uint pname, float param)
	{
		OpenGLDllImportBase.GetDelegateFor<glPointParameterfDelegate>()(pname, param);
	}

	public static void glPointParameterfv(uint pname, float[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glPointParameterfvDelegate>()(pname, @params);
	}

	public static void glPointParameteri(uint pname, int param)
	{
		OpenGLDllImportBase.GetDelegateFor<glPointParameteriDelegate>()(pname, param);
	}

	public static void glPointParameteriv(uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glPointParameterivDelegate>()(pname, @params);
	}

	public static void glFogCoordf(float coord)
	{
		OpenGLDllImportBase.GetDelegateFor<glFogCoordfDelegate>()(coord);
	}

	public static void glFogCoordfv(float[] coord)
	{
		OpenGLDllImportBase.GetDelegateFor<glFogCoordfvDelegate>()(coord);
	}

	public static void glFogCoordd(double coord)
	{
		OpenGLDllImportBase.GetDelegateFor<glFogCoorddDelegate>()(coord);
	}

	public static void glFogCoorddv(double[] coord)
	{
		OpenGLDllImportBase.GetDelegateFor<glFogCoorddvDelegate>()(coord);
	}

	public static void glFogCoordPointer(uint type, int stride, IntPtr pointer)
	{
		OpenGLDllImportBase.GetDelegateFor<glFogCoordPointerDelegate>()(type, stride, pointer);
	}

	public static void glSecondaryColor3b(byte red, byte green, byte blue)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3bDelegate>()(red, green, blue);
	}

	public static void glSecondaryColor3bv(byte[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3bvDelegate>()(v);
	}

	public static void glSecondaryColor3d(double red, double green, double blue)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3dDelegate>()(red, green, blue);
	}

	public static void glSecondaryColor3dv(double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3dvDelegate>()(v);
	}

	public static void glSecondaryColor3f(float red, float green, float blue)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3fDelegate>()(red, green, blue);
	}

	public static void glSecondaryColor3fv(float[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3fvDelegate>()(v);
	}

	public static void glSecondaryColor3i(int red, int green, int blue)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3iDelegate>()(red, green, blue);
	}

	public static void glSecondaryColor3iv(int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3ivDelegate>()(v);
	}

	public static void glSecondaryColor3s(short red, short green, short blue)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3sDelegate>()(red, green, blue);
	}

	public static void glSecondaryColor3sv(short[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3svDelegate>()(v);
	}

	public static void glSecondaryColor3ub(byte red, byte green, byte blue)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3ubDelegate>()(red, green, blue);
	}

	public static void glSecondaryColor3ubv(byte[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3ubvDelegate>()(v);
	}

	public static void glSecondaryColor3ui(uint red, uint green, uint blue)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3uiDelegate>()(red, green, blue);
	}

	public static void glSecondaryColor3uiv(uint[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3uivDelegate>()(v);
	}

	public static void glSecondaryColor3us(ushort red, ushort green, ushort blue)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3usDelegate>()(red, green, blue);
	}

	public static void glSecondaryColor3usv(ushort[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColor3usvDelegate>()(v);
	}

	public static void glSecondaryColorPointer(int size, uint type, int stride, IntPtr pointer)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColorPointerDelegate>()(size, type, stride, pointer);
	}

	public static void glWindowPos2d(double x, double y)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos2dDelegate>()(x, y);
	}

	public static void glWindowPos2dv(double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos2dvDelegate>()(v);
	}

	public static void glWindowPos2f(float x, float y)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos2fDelegate>()(x, y);
	}

	public static void glWindowPos2fv(float[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos2fvDelegate>()(v);
	}

	public static void glWindowPos2i(int x, int y)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos2iDelegate>()(x, y);
	}

	public static void glWindowPos2iv(int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos2ivDelegate>()(v);
	}

	public static void glWindowPos2s(short x, short y)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos2sDelegate>()(x, y);
	}

	public static void glWindowPos2sv(short[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos2svDelegate>()(v);
	}

	public static void glWindowPos3d(double x, double y, double z)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos3dDelegate>()(x, y, z);
	}

	public static void glWindowPos3dv(double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos3dvDelegate>()(v);
	}

	public static void glWindowPos3f(float x, float y, float z)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos3fDelegate>()(x, y, z);
	}

	public static void glWindowPos3fv(float[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos3fvDelegate>()(v);
	}

	public static void glWindowPos3i(int x, int y, int z)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos3iDelegate>()(x, y, z);
	}

	public static void glWindowPos3iv(int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos3ivDelegate>()(v);
	}

	public static void glWindowPos3s(short x, short y, short z)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos3sDelegate>()(x, y, z);
	}

	public static void glWindowPos3sv(short[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glWindowPos3svDelegate>()(v);
	}

	public static void glBlendColor(float red, float green, float blue, float alpha)
	{
		OpenGLDllImportBase.GetDelegateFor<glBlendColorDelegate>()(red, green, blue, alpha);
	}

	public static void glBlendEquation(uint mode)
	{
		OpenGLDllImportBase.GetDelegateFor<glBlendEquationDelegate>()(mode);
	}

	public static void glGenQueries(int n, uint[] ids)
	{
		OpenGLDllImportBase.GetDelegateFor<glGenQueriesDelegate>()(n, ids);
	}

	public static void glDeleteQueries(int n, uint[] ids)
	{
		OpenGLDllImportBase.GetDelegateFor<glDeleteQueriesDelegate>()(n, ids);
	}

	public static bool glIsQuery(uint id)
	{
		return (bool)OpenGLDllImportBase.GetDelegateFor<glIsQueryDelegate>()(id);
	}

	public static void glBeginQuery(uint target, uint id)
	{
		OpenGLDllImportBase.GetDelegateFor<glBeginQueryDelegate>()(target, id);
	}

	public static void glEndQuery(uint target)
	{
		OpenGLDllImportBase.GetDelegateFor<glEndQueryDelegate>()(target);
	}

	public static void glGetQueryiv(uint target, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetQueryivDelegate>()(target, pname, @params);
	}

	public static void glGetQueryObjectiv(uint id, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetQueryObjectivDelegate>()(id, pname, @params);
	}

	public static void glGetQueryObjectuiv(uint id, uint pname, uint[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetQueryObjectuivDelegate>()(id, pname, @params);
	}

	public static void glBindBuffer(uint target, uint buffer)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindBufferDelegate>()(target, buffer);
	}

	public static void glDeleteBuffers(int n, uint[] buffers)
	{
		OpenGLDllImportBase.GetDelegateFor<glDeleteBuffersDelegate>()(n, buffers);
	}

	public static void glGenBuffers(int n, uint[] buffers)
	{
		OpenGLDllImportBase.GetDelegateFor<glGenBuffersDelegate>()(n, buffers);
	}

	public static bool glIsBuffer(uint buffer)
	{
		return (bool)OpenGLDllImportBase.GetDelegateFor<glIsBufferDelegate>()(buffer);
	}

	public static void glBufferData(uint target, int size, float[] data, uint usage)
	{
		OpenGLDllImportBase.GetDelegateFor<glBufferDataFloatDelegate>("glBufferData")(target, size, data, usage);
	}

	public static void glBufferData(uint target, int size, uint[] data, uint usage)
	{
		OpenGLDllImportBase.GetDelegateFor<glBufferDataUintDelegate>("glBufferData")(target, size, data, usage);
	}

	public static void glBufferData(uint target, int size, int[] data, uint usage)
	{
		OpenGLDllImportBase.GetDelegateFor<glBufferDataIntDelegate>("glBufferData")(target, size, data, usage);
	}

	public static void glBufferData(uint target, int size, double[] data, uint usage)
	{
		OpenGLDllImportBase.GetDelegateFor<glBufferDataDoubleDelegate>("glBufferData")(target, size, data, usage);
	}

	public static void glBufferSubData(uint target, int offset, int size, float[] data)
	{
		OpenGLDllImportBase.GetDelegateFor<glBufferSubDataDelegate>()(target, offset, size, data);
	}

	public static void glGetBufferSubData(uint target, IntPtr offset, IntPtr size, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetBufferSubDataDelegate>()(target, offset, size, data);
	}

	public static void glMapBuffer(uint target, uint access)
	{
		OpenGLDllImportBase.GetDelegateFor<glMapBufferDelegate>()(target, access);
	}

	public static bool glUnmapBuffer(uint target)
	{
		return (bool)OpenGLDllImportBase.GetDelegateFor<glUnmapBufferDelegate>()(target);
	}

	public static void glGetBufferParameteriv(uint target, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetBufferParameterivDelegate>()(target, pname, @params);
	}

	public static void glGetBufferPointerv(uint target, uint pname, IntPtr @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetBufferPointervDelegate>()(target, pname, @params);
	}

	public static void glBlendEquationSeparate(uint modeRGB, uint modeAlpha)
	{
		OpenGLDllImportBase.GetDelegateFor<glBlendEquationSeparateDelegate>()(modeRGB, modeAlpha);
	}

	public static void glDrawBuffers(int n, uint[] bufs)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawBuffersDelegate>()(n, bufs);
	}

	public static void glStencilOpSeparate(uint face, uint sfail, uint dpfail, uint dppass)
	{
		OpenGLDllImportBase.GetDelegateFor<glStencilOpSeparateDelegate>()(face, sfail, dpfail, dppass);
	}

	public static void glStencilFuncSeparate(uint face, uint func, int @ref, uint mask)
	{
		OpenGLDllImportBase.GetDelegateFor<glStencilFuncSeparateDelegate>()(face, func, @ref, mask);
	}

	public static void glStencilMaskSeparate(uint face, uint mask)
	{
		OpenGLDllImportBase.GetDelegateFor<glStencilMaskSeparateDelegate>()(face, mask);
	}

	public static void glAttachShader(uint program, uint shader)
	{
		OpenGLDllImportBase.GetDelegateFor<glAttachShaderDelegate>()(program, shader);
	}

	public static void glBindAttribLocation(uint program, uint index, string[] name)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindAttribLocationDelegate>()(program, index, name);
	}

	public static void glCompileShader(uint shader)
	{
		OpenGLDllImportBase.GetDelegateFor<glCompileShaderDelegate>()(shader);
	}

	public static uint glCreateProgram()
	{
		return (uint)OpenGLDllImportBase.GetDelegateFor<glCreateProgramDelegate>()();
	}

	public static uint glCreateShader(uint type)
	{
		return (uint)OpenGLDllImportBase.GetDelegateFor<glCreateShaderDelegate>()(type);
	}

	public static void glDeleteProgram(uint program)
	{
		OpenGLDllImportBase.GetDelegateFor<glDeleteProgramDelegate>()(program);
	}

	public static void glDeleteShader(uint shader)
	{
		OpenGLDllImportBase.GetDelegateFor<glDeleteShaderDelegate>()(shader);
	}

	public static void glDetachShader(uint program, uint shader)
	{
		OpenGLDllImportBase.GetDelegateFor<glDetachShaderDelegate>()(program, shader);
	}

	public static void glDisableVertexAttribArray(uint index)
	{
		OpenGLDllImportBase.GetDelegateFor<glDisableVertexAttribArrayDelegate>()(index);
	}

	public static void glEnableVertexAttribArray(uint index)
	{
		OpenGLDllImportBase.GetDelegateFor<glEnableVertexAttribArrayDelegate>()(index);
	}

	public static void glGetActiveAttrib(uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string[] name)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetActiveAttribDelegate>()(program, index, bufSize, length, size, type, name);
	}

	public static void glGetActiveUniform(uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string[] name)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetActiveUniformDelegate>()(program, index, bufSize, length, size, type, name);
	}

	public static void glGetAttachedShaders(uint program, int maxCount, int[] count, uint[] shaders)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetAttachedShadersDelegate>()(program, maxCount, count, shaders);
	}

	public static uint glGetAttribLocation(uint program, string name)
	{
		return (uint)OpenGLDllImportBase.GetDelegateFor<glGetAttribLocationDelegate>()(program, name);
	}

	public static void glGetProgramiv(uint program, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetProgramivDelegate>()(program, pname, @params);
	}

	public static void glGetProgramInfoLog(uint program, int bufSize, ref int length, byte[] infoLog)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetProgramInfoLogDelegate>()(program, bufSize, ref length, infoLog);
	}

	public static void glGetShaderiv(uint shader, uint pname, ref int @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetShaderivDelegate>()(shader, pname, ref @params);
	}

	public static void glGetShaderInfoLog(uint shader, int bufSize, ref int length, byte[] infoLog)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetShaderInfoLogDelegate>()(shader, bufSize, ref length, infoLog);
	}

	public static void glGetShaderSource(uint shader, int bufSize, int[] length, char[] source)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetShaderSourceDelegate>()(shader, bufSize, length, source);
	}

	public static uint glGetUniformLocation(uint program, string name)
	{
		return (uint)OpenGLDllImportBase.GetDelegateFor<glGetUniformLocationDelegate>()(program, name);
	}

	public static void glGetUniformfv(uint program, uint location, float[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetUniformfvDelegate>()(program, location, @params);
	}

	public static void glGetUniformiv(uint program, uint location, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetUniformivDelegate>()(program, location, @params);
	}

	public static void glGetVertexAttribdv(uint index, uint pname, double[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetVertexAttribdvDelegate>()(index, pname, @params);
	}

	public static void glGetVertexAttribfv(uint index, uint pname, float[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetVertexAttribfvDelegate>()(index, pname, @params);
	}

	public static void glGetVertexAttribiv(uint index, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetVertexAttribivDelegate>()(index, pname, @params);
	}

	public static void glGetVertexAttribPointerv(uint index, uint pname, IntPtr pointer)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetVertexAttribPointervDelegate>()(index, pname, pointer);
	}

	public static bool glIsProgram(uint program)
	{
		return (bool)OpenGLDllImportBase.GetDelegateFor<glIsProgramDelegate>()(program);
	}

	public static bool glIsShader(uint shader)
	{
		return (bool)OpenGLDllImportBase.GetDelegateFor<glIsShaderDelegate>()(shader);
	}

	public static void glLinkProgram(uint program)
	{
		OpenGLDllImportBase.GetDelegateFor<glLinkProgramDelegate>()(program);
	}

	public static void glShaderSource(uint shader, int count, string[] @string, int[] length)
	{
		OpenGLDllImportBase.GetDelegateFor<glShaderSourceDelegate>()(shader, count, @string, length);
	}

	public static void glUseProgram(uint program)
	{
		OpenGLDllImportBase.GetDelegateFor<glUseProgramDelegate>()(program);
	}

	public static void glUniform1f(uint location, float v0)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform1fDelegate>()(location, v0);
	}

	public static void glUniform2f(uint location, float v0, float v1)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform2fDelegate>()(location, v0, v1);
	}

	public static void glUniform3f(uint location, float v0, float v1, float v2)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform3fDelegate>()(location, v0, v1, v2);
	}

	public static void glUniform4f(uint location, float v0, float v1, float v2, float v3)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform4fDelegate>()(location, v0, v1, v2, v3);
	}

	public static void glUniform1i(uint location, int v0)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform1iDelegate>()(location, v0);
	}

	public static void glUniform2i(uint location, int v0, int v1)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform2iDelegate>()(location, v0, v1);
	}

	public static void glUniform3i(uint location, int v0, int v1, int v2)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform3iDelegate>()(location, v0, v1, v2);
	}

	public static void glUniform4i(uint location, int v0, int v1, int v2, int v3)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform4iDelegate>()(location, v0, v1, v2, v3);
	}

	public static void glUniform1fv(uint location, int count, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform1fvDelegate>()(location, count, value);
	}

	public static void glUniform2fv(uint location, int count, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform2fvDelegate>()(location, count, value);
	}

	public static void glUniform3fv(uint location, int count, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform3fvDelegate>()(location, count, value);
	}

	public static void glUniform4fv(uint location, int count, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform4fvDelegate>()(location, count, value);
	}

	public static void glUniform1iv(uint location, int count, int[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform1ivDelegate>()(location, count, value);
	}

	public static void glUniform2iv(uint location, int count, int[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform2ivDelegate>()(location, count, value);
	}

	public static void glUniform3iv(uint location, int count, int[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform3ivDelegate>()(location, count, value);
	}

	public static void glUniform4iv(uint location, int count, int[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform4ivDelegate>()(location, count, value);
	}

	public static void glUniformMatrix2fv(uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix2fvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix3fv(uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix3fvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix4fv(uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix4fvDelegate>()(location, count, transpose, value);
	}

	public static void glValidateProgram(uint program)
	{
		OpenGLDllImportBase.GetDelegateFor<glValidateProgramDelegate>()(program);
	}

	public static void glVertexAttrib1d(uint index, double x)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib1dDelegate>()(index, x);
	}

	public static void glVertexAttrib1dv(uint index, double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib1dvDelegate>()(index, v);
	}

	public static void glVertexAttrib1f(uint index, float x)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib1fDelegate>()(index, x);
	}

	public static void glVertexAttrib1fv(uint index, float[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib1fvDelegate>()(index, v);
	}

	public static void glVertexAttrib1s(uint index, short x)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib1sDelegate>()(index, x);
	}

	public static void glVertexAttrib1sv(uint index, short[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib1svDelegate>()(index, v);
	}

	public static void glVertexAttrib2d(uint index, double x, double y)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib2dDelegate>()(index, x, y);
	}

	public static void glVertexAttrib2dv(uint index, double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib2dvDelegate>()(index, v);
	}

	public static void glVertexAttrib2f(uint index, float x, float y)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib2fDelegate>()(index, x, y);
	}

	public static void glVertexAttrib2fv(uint index, float[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib2fvDelegate>()(index, v);
	}

	public static void glVertexAttrib2s(uint index, short x, short y)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib2sDelegate>()(index, x, y);
	}

	public static void glVertexAttrib2sv(uint index, short[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib2svDelegate>()(index, v);
	}

	public static void glVertexAttrib3d(uint index, double x, double y, double z)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib3dDelegate>()(index, x, y, z);
	}

	public static void glVertexAttrib3dv(uint index, double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib3dvDelegate>()(index, v);
	}

	public static void glVertexAttrib3f(uint index, float x, float y, float z)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib3fDelegate>()(index, x, y, z);
	}

	public static void glVertexAttrib3fv(uint index, float[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib3fvDelegate>()(index, v);
	}

	public static void glVertexAttrib3s(uint index, short x, short y, short z)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib3sDelegate>()(index, x, y, z);
	}

	public static void glVertexAttrib3sv(uint index, short[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib3svDelegate>()(index, v);
	}

	public static void glVertexAttrib4Nbv(uint index, byte[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4NbvDelegate>()(index, v);
	}

	public static void glVertexAttrib4Niv(uint index, int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4NivDelegate>()(index, v);
	}

	public static void glVertexAttrib4Nsv(uint index, short[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4NsvDelegate>()(index, v);
	}

	public static void glVertexAttrib4Nub(uint index, byte x, byte y, byte z, byte w)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4NubDelegate>()(index, x, y, z, w);
	}

	public static void glVertexAttrib4Nubv(uint index, byte[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4NubvDelegate>()(index, v);
	}

	public static void glVertexAttrib4Nuiv(uint index, uint[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4NuivDelegate>()(index, v);
	}

	public static void glVertexAttrib4Nusv(uint index, ushort[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4NusvDelegate>()(index, v);
	}

	public static void glVertexAttrib4bv(uint index, byte[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4bvDelegate>()(index, v);
	}

	public static void glVertexAttrib4d(uint index, double x, double y, double z, double w)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4dDelegate>()(index, x, y, z, w);
	}

	public static void glVertexAttrib4dv(uint index, double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4dvDelegate>()(index, v);
	}

	public static void glVertexAttrib4f(uint index, float x, float y, float z, float w)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4fDelegate>()(index, x, y, z, w);
	}

	public static void glVertexAttrib4fv(uint index, float[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4fvDelegate>()(index, v);
	}

	public static void glVertexAttrib4iv(uint index, int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4ivDelegate>()(index, v);
	}

	public static void glVertexAttrib4s(uint index, short x, short y, short z, short w)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4sDelegate>()(index, x, y, z, w);
	}

	public static void glVertexAttrib4sv(uint index, short[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4svDelegate>()(index, v);
	}

	public static void glVertexAttrib4ubv(uint index, byte[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4ubvDelegate>()(index, v);
	}

	public static void glVertexAttrib4uiv(uint index, uint[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4uivDelegate>()(index, v);
	}

	public static void glVertexAttrib4usv(uint index, ushort[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttrib4usvDelegate>()(index, v);
	}

	public static void glVertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribPointerDelegate>()(index, size, type, normalized, stride, pointer);
	}

	public static void glUniformMatrix2x3fv(uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix2x3fvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix3x2fv(uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix3x2fvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix2x4fv(uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix2x4fvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix4x2fv(uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix4x2fvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix3x4fv(uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix3x4fvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix4x3fv(uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix4x3fvDelegate>()(location, count, transpose, value);
	}

	public static void glColorMaski(uint index, bool r, bool g, bool b, bool a)
	{
		OpenGLDllImportBase.GetDelegateFor<glColorMaskiDelegate>()(index, r, g, b, a);
	}

	public static void glGetBooleani_v(uint target, uint index, bool[] data)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetBooleani_vDelegate>()(target, index, data);
	}

	public static void glGetIntegeri_v(uint target, uint index, int[] data)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetIntegeri_vDelegate>()(target, index, data);
	}

	public static void glEnablei(uint target, uint index)
	{
		OpenGLDllImportBase.GetDelegateFor<glEnableiDelegate>()(target, index);
	}

	public static void glDisablei(uint target, uint index)
	{
		OpenGLDllImportBase.GetDelegateFor<glDisableiDelegate>()(target, index);
	}

	public static bool glIsEnabledi(uint target, uint index)
	{
		return (bool)OpenGLDllImportBase.GetDelegateFor<glIsEnablediDelegate>()(target, index);
	}

	public static void glBeginTransformFeedback(uint primitiveMode)
	{
		OpenGLDllImportBase.GetDelegateFor<glBeginTransformFeedbackDelegate>()(primitiveMode);
	}

	public static void glEndTransformFeedback()
	{
		OpenGLDllImportBase.GetDelegateFor<glEndTransformFeedbackDelegate>()();
	}

	public static void glBindBufferRange(uint target, uint index, uint buffer, IntPtr offset, IntPtr size)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindBufferRangeDelegate>()(target, index, buffer, offset, size);
	}

	public static void glBindBufferBase(uint target, uint index, uint buffer)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindBufferBaseDelegate>()(target, index, buffer);
	}

	public static void glTransformFeedbackVaryings(uint program, int count, char[] varyings, uint bufferMode)
	{
		OpenGLDllImportBase.GetDelegateFor<glTransformFeedbackVaryingsDelegate>()(program, count, varyings, bufferMode);
	}

	public static void glGetTransformFeedbackVarying(uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, char[] name)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetTransformFeedbackVaryingDelegate>()(program, index, bufSize, length, size, type, name);
	}

	public static void glClampColor(uint target, uint clamp)
	{
		OpenGLDllImportBase.GetDelegateFor<glClampColorDelegate>()(target, clamp);
	}

	public static void glBeginConditionalRender(uint id, uint mode)
	{
		OpenGLDllImportBase.GetDelegateFor<glBeginConditionalRenderDelegate>()(id, mode);
	}

	public static void glEndConditionalRender()
	{
		OpenGLDllImportBase.GetDelegateFor<glEndConditionalRenderDelegate>()();
	}

	public static void glVertexAttribIPointer(uint index, int size, uint type, int stride, IntPtr pointer)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribIPointerDelegate>()(index, size, type, stride, pointer);
	}

	public static void glGetVertexAttribIiv(uint index, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetVertexAttribIivDelegate>()(index, pname, @params);
	}

	public static void glGetVertexAttribIuiv(uint index, uint pname, uint[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetVertexAttribIuivDelegate>()(index, pname, @params);
	}

	public static void glVertexAttribI1i(uint index, int x)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI1iDelegate>()(index, x);
	}

	public static void glVertexAttribI2i(uint index, int x, int y)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI2iDelegate>()(index, x, y);
	}

	public static void glVertexAttribI3i(uint index, int x, int y, int z)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI3iDelegate>()(index, x, y, z);
	}

	public static void glVertexAttribI4i(uint index, int x, int y, int z, int w)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI4iDelegate>()(index, x, y, z, w);
	}

	public static void glVertexAttribI1ui(uint index, uint x)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI1uiDelegate>()(index, x);
	}

	public static void glVertexAttribI2ui(uint index, uint x, uint y)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI2uiDelegate>()(index, x, y);
	}

	public static void glVertexAttribI3ui(uint index, uint x, uint y, uint z)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI3uiDelegate>()(index, x, y, z);
	}

	public static void glVertexAttribI4ui(uint index, uint x, uint y, uint z, uint w)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI4uiDelegate>()(index, x, y, z, w);
	}

	public static void glVertexAttribI1iv(uint index, int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI1ivDelegate>()(index, v);
	}

	public static void glVertexAttribI2iv(uint index, int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI2ivDelegate>()(index, v);
	}

	public static void glVertexAttribI3iv(uint index, int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI3ivDelegate>()(index, v);
	}

	public static void glVertexAttribI4iv(uint index, int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI4ivDelegate>()(index, v);
	}

	public static void glVertexAttribI1uiv(uint index, uint[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI1uivDelegate>()(index, v);
	}

	public static void glVertexAttribI2uiv(uint index, uint[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI2uivDelegate>()(index, v);
	}

	public static void glVertexAttribI3uiv(uint index, uint[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI3uivDelegate>()(index, v);
	}

	public static void glVertexAttribI4uiv(uint index, uint[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI4uivDelegate>()(index, v);
	}

	public static void glVertexAttribI4bv(uint index, byte[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI4bvDelegate>()(index, v);
	}

	public static void glVertexAttribI4sv(uint index, short[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI4svDelegate>()(index, v);
	}

	public static void glVertexAttribI4ubv(uint index, byte[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI4ubvDelegate>()(index, v);
	}

	public static void glVertexAttribI4usv(uint index, ushort[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribI4usvDelegate>()(index, v);
	}

	public static void glGetUniformuiv(uint program, uint location, uint[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetUniformuivDelegate>()(program, location, @params);
	}

	public static void glBindFragDataLocation(uint program, uint color, char[] name)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindFragDataLocationDelegate>()(program, color, name);
	}

	public static int glGetFragDataLocation(uint program, char[] name)
	{
		return (int)OpenGLDllImportBase.GetDelegateFor<glGetFragDataLocationDelegate>()(program, name);
	}

	public static void glUniform1ui(uint location, uint v0)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform1uiDelegate>()(location, v0);
	}

	public static void glUniform2ui(uint location, uint v0, uint v1)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform2uiDelegate>()(location, v0, v1);
	}

	public static void glUniform3ui(uint location, uint v0, uint v1, uint v2)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform3uiDelegate>()(location, v0, v1, v2);
	}

	public static void glUniform4ui(uint location, uint v0, uint v1, uint v2, uint v3)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform4uiDelegate>()(location, v0, v1, v2, v3);
	}

	public static void glUniform1uiv(uint location, int count, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform1uivDelegate>()(location, count, value);
	}

	public static void glUniform2uiv(uint location, int count, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform2uivDelegate>()(location, count, value);
	}

	public static void glUniform3uiv(uint location, int count, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform3uivDelegate>()(location, count, value);
	}

	public static void glUniform4uiv(uint location, int count, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform4uivDelegate>()(location, count, value);
	}

	public static void glTexParameterIiv(uint target, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexParameterIivDelegate>()(target, pname, @params);
	}

	public static void glTexParameterIuiv(uint target, uint pname, uint[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexParameterIuivDelegate>()(target, pname, @params);
	}

	public static void glGetTexParameterIiv(uint target, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetTexParameterIivDelegate>()(target, pname, @params);
	}

	public static void glGetTexParameterIuiv(uint target, uint pname, uint[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetTexParameterIuivDelegate>()(target, pname, @params);
	}

	public static void glClearBufferiv(uint buffer, int drawbuffer, int[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearBufferivDelegate>()(buffer, drawbuffer, value);
	}

	public static void glClearBufferuiv(uint buffer, int drawbuffer, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearBufferuivDelegate>()(buffer, drawbuffer, value);
	}

	public static void glClearBufferfv(uint buffer, int drawbuffer, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearBufferfvDelegate>()(buffer, drawbuffer, value);
	}

	public static void glClearBufferfi(uint buffer, int drawbuffer, float depth, int stencil)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearBufferfiDelegate>()(buffer, drawbuffer, depth, stencil);
	}

	public static bool glIsRenderbuffer(uint renderbuffer)
	{
		return (bool)OpenGLDllImportBase.GetDelegateFor<glIsRenderbufferDelegate>()(renderbuffer);
	}

	public static void glBindRenderbuffer(uint target, uint renderbuffer)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindRenderbufferDelegate>()(target, renderbuffer);
	}

	public static void glDeleteRenderbuffers(int n, uint[] renderbuffers)
	{
		OpenGLDllImportBase.GetDelegateFor<glDeleteRenderbuffersDelegate>()(n, renderbuffers);
	}

	public static void glGenRenderbuffers(int n, uint[] renderbuffers)
	{
		OpenGLDllImportBase.GetDelegateFor<glGenRenderbuffersDelegate>()(n, renderbuffers);
	}

	public static void glRenderbufferStorage(uint target, uint internalformat, int width, int height)
	{
		OpenGLDllImportBase.GetDelegateFor<glRenderbufferStorageDelegate>()(target, internalformat, width, height);
	}

	public static void glGetRenderbufferParameteriv(uint target, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetRenderbufferParameterivDelegate>()(target, pname, @params);
	}

	public static bool glIsFramebuffer(uint framebuffer)
	{
		return (bool)OpenGLDllImportBase.GetDelegateFor<glIsFramebufferDelegate>()(framebuffer);
	}

	public static void glBindFramebuffer(uint target, uint framebuffer)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindFramebufferDelegate>()(target, framebuffer);
	}

	public static void glDeleteFramebuffers(int n, uint[] framebuffers)
	{
		OpenGLDllImportBase.GetDelegateFor<glDeleteFramebuffersDelegate>()(n, framebuffers);
	}

	public static void glGenFramebuffers(int n, uint[] framebuffers)
	{
		OpenGLDllImportBase.GetDelegateFor<glGenFramebuffersDelegate>()(n, framebuffers);
	}

	public static uint glCheckFramebufferStatus(uint target)
	{
		return (uint)OpenGLDllImportBase.GetDelegateFor<glCheckFramebufferStatusDelegate>()(target);
	}

	public static void glFramebufferTexture1D(uint target, uint attachment, uint textarget, uint texture, int level)
	{
		OpenGLDllImportBase.GetDelegateFor<glFramebufferTexture1DDelegate>()(target, attachment, textarget, texture, level);
	}

	public static void glFramebufferTexture2D(uint target, uint attachment, uint textarget, uint texture, int level)
	{
		OpenGLDllImportBase.GetDelegateFor<glFramebufferTexture2DDelegate>()(target, attachment, textarget, texture, level);
	}

	public static void glFramebufferTexture3D(uint target, uint attachment, uint textarget, uint texture, int level, int zoffset)
	{
		OpenGLDllImportBase.GetDelegateFor<glFramebufferTexture3DDelegate>()(target, attachment, textarget, texture, level, zoffset);
	}

	public static void glFramebufferRenderbuffer(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer)
	{
		OpenGLDllImportBase.GetDelegateFor<glFramebufferRenderbufferDelegate>()(target, attachment, renderbuffertarget, renderbuffer);
	}

	public static void glGetFramebufferAttachmentParameteriv(uint target, uint attachment, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetFramebufferAttachmentParameterivDelegate>()(target, attachment, pname, @params);
	}

	public static void glGenerateMipmap(uint target)
	{
		OpenGLDllImportBase.GetDelegateFor<glGenerateMipmapDelegate>()(target);
	}

	public static void glBlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter)
	{
		OpenGLDllImportBase.GetDelegateFor<glBlitFramebufferDelegate>()(srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, mask, filter);
	}

	public static void glRenderbufferStorageMultisample(uint target, int samples, uint internalformat, int width, int height)
	{
		OpenGLDllImportBase.GetDelegateFor<glRenderbufferStorageMultisampleDelegate>()(target, samples, internalformat, width, height);
	}

	public static void glFramebufferTextureLayer(uint target, uint attachment, uint texture, int level, int layer)
	{
		OpenGLDllImportBase.GetDelegateFor<glFramebufferTextureLayerDelegate>()(target, attachment, texture, level, layer);
	}

	public static void glMapBufferRange(uint target, IntPtr offset, IntPtr length, uint access)
	{
		OpenGLDllImportBase.GetDelegateFor<glMapBufferRangeDelegate>()(target, offset, length, access);
	}

	public static void glFlushMappedBufferRange(uint target, IntPtr offset, IntPtr length)
	{
		OpenGLDllImportBase.GetDelegateFor<glFlushMappedBufferRangeDelegate>()(target, offset, length);
	}

	public static void glBindVertexArray(uint array)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindVertexArrayDelegate>()(array);
	}

	public static void glDeleteVertexArrays(int n, uint[] arrays)
	{
		OpenGLDllImportBase.GetDelegateFor<glDeleteVertexArraysDelegate>()(n, arrays);
	}

	public static void glGenVertexArrays(int n, uint[] arrays)
	{
		OpenGLDllImportBase.GetDelegateFor<glGenVertexArraysDelegate>()(n, arrays);
	}

	public static bool glIsVertexArray(uint array)
	{
		return (bool)OpenGLDllImportBase.GetDelegateFor<glIsVertexArrayDelegate>()(array);
	}

	public static void glDrawElementsBaseVertex(uint mode, int count, uint type, IntPtr indices, int basevertex)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawElementsBaseVertexDelegate>()(mode, count, type, indices, basevertex);
	}

	public static void glDrawRangeElementsBaseVertex(uint mode, uint start, uint end, int count, uint type, IntPtr indices, int basevertex)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawRangeElementsBaseVertexDelegate>()(mode, start, end, count, type, indices, basevertex);
	}

	public static void glDrawElementsInstancedBaseVertex(uint mode, int count, uint type, IntPtr indices, int instancecount, int basevertex)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawElementsInstancedBaseVertexDelegate>()(mode, count, type, indices, instancecount, basevertex);
	}

	public static void glMultiDrawElementsBaseVertex(uint mode, int[] count, uint type, IntPtr indices, int drawcount, int[] basevertex)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiDrawElementsBaseVertexDelegate>()(mode, count, type, indices, drawcount, basevertex);
	}

	public static void glProvokingVertex(uint mode)
	{
		OpenGLDllImportBase.GetDelegateFor<glProvokingVertexDelegate>()(mode);
	}

	public static IntPtr glFenceSync(uint condition, uint flags)
	{
		return (IntPtr)OpenGLDllImportBase.GetDelegateFor<glFenceSyncDelegate>()(condition, flags);
	}

	public static bool glIsSync(IntPtr sync)
	{
		return (bool)OpenGLDllImportBase.GetDelegateFor<glIsSyncDelegate>()(sync);
	}

	public static void glDeleteSync(IntPtr sync)
	{
		OpenGLDllImportBase.GetDelegateFor<glDeleteSyncDelegate>()(sync);
	}

	public static uint glClientWaitSync(IntPtr sync, uint flags, UInt64 timeout)
	{
		return (uint)OpenGLDllImportBase.GetDelegateFor<glClientWaitSyncDelegate>()(sync, flags, timeout);
	}

	public static void glWaitSync(IntPtr sync, uint flags, UInt64 timeout)
	{
		OpenGLDllImportBase.GetDelegateFor<glWaitSyncDelegate>()(sync, flags, timeout);
	}

	public static void glGetInteger64v(uint pname, Int64[] data)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetInteger64vDelegate>()(pname, data);
	}

	public static void glGetSynciv(IntPtr sync, uint pname, int bufSize, int[] length, int[] values)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetSyncivDelegate>()(sync, pname, bufSize, length, values);
	}

	public static void glGetInteger64i_v(uint target, uint index, Int64[] data)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetInteger64i_vDelegate>()(target, index, data);
	}

	public static void glGetBufferParameteri64v(uint target, uint pname, Int64[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetBufferParameteri64vDelegate>()(target, pname, @params);
	}

	public static void glFramebufferTexture(uint target, uint attachment, uint texture, int level)
	{
		OpenGLDllImportBase.GetDelegateFor<glFramebufferTextureDelegate>()(target, attachment, texture, level);
	}

	public static void glTexImage2DMultisample(uint target, int samples, uint internalformat, int width, int height, bool fixedsamplelocations)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexImage2DMultisampleDelegate>()(target, samples, internalformat, width, height, fixedsamplelocations);
	}

	public static void glTexImage3DMultisample(uint target, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexImage3DMultisampleDelegate>()(target, samples, internalformat, width, height, depth, fixedsamplelocations);
	}

	public static void glGetMultisamplefv(uint pname, uint index, float[] val)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetMultisamplefvDelegate>()(pname, index, val);
	}

	public static void glSampleMaski(uint maskNumber, uint mask)
	{
		OpenGLDllImportBase.GetDelegateFor<glSampleMaskiDelegate>()(maskNumber, mask);
	}

	public static void glDrawArraysInstanced(uint mode, int first, int count, int instancecount)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawArraysInstancedDelegate>()(mode, first, count, instancecount);
	}

	public static void glDrawElementsInstanced(uint mode, int count, uint type, IntPtr indices, int instancecount)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawElementsInstancedDelegate>()(mode, count, type, indices, instancecount);
	}

	public static void glTexBuffer(uint target, uint internalformat, uint buffer)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexBufferDelegate>()(target, internalformat, buffer);
	}

	public static void glPrimitiveRestartIndex(uint index)
	{
		OpenGLDllImportBase.GetDelegateFor<glPrimitiveRestartIndexDelegate>()(index);
	}

	public static void glCopyBufferSubData(uint readTarget, uint writeTarget, IntPtr readOffset, IntPtr writeOffset, IntPtr size)
	{
		OpenGLDllImportBase.GetDelegateFor<glCopyBufferSubDataDelegate>()(readTarget, writeTarget, readOffset, writeOffset, size);
	}

	public static void glGetUniformIndices(uint program, int uniformCount, char[] uniformNames, uint[] uniformIndices)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetUniformIndicesDelegate>()(program, uniformCount, uniformNames, uniformIndices);
	}

	public static void glGetActiveUniformsiv(uint program, int uniformCount, uint[] uniformIndices, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetActiveUniformsivDelegate>()(program, uniformCount, uniformIndices, pname, @params);
	}

	public static void glGetActiveUniformName(uint program, uint uniformIndex, int bufSize, int[] length, char[] uniformName)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetActiveUniformNameDelegate>()(program, uniformIndex, bufSize, length, uniformName);
	}

	public static uint glGetUniformBlockIndex(uint program, char[] uniformBlockName)
	{
		return (uint)OpenGLDllImportBase.GetDelegateFor<glGetUniformBlockIndexDelegate>()(program, uniformBlockName);
	}

	public static void glGetActiveUniformBlockiv(uint program, uint uniformBlockIndex, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetActiveUniformBlockivDelegate>()(program, uniformBlockIndex, pname, @params);
	}

	public static void glGetActiveUniformBlockName(uint program, uint uniformBlockIndex, int bufSize, int[] length, char[] uniformBlockName)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetActiveUniformBlockNameDelegate>()(program, uniformBlockIndex, bufSize, length, uniformBlockName);
	}

	public static void glUniformBlockBinding(uint program, uint uniformBlockIndex, uint uniformBlockBinding)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformBlockBindingDelegate>()(program, uniformBlockIndex, uniformBlockBinding);
	}

	public static void glBindFragDataLocationIndexed(uint program, uint colorNumber, uint index, char[] name)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindFragDataLocationIndexedDelegate>()(program, colorNumber, index, name);
	}

	public static int glGetFragDataIndex(uint program, char[] name)
	{
		return (int)OpenGLDllImportBase.GetDelegateFor<glGetFragDataIndexDelegate>()(program, name);
	}

	public static void glGenSamplers(int count, uint[] samplers)
	{
		OpenGLDllImportBase.GetDelegateFor<glGenSamplersDelegate>()(count, samplers);
	}

	public static void glDeleteSamplers(int count, uint[] samplers)
	{
		OpenGLDllImportBase.GetDelegateFor<glDeleteSamplersDelegate>()(count, samplers);
	}

	public static bool glIsSampler(uint sampler)
	{
		return (bool)OpenGLDllImportBase.GetDelegateFor<glIsSamplerDelegate>()(sampler);
	}

	public static void glBindSampler(uint unit, uint sampler)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindSamplerDelegate>()(unit, sampler);
	}

	public static void glSamplerParameteri(uint sampler, uint pname, int param)
	{
		OpenGLDllImportBase.GetDelegateFor<glSamplerParameteriDelegate>()(sampler, pname, param);
	}

	public static void glSamplerParameteriv(uint sampler, uint pname, int[] param)
	{
		OpenGLDllImportBase.GetDelegateFor<glSamplerParameterivDelegate>()(sampler, pname, param);
	}

	public static void glSamplerParameterf(uint sampler, uint pname, float param)
	{
		OpenGLDllImportBase.GetDelegateFor<glSamplerParameterfDelegate>()(sampler, pname, param);
	}

	public static void glSamplerParameterfv(uint sampler, uint pname, float[] param)
	{
		OpenGLDllImportBase.GetDelegateFor<glSamplerParameterfvDelegate>()(sampler, pname, param);
	}

	public static void glSamplerParameterIiv(uint sampler, uint pname, int[] param)
	{
		OpenGLDllImportBase.GetDelegateFor<glSamplerParameterIivDelegate>()(sampler, pname, param);
	}

	public static void glSamplerParameterIuiv(uint sampler, uint pname, uint[] param)
	{
		OpenGLDllImportBase.GetDelegateFor<glSamplerParameterIuivDelegate>()(sampler, pname, param);
	}

	public static void glGetSamplerParameteriv(uint sampler, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetSamplerParameterivDelegate>()(sampler, pname, @params);
	}

	public static void glGetSamplerParameterIiv(uint sampler, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetSamplerParameterIivDelegate>()(sampler, pname, @params);
	}

	public static void glGetSamplerParameterfv(uint sampler, uint pname, float[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetSamplerParameterfvDelegate>()(sampler, pname, @params);
	}

	public static void glGetSamplerParameterIuiv(uint sampler, uint pname, uint[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetSamplerParameterIuivDelegate>()(sampler, pname, @params);
	}

	public static void glQueryCounter(uint id, uint target)
	{
		OpenGLDllImportBase.GetDelegateFor<glQueryCounterDelegate>()(id, target);
	}

	public static void glGetQueryObjecti64v(uint id, uint pname, Int64[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetQueryObjecti64vDelegate>()(id, pname, @params);
	}

	public static void glGetQueryObjectui64v(uint id, uint pname, UInt64[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetQueryObjectui64vDelegate>()(id, pname, @params);
	}

	public static void glVertexAttribDivisor(uint index, uint divisor)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribDivisorDelegate>()(index, divisor);
	}

	public static void glVertexAttribP1ui(uint index, uint type, bool normalized, uint value)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribP1uiDelegate>()(index, type, normalized, value);
	}

	public static void glVertexAttribP1uiv(uint index, uint type, bool normalized, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribP1uivDelegate>()(index, type, normalized, value);
	}

	public static void glVertexAttribP2ui(uint index, uint type, bool normalized, uint value)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribP2uiDelegate>()(index, type, normalized, value);
	}

	public static void glVertexAttribP2uiv(uint index, uint type, bool normalized, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribP2uivDelegate>()(index, type, normalized, value);
	}

	public static void glVertexAttribP3ui(uint index, uint type, bool normalized, uint value)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribP3uiDelegate>()(index, type, normalized, value);
	}

	public static void glVertexAttribP3uiv(uint index, uint type, bool normalized, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribP3uivDelegate>()(index, type, normalized, value);
	}

	public static void glVertexAttribP4ui(uint index, uint type, bool normalized, uint value)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribP4uiDelegate>()(index, type, normalized, value);
	}

	public static void glVertexAttribP4uiv(uint index, uint type, bool normalized, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribP4uivDelegate>()(index, type, normalized, value);
	}

	public static void glVertexP2ui(uint type, uint value)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexP2uiDelegate>()(type, value);
	}

	public static void glVertexP2uiv(uint type, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexP2uivDelegate>()(type, value);
	}

	public static void glVertexP3ui(uint type, uint value)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexP3uiDelegate>()(type, value);
	}

	public static void glVertexP3uiv(uint type, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexP3uivDelegate>()(type, value);
	}

	public static void glVertexP4ui(uint type, uint value)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexP4uiDelegate>()(type, value);
	}

	public static void glVertexP4uiv(uint type, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexP4uivDelegate>()(type, value);
	}

	public static void glTexCoordP1ui(uint type, uint coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexCoordP1uiDelegate>()(type, coords);
	}

	public static void glTexCoordP1uiv(uint type, uint[] coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexCoordP1uivDelegate>()(type, coords);
	}

	public static void glTexCoordP2ui(uint type, uint coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexCoordP2uiDelegate>()(type, coords);
	}

	public static void glTexCoordP2uiv(uint type, uint[] coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexCoordP2uivDelegate>()(type, coords);
	}

	public static void glTexCoordP3ui(uint type, uint coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexCoordP3uiDelegate>()(type, coords);
	}

	public static void glTexCoordP3uiv(uint type, uint[] coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexCoordP3uivDelegate>()(type, coords);
	}

	public static void glTexCoordP4ui(uint type, uint coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexCoordP4uiDelegate>()(type, coords);
	}

	public static void glTexCoordP4uiv(uint type, uint[] coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexCoordP4uivDelegate>()(type, coords);
	}

	public static void glMultiTexCoordP1ui(uint texture, uint type, uint coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoordP1uiDelegate>()(texture, type, coords);
	}

	public static void glMultiTexCoordP1uiv(uint texture, uint type, uint[] coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoordP1uivDelegate>()(texture, type, coords);
	}

	public static void glMultiTexCoordP2ui(uint texture, uint type, uint coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoordP2uiDelegate>()(texture, type, coords);
	}

	public static void glMultiTexCoordP2uiv(uint texture, uint type, uint[] coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoordP2uivDelegate>()(texture, type, coords);
	}

	public static void glMultiTexCoordP3ui(uint texture, uint type, uint coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoordP3uiDelegate>()(texture, type, coords);
	}

	public static void glMultiTexCoordP3uiv(uint texture, uint type, uint[] coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoordP3uivDelegate>()(texture, type, coords);
	}

	public static void glMultiTexCoordP4ui(uint texture, uint type, uint coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoordP4uiDelegate>()(texture, type, coords);
	}

	public static void glMultiTexCoordP4uiv(uint texture, uint type, uint[] coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiTexCoordP4uivDelegate>()(texture, type, coords);
	}

	public static void glNormalP3ui(uint type, uint coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glNormalP3uiDelegate>()(type, coords);
	}

	public static void glNormalP3uiv(uint type, uint[] coords)
	{
		OpenGLDllImportBase.GetDelegateFor<glNormalP3uivDelegate>()(type, coords);
	}

	public static void glColorP3ui(uint type, uint color)
	{
		OpenGLDllImportBase.GetDelegateFor<glColorP3uiDelegate>()(type, color);
	}

	public static void glColorP3uiv(uint type, uint[] color)
	{
		OpenGLDllImportBase.GetDelegateFor<glColorP3uivDelegate>()(type, color);
	}

	public static void glColorP4ui(uint type, uint color)
	{
		OpenGLDllImportBase.GetDelegateFor<glColorP4uiDelegate>()(type, color);
	}

	public static void glColorP4uiv(uint type, uint[] color)
	{
		OpenGLDllImportBase.GetDelegateFor<glColorP4uivDelegate>()(type, color);
	}

	public static void glSecondaryColorP3ui(uint type, uint color)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColorP3uiDelegate>()(type, color);
	}

	public static void glSecondaryColorP3uiv(uint type, uint[] color)
	{
		OpenGLDllImportBase.GetDelegateFor<glSecondaryColorP3uivDelegate>()(type, color);
	}

	public static void glMinSampleShading(float value)
	{
		OpenGLDllImportBase.GetDelegateFor<glMinSampleShadingDelegate>()(value);
	}

	public static void glBlendEquationi(uint buf, uint mode)
	{
		OpenGLDllImportBase.GetDelegateFor<glBlendEquationiDelegate>()(buf, mode);
	}

	public static void glBlendEquationSeparatei(uint buf, uint modeRGB, uint modeAlpha)
	{
		OpenGLDllImportBase.GetDelegateFor<glBlendEquationSeparateiDelegate>()(buf, modeRGB, modeAlpha);
	}

	public static void glBlendFunci(uint buf, uint src, uint dst)
	{
		OpenGLDllImportBase.GetDelegateFor<glBlendFunciDelegate>()(buf, src, dst);
	}

	public static void glBlendFuncSeparatei(uint buf, uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha)
	{
		OpenGLDllImportBase.GetDelegateFor<glBlendFuncSeparateiDelegate>()(buf, srcRGB, dstRGB, srcAlpha, dstAlpha);
	}

	public static void glDrawArraysIndirect(uint mode, IntPtr indirect)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawArraysIndirectDelegate>()(mode, indirect);
	}

	public static void glDrawElementsIndirect(uint mode, uint type, IntPtr indirect)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawElementsIndirectDelegate>()(mode, type, indirect);
	}

	public static void glUniform1d(uint location, double x)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform1dDelegate>()(location, x);
	}

	public static void glUniform2d(uint location, double x, double y)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform2dDelegate>()(location, x, y);
	}

	public static void glUniform3d(uint location, double x, double y, double z)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform3dDelegate>()(location, x, y, z);
	}

	public static void glUniform4d(uint location, double x, double y, double z, double w)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform4dDelegate>()(location, x, y, z, w);
	}

	public static void glUniform1dv(uint location, int count, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform1dvDelegate>()(location, count, value);
	}

	public static void glUniform2dv(uint location, int count, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform2dvDelegate>()(location, count, value);
	}

	public static void glUniform3dv(uint location, int count, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform3dvDelegate>()(location, count, value);
	}

	public static void glUniform4dv(uint location, int count, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniform4dvDelegate>()(location, count, value);
	}

	public static void glUniformMatrix2dv(uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix2dvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix3dv(uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix3dvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix4dv(uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix4dvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix2x3dv(uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix2x3dvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix2x4dv(uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix2x4dvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix3x2dv(uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix3x2dvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix3x4dv(uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix3x4dvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix4x2dv(uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix4x2dvDelegate>()(location, count, transpose, value);
	}

	public static void glUniformMatrix4x3dv(uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformMatrix4x3dvDelegate>()(location, count, transpose, value);
	}

	public static void glGetUniformdv(uint program, uint location, double[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetUniformdvDelegate>()(program, location, @params);
	}

	public static int glGetSubroutineUniformLocation(uint program, uint shadertype, char[] name)
	{
		return (int)OpenGLDllImportBase.GetDelegateFor<glGetSubroutineUniformLocationDelegate>()(program, shadertype, name);
	}

	public static uint glGetSubroutineIndex(uint program, uint shadertype, char[] name)
	{
		return (uint)OpenGLDllImportBase.GetDelegateFor<glGetSubroutineIndexDelegate>()(program, shadertype, name);
	}

	public static void glGetActiveSubroutineUniformiv(uint program, uint shadertype, uint index, uint pname, int[] values)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetActiveSubroutineUniformivDelegate>()(program, shadertype, index, pname, values);
	}

	public static void glGetActiveSubroutineUniformName(uint program, uint shadertype, uint index, int bufsize, int[] length, char[] name)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetActiveSubroutineUniformNameDelegate>()(program, shadertype, index, bufsize, length, name);
	}

	public static void glGetActiveSubroutineName(uint program, uint shadertype, uint index, int bufsize, int[] length, char[] name)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetActiveSubroutineNameDelegate>()(program, shadertype, index, bufsize, length, name);
	}

	public static void glUniformSubroutinesuiv(uint shadertype, int count, uint[] indices)
	{
		OpenGLDllImportBase.GetDelegateFor<glUniformSubroutinesuivDelegate>()(shadertype, count, indices);
	}

	public static void glGetUniformSubroutineuiv(uint shadertype, uint location, uint[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetUniformSubroutineuivDelegate>()(shadertype, location, @params);
	}

	public static void glGetProgramStageiv(uint program, uint shadertype, uint pname, int[] values)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetProgramStageivDelegate>()(program, shadertype, pname, values);
	}

	public static void glPatchParameteri(uint pname, int value)
	{
		OpenGLDllImportBase.GetDelegateFor<glPatchParameteriDelegate>()(pname, value);
	}

	public static void glPatchParameterfv(uint pname, float[] values)
	{
		OpenGLDllImportBase.GetDelegateFor<glPatchParameterfvDelegate>()(pname, values);
	}

	public static void glBindTransformFeedback(uint target, uint id)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindTransformFeedbackDelegate>()(target, id);
	}

	public static void glDeleteTransformFeedbacks(int n, uint[] ids)
	{
		OpenGLDllImportBase.GetDelegateFor<glDeleteTransformFeedbacksDelegate>()(n, ids);
	}

	public static void glGenTransformFeedbacks(int n, uint[] ids)
	{
		OpenGLDllImportBase.GetDelegateFor<glGenTransformFeedbacksDelegate>()(n, ids);
	}

	public static bool glIsTransformFeedback(uint id)
	{
		return (bool)OpenGLDllImportBase.GetDelegateFor<glIsTransformFeedbackDelegate>()(id);
	}

	public static void glPauseTransformFeedback()
	{
		OpenGLDllImportBase.GetDelegateFor<glPauseTransformFeedbackDelegate>()();
	}

	public static void glResumeTransformFeedback()
	{
		OpenGLDllImportBase.GetDelegateFor<glResumeTransformFeedbackDelegate>()();
	}

	public static void glDrawTransformFeedback(uint mode, uint id)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawTransformFeedbackDelegate>()(mode, id);
	}

	public static void glDrawTransformFeedbackStream(uint mode, uint id, uint stream)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawTransformFeedbackStreamDelegate>()(mode, id, stream);
	}

	public static void glBeginQueryIndexed(uint target, uint index, uint id)
	{
		OpenGLDllImportBase.GetDelegateFor<glBeginQueryIndexedDelegate>()(target, index, id);
	}

	public static void glEndQueryIndexed(uint target, uint index)
	{
		OpenGLDllImportBase.GetDelegateFor<glEndQueryIndexedDelegate>()(target, index);
	}

	public static void glGetQueryIndexediv(uint target, uint index, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetQueryIndexedivDelegate>()(target, index, pname, @params);
	}

	public static void glReleaseShaderCompiler()
	{
		OpenGLDllImportBase.GetDelegateFor<glReleaseShaderCompilerDelegate>()();
	}

	public static void glShaderBinary(int count, uint[] shaders, uint binaryformat, IntPtr binary, int length)
	{
		OpenGLDllImportBase.GetDelegateFor<glShaderBinaryDelegate>()(count, shaders, binaryformat, binary, length);
	}

	public static void glGetShaderPrecisionFormat(uint shadertype, uint precisiontype, int[] range, int[] precision)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetShaderPrecisionFormatDelegate>()(shadertype, precisiontype, range, precision);
	}

	public static void glDepthRangef(float n, float f)
	{
		OpenGLDllImportBase.GetDelegateFor<glDepthRangefDelegate>()(n, f);
	}

	public static void glClearDepthf(float d)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearDepthfDelegate>()(d);
	}

	public static void glGetProgramBinary(uint program, int bufSize, int[] length, uint[] binaryFormat, IntPtr binary)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetProgramBinaryDelegate>()(program, bufSize, length, binaryFormat, binary);
	}

	public static void glProgramBinary(uint program, uint binaryFormat, IntPtr binary, int length)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramBinaryDelegate>()(program, binaryFormat, binary, length);
	}

	public static void glProgramParameteri(uint program, uint pname, int value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramParameteriDelegate>()(program, pname, value);
	}

	public static void glUseProgramStages(uint pipeline, uint stages, uint program)
	{
		OpenGLDllImportBase.GetDelegateFor<glUseProgramStagesDelegate>()(pipeline, stages, program);
	}

	public static void glActiveShaderProgram(uint pipeline, uint program)
	{
		OpenGLDllImportBase.GetDelegateFor<glActiveShaderProgramDelegate>()(pipeline, program);
	}

	public static uint glCreateShaderProgramv(uint type, int count, char[] strings)
	{
		return (uint)OpenGLDllImportBase.GetDelegateFor<glCreateShaderProgramvDelegate>()(type, count, strings);
	}

	public static void glBindProgramPipeline(uint pipeline)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindProgramPipelineDelegate>()(pipeline);
	}

	public static void glDeleteProgramPipelines(int n, uint[] pipelines)
	{
		OpenGLDllImportBase.GetDelegateFor<glDeleteProgramPipelinesDelegate>()(n, pipelines);
	}

	public static void glGenProgramPipelines(int n, uint[] pipelines)
	{
		OpenGLDllImportBase.GetDelegateFor<glGenProgramPipelinesDelegate>()(n, pipelines);
	}

	public static bool glIsProgramPipeline(uint pipeline)
	{
		return (bool)OpenGLDllImportBase.GetDelegateFor<glIsProgramPipelineDelegate>()(pipeline);
	}

	public static void glGetProgramPipelineiv(uint pipeline, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetProgramPipelineivDelegate>()(pipeline, pname, @params);
	}

	public static void glProgramUniform1i(uint program, uint location, int v0)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform1iDelegate>()(program, location, v0);
	}

	public static void glProgramUniform1iv(uint program, uint location, int count, int[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform1ivDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform1f(uint program, uint location, float v0)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform1fDelegate>()(program, location, v0);
	}

	public static void glProgramUniform1fv(uint program, uint location, int count, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform1fvDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform1d(uint program, uint location, double v0)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform1dDelegate>()(program, location, v0);
	}

	public static void glProgramUniform1dv(uint program, uint location, int count, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform1dvDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform1ui(uint program, uint location, uint v0)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform1uiDelegate>()(program, location, v0);
	}

	public static void glProgramUniform1uiv(uint program, uint location, int count, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform1uivDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform2i(uint program, uint location, int v0, int v1)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform2iDelegate>()(program, location, v0, v1);
	}

	public static void glProgramUniform2iv(uint program, uint location, int count, int[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform2ivDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform2f(uint program, uint location, float v0, float v1)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform2fDelegate>()(program, location, v0, v1);
	}

	public static void glProgramUniform2fv(uint program, uint location, int count, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform2fvDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform2d(uint program, uint location, double v0, double v1)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform2dDelegate>()(program, location, v0, v1);
	}

	public static void glProgramUniform2dv(uint program, uint location, int count, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform2dvDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform2ui(uint program, uint location, uint v0, uint v1)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform2uiDelegate>()(program, location, v0, v1);
	}

	public static void glProgramUniform2uiv(uint program, uint location, int count, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform2uivDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform3i(uint program, uint location, int v0, int v1, int v2)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform3iDelegate>()(program, location, v0, v1, v2);
	}

	public static void glProgramUniform3iv(uint program, uint location, int count, int[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform3ivDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform3f(uint program, uint location, float v0, float v1, float v2)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform3fDelegate>()(program, location, v0, v1, v2);
	}

	public static void glProgramUniform3fv(uint program, uint location, int count, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform3fvDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform3d(uint program, uint location, double v0, double v1, double v2)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform3dDelegate>()(program, location, v0, v1, v2);
	}

	public static void glProgramUniform3dv(uint program, uint location, int count, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform3dvDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform3ui(uint program, uint location, uint v0, uint v1, uint v2)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform3uiDelegate>()(program, location, v0, v1, v2);
	}

	public static void glProgramUniform3uiv(uint program, uint location, int count, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform3uivDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform4i(uint program, uint location, int v0, int v1, int v2, int v3)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform4iDelegate>()(program, location, v0, v1, v2, v3);
	}

	public static void glProgramUniform4iv(uint program, uint location, int count, int[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform4ivDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform4f(uint program, uint location, float v0, float v1, float v2, float v3)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform4fDelegate>()(program, location, v0, v1, v2, v3);
	}

	public static void glProgramUniform4fv(uint program, uint location, int count, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform4fvDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform4d(uint program, uint location, double v0, double v1, double v2, double v3)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform4dDelegate>()(program, location, v0, v1, v2, v3);
	}

	public static void glProgramUniform4dv(uint program, uint location, int count, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform4dvDelegate>()(program, location, count, value);
	}

	public static void glProgramUniform4ui(uint program, uint location, uint v0, uint v1, uint v2, uint v3)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform4uiDelegate>()(program, location, v0, v1, v2, v3);
	}

	public static void glProgramUniform4uiv(uint program, uint location, int count, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniform4uivDelegate>()(program, location, count, value);
	}

	public static void glProgramUniformMatrix2fv(uint program, uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix2fvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix3fv(uint program, uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix3fvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix4fv(uint program, uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix4fvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix2dv(uint program, uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix2dvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix3dv(uint program, uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix3dvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix4dv(uint program, uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix4dvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix2x3fv(uint program, uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix2x3fvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix3x2fv(uint program, uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix3x2fvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix2x4fv(uint program, uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix2x4fvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix4x2fv(uint program, uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix4x2fvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix3x4fv(uint program, uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix3x4fvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix4x3fv(uint program, uint location, int count, bool transpose, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix4x3fvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix2x3dv(uint program, uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix2x3dvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix3x2dv(uint program, uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix3x2dvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix2x4dv(uint program, uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix2x4dvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix4x2dv(uint program, uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix4x2dvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix3x4dv(uint program, uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix3x4dvDelegate>()(program, location, count, transpose, value);
	}

	public static void glProgramUniformMatrix4x3dv(uint program, uint location, int count, bool transpose, double[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glProgramUniformMatrix4x3dvDelegate>()(program, location, count, transpose, value);
	}

	public static void glValidateProgramPipeline(uint pipeline)
	{
		OpenGLDllImportBase.GetDelegateFor<glValidateProgramPipelineDelegate>()(pipeline);
	}

	public static void glGetProgramPipelineInfoLog(uint pipeline, int bufSize, int[] length, char[] infoLog)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetProgramPipelineInfoLogDelegate>()(pipeline, bufSize, length, infoLog);
	}

	public static void glVertexAttribL1d(uint index, double x)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribL1dDelegate>()(index, x);
	}

	public static void glVertexAttribL2d(uint index, double x, double y)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribL2dDelegate>()(index, x, y);
	}

	public static void glVertexAttribL3d(uint index, double x, double y, double z)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribL3dDelegate>()(index, x, y, z);
	}

	public static void glVertexAttribL4d(uint index, double x, double y, double z, double w)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribL4dDelegate>()(index, x, y, z, w);
	}

	public static void glVertexAttribL1dv(uint index, double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribL1dvDelegate>()(index, v);
	}

	public static void glVertexAttribL2dv(uint index, double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribL2dvDelegate>()(index, v);
	}

	public static void glVertexAttribL3dv(uint index, double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribL3dvDelegate>()(index, v);
	}

	public static void glVertexAttribL4dv(uint index, double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribL4dvDelegate>()(index, v);
	}

	public static void glVertexAttribLPointer(uint index, int size, uint type, int stride, IntPtr pointer)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribLPointerDelegate>()(index, size, type, stride, pointer);
	}

	public static void glGetVertexAttribLdv(uint index, uint pname, double[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetVertexAttribLdvDelegate>()(index, pname, @params);
	}

	public static void glViewportArrayv(uint first, int count, float[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glViewportArrayvDelegate>()(first, count, v);
	}

	public static void glViewportIndexedf(uint index, float x, float y, float w, float h)
	{
		OpenGLDllImportBase.GetDelegateFor<glViewportIndexedfDelegate>()(index, x, y, w, h);
	}

	public static void glViewportIndexedfv(uint index, float[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glViewportIndexedfvDelegate>()(index, v);
	}

	public static void glScissorArrayv(uint first, int count, int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glScissorArrayvDelegate>()(first, count, v);
	}

	public static void glScissorIndexed(uint index, int left, int bottom, int width, int height)
	{
		OpenGLDllImportBase.GetDelegateFor<glScissorIndexedDelegate>()(index, left, bottom, width, height);
	}

	public static void glScissorIndexedv(uint index, int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glScissorIndexedvDelegate>()(index, v);
	}

	public static void glDepthRangeArrayv(uint first, int count, double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glDepthRangeArrayvDelegate>()(first, count, v);
	}

	public static void glDepthRangeIndexed(uint index, double n, double f)
	{
		OpenGLDllImportBase.GetDelegateFor<glDepthRangeIndexedDelegate>()(index, n, f);
	}

	public static void glGetFloati_v(uint target, uint index, float[] data)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetFloati_vDelegate>()(target, index, data);
	}

	public static void glGetDoublei_v(uint target, uint index, double[] data)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetDoublei_vDelegate>()(target, index, data);
	}

	public static void glDrawArraysInstancedBaseInstance(uint mode, int first, int count, int instancecount, uint baseinstance)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawArraysInstancedBaseInstanceDelegate>()(mode, first, count, instancecount, baseinstance);
	}

	public static void glDrawElementsInstancedBaseInstance(uint mode, int count, uint type, IntPtr indices, int instancecount, uint baseinstance)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawElementsInstancedBaseInstanceDelegate>()(mode, count, type, indices, instancecount, baseinstance);
	}

	public static void glDrawElementsInstancedBaseVertexBaseInstance(uint mode, int count, uint type, IntPtr indices, int instancecount, int basevertex, uint baseinstance)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawElementsInstancedBaseVertexBaseInstanceDelegate>()(mode, count, type, indices, instancecount, basevertex, baseinstance);
	}

	public static void glGetInternalformativ(uint target, uint internalformat, uint pname, int bufSize, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetInternalformativDelegate>()(target, internalformat, pname, bufSize, @params);
	}

	public static void glGetActiveAtomicCounterBufferiv(uint program, uint bufferIndex, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetActiveAtomicCounterBufferivDelegate>()(program, bufferIndex, pname, @params);
	}

	public static void glBindImageTexture(uint unit, uint texture, int level, bool layered, int layer, uint access, uint format)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindImageTextureDelegate>()(unit, texture, level, layered, layer, access, format);
	}

	public static void glMemoryBarrier(uint barriers)
	{
		OpenGLDllImportBase.GetDelegateFor<glMemoryBarrierDelegate>()(barriers);
	}

	public static void glTexStorage1D(uint target, int levels, uint internalformat, int width)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexStorage1DDelegate>()(target, levels, internalformat, width);
	}

	public static void glTexStorage2D(uint target, int levels, uint internalformat, int width, int height)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexStorage2DDelegate>()(target, levels, internalformat, width, height);
	}

	public static void glTexStorage3D(uint target, int levels, uint internalformat, int width, int height, int depth)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexStorage3DDelegate>()(target, levels, internalformat, width, height, depth);
	}

	public static void glDrawTransformFeedbackInstanced(uint mode, uint id, int instancecount)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawTransformFeedbackInstancedDelegate>()(mode, id, instancecount);
	}

	public static void glDrawTransformFeedbackStreamInstanced(uint mode, uint id, uint stream, int instancecount)
	{
		OpenGLDllImportBase.GetDelegateFor<glDrawTransformFeedbackStreamInstancedDelegate>()(mode, id, stream, instancecount);
	}

	public static void glClearBufferData(uint target, uint internalformat, uint format, uint type, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearBufferDataDelegate>()(target, internalformat, format, type, data);
	}

	public static void glClearBufferSubData(uint target, uint internalformat, IntPtr offset, IntPtr size, uint format, uint type, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearBufferSubDataDelegate>()(target, internalformat, offset, size, format, type, data);
	}

	public static void glDispatchCompute(uint num_groups_x, uint num_groups_y, uint num_groups_z)
	{
		OpenGLDllImportBase.GetDelegateFor<glDispatchComputeDelegate>()(num_groups_x, num_groups_y, num_groups_z);
	}

	public static void glDispatchComputeIndirect(IntPtr indirect)
	{
		OpenGLDllImportBase.GetDelegateFor<glDispatchComputeIndirectDelegate>()(indirect);
	}

	public static void glCopyImageSubData(uint srcName, uint srcTarget, int srcLevel, int srcX, int srcY, int srcZ, uint dstName, uint dstTarget, int dstLevel, int dstX, int dstY, int dstZ, int srcWidth, int srcHeight, int srcDepth)
	{
		OpenGLDllImportBase.GetDelegateFor<glCopyImageSubDataDelegate>()(srcName, srcTarget, srcLevel, srcX, srcY, srcZ, dstName, dstTarget, dstLevel, dstX, dstY, dstZ, srcWidth, srcHeight, srcDepth);
	}

	public static void glFramebufferParameteri(uint target, uint pname, int param)
	{
		OpenGLDllImportBase.GetDelegateFor<glFramebufferParameteriDelegate>()(target, pname, param);
	}

	public static void glGetFramebufferParameteriv(uint target, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetFramebufferParameterivDelegate>()(target, pname, @params);
	}

	public static void glGetInternalformati64v(uint target, uint internalformat, uint pname, int bufSize, Int64[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetInternalformati64vDelegate>()(target, internalformat, pname, bufSize, @params);
	}

	public static void glInvalidateTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth)
	{
		OpenGLDllImportBase.GetDelegateFor<glInvalidateTexSubImageDelegate>()(texture, level, xoffset, yoffset, zoffset, width, height, depth);
	}

	public static void glInvalidateTexImage(uint texture, int level)
	{
		OpenGLDllImportBase.GetDelegateFor<glInvalidateTexImageDelegate>()(texture, level);
	}

	public static void glInvalidateBufferSubData(uint buffer, IntPtr offset, IntPtr length)
	{
		OpenGLDllImportBase.GetDelegateFor<glInvalidateBufferSubDataDelegate>()(buffer, offset, length);
	}

	public static void glInvalidateBufferData(uint buffer)
	{
		OpenGLDllImportBase.GetDelegateFor<glInvalidateBufferDataDelegate>()(buffer);
	}

	public static void glInvalidateFramebuffer(uint target, int numAttachments, uint[] attachments)
	{
		OpenGLDllImportBase.GetDelegateFor<glInvalidateFramebufferDelegate>()(target, numAttachments, attachments);
	}

	public static void glInvalidateSubFramebuffer(uint target, int numAttachments, uint[] attachments, int x, int y, int width, int height)
	{
		OpenGLDllImportBase.GetDelegateFor<glInvalidateSubFramebufferDelegate>()(target, numAttachments, attachments, x, y, width, height);
	}

	public static void glMultiDrawArraysIndirect(uint mode, IntPtr indirect, int drawcount, int stride)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiDrawArraysIndirectDelegate>()(mode, indirect, drawcount, stride);
	}

	public static void glMultiDrawElementsIndirect(uint mode, uint type, IntPtr indirect, int drawcount, int stride)
	{
		OpenGLDllImportBase.GetDelegateFor<glMultiDrawElementsIndirectDelegate>()(mode, type, indirect, drawcount, stride);
	}

	public static void glGetProgramInterfaceiv(uint program, uint programInterface, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetProgramInterfaceivDelegate>()(program, programInterface, pname, @params);
	}

	public static uint glGetProgramResourceIndex(uint program, uint programInterface, char[] name)
	{
		return (uint)OpenGLDllImportBase.GetDelegateFor<glGetProgramResourceIndexDelegate>()(program, programInterface, name);
	}

	public static void glGetProgramResourceName(uint program, uint programInterface, uint index, int bufSize, int[] length, char[] name)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetProgramResourceNameDelegate>()(program, programInterface, index, bufSize, length, name);
	}

	public static void glGetProgramResourceiv(uint program, uint programInterface, uint index, int propCount, uint[] props, int bufSize, int[] length, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetProgramResourceivDelegate>()(program, programInterface, index, propCount, props, bufSize, length, @params);
	}

	public static int glGetProgramResourceLocation(uint program, uint programInterface, char[] name)
	{
		return (int)OpenGLDllImportBase.GetDelegateFor<glGetProgramResourceLocationDelegate>()(program, programInterface, name);
	}

	public static int glGetProgramResourceLocationIndex(uint program, uint programInterface, char[] name)
	{
		return (int)OpenGLDllImportBase.GetDelegateFor<glGetProgramResourceLocationIndexDelegate>()(program, programInterface, name);
	}

	public static void glShaderStorageBlockBinding(uint program, uint storageBlockIndex, uint storageBlockBinding)
	{
		OpenGLDllImportBase.GetDelegateFor<glShaderStorageBlockBindingDelegate>()(program, storageBlockIndex, storageBlockBinding);
	}

	public static void glTexBufferRange(uint target, uint internalformat, uint buffer, IntPtr offset, IntPtr size)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexBufferRangeDelegate>()(target, internalformat, buffer, offset, size);
	}

	public static void glTexStorage2DMultisample(uint target, int samples, uint internalformat, int width, int height, bool fixedsamplelocations)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexStorage2DMultisampleDelegate>()(target, samples, internalformat, width, height, fixedsamplelocations);
	}

	public static void glTexStorage3DMultisample(uint target, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations)
	{
		OpenGLDllImportBase.GetDelegateFor<glTexStorage3DMultisampleDelegate>()(target, samples, internalformat, width, height, depth, fixedsamplelocations);
	}

	public static void glTextureView(uint texture, uint target, uint origtexture, uint internalformat, uint minlevel, uint numlevels, uint minlayer, uint numlayers)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureViewDelegate>()(texture, target, origtexture, internalformat, minlevel, numlevels, minlayer, numlayers);
	}

	public static void glBindVertexBuffer(uint bindingindex, uint buffer, IntPtr offset, int stride)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindVertexBufferDelegate>()(bindingindex, buffer, offset, stride);
	}

	public static void glVertexAttribFormat(uint attribindex, int size, uint type, bool normalized, uint relativeoffset)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribFormatDelegate>()(attribindex, size, type, normalized, relativeoffset);
	}

	public static void glVertexAttribIFormat(uint attribindex, int size, uint type, uint relativeoffset)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribIFormatDelegate>()(attribindex, size, type, relativeoffset);
	}

	public static void glVertexAttribLFormat(uint attribindex, int size, uint type, uint relativeoffset)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribLFormatDelegate>()(attribindex, size, type, relativeoffset);
	}

	public static void glVertexAttribBinding(uint attribindex, uint bindingindex)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexAttribBindingDelegate>()(attribindex, bindingindex);
	}

	public static void glVertexBindingDivisor(uint bindingindex, uint divisor)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexBindingDivisorDelegate>()(bindingindex, divisor);
	}

	public static void glDebugMessageControl(uint source, uint type, uint severity, int count, uint[] ids, bool enabled)
	{
		OpenGLDllImportBase.GetDelegateFor<glDebugMessageControlDelegate>()(source, type, severity, count, ids, enabled);
	}

	public static void glDebugMessageInsert(uint source, uint type, uint id, uint severity, int length, char[] buf)
	{
		OpenGLDllImportBase.GetDelegateFor<glDebugMessageInsertDelegate>()(source, type, id, severity, length, buf);
	}

	public static void glDebugMessageCallback(IntPtr callback, IntPtr userParam)
	{
		OpenGLDllImportBase.GetDelegateFor<glDebugMessageCallbackDelegate>()(callback, userParam);
	}

	public static uint glGetDebugMessageLog(uint count, int bufSize, uint[] sources, uint[] types, uint[] ids, uint[] severities, int[] lengths, char[] messageLog)
	{
		return (uint)OpenGLDllImportBase.GetDelegateFor<glGetDebugMessageLogDelegate>()(count, bufSize, sources, types, ids, severities, lengths, messageLog);
	}

	public static void glPushDebugGroup(uint source, uint id, int length, char[] message)
	{
		OpenGLDllImportBase.GetDelegateFor<glPushDebugGroupDelegate>()(source, id, length, message);
	}

	public static void glPopDebugGroup()
	{
		OpenGLDllImportBase.GetDelegateFor<glPopDebugGroupDelegate>()();
	}

	public static void glObjectLabel(uint identifier, uint name, int length, char[] label)
	{
		OpenGLDllImportBase.GetDelegateFor<glObjectLabelDelegate>()(identifier, name, length, label);
	}

	public static void glGetObjectLabel(uint identifier, uint name, int bufSize, int[] length, char[] label)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetObjectLabelDelegate>()(identifier, name, bufSize, length, label);
	}

	public static void glObjectPtrLabel(IntPtr ptr, int length, char[] label)
	{
		OpenGLDllImportBase.GetDelegateFor<glObjectPtrLabelDelegate>()(ptr, length, label);
	}

	public static void glGetObjectPtrLabel(IntPtr ptr, int bufSize, int[] length, char[] label)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetObjectPtrLabelDelegate>()(ptr, bufSize, length, label);
	}

	public static void glBufferStorage(uint target, IntPtr size, IntPtr data, uint flags)
	{
		OpenGLDllImportBase.GetDelegateFor<glBufferStorageDelegate>()(target, size, data, flags);
	}

	public static void glClearTexImage(uint texture, int level, uint format, uint type, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearTexImageDelegate>()(texture, level, format, type, data);
	}

	public static void glClearTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearTexSubImageDelegate>()(texture, level, xoffset, yoffset, zoffset, width, height, depth, format, type, data);
	}

	public static void glBindBuffersBase(uint target, uint first, int count, uint[] buffers)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindBuffersBaseDelegate>()(target, first, count, buffers);
	}

	public static void glBindBuffersRange(uint target, uint first, int count, uint[] buffers, IntPtr offsets, IntPtr sizes)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindBuffersRangeDelegate>()(target, first, count, buffers, offsets, sizes);
	}

	public static void glBindTextures(uint first, int count, uint[] textures)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindTexturesDelegate>()(first, count, textures);
	}

	public static void glBindSamplers(uint first, int count, uint[] samplers)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindSamplersDelegate>()(first, count, samplers);
	}

	public static void glBindImageTextures(uint first, int count, uint[] textures)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindImageTexturesDelegate>()(first, count, textures);
	}

	public static void glBindVertexBuffers(uint first, int count, uint[] buffers, IntPtr offsets, int[] strides)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindVertexBuffersDelegate>()(first, count, buffers, offsets, strides);
	}

	public static void glClipControl(uint origin, uint depth)
	{
		OpenGLDllImportBase.GetDelegateFor<glClipControlDelegate>()(origin, depth);
	}

	public static void glCreateTransformFeedbacks(int n, uint[] ids)
	{
		OpenGLDllImportBase.GetDelegateFor<glCreateTransformFeedbacksDelegate>()(n, ids);
	}

	public static void glTransformFeedbackBufferBase(uint xfb, uint index, uint buffer)
	{
		OpenGLDllImportBase.GetDelegateFor<glTransformFeedbackBufferBaseDelegate>()(xfb, index, buffer);
	}

	public static void glTransformFeedbackBufferRange(uint xfb, uint index, uint buffer, IntPtr offset, IntPtr size)
	{
		OpenGLDllImportBase.GetDelegateFor<glTransformFeedbackBufferRangeDelegate>()(xfb, index, buffer, offset, size);
	}

	public static void glGetTransformFeedbackiv(uint xfb, uint pname, int[] param)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetTransformFeedbackivDelegate>()(xfb, pname, param);
	}

	public static void glGetTransformFeedbacki_v(uint xfb, uint pname, uint index, int[] param)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetTransformFeedbacki_vDelegate>()(xfb, pname, index, param);
	}

	public static void glGetTransformFeedbacki64_v(uint xfb, uint pname, uint index, Int64[] param)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetTransformFeedbacki64_vDelegate>()(xfb, pname, index, param);
	}

	public static void glCreateBuffers(int n, uint[] buffers)
	{
		OpenGLDllImportBase.GetDelegateFor<glCreateBuffersDelegate>()(n, buffers);
	}

	public static void glNamedBufferStorage(uint buffer, IntPtr size, IntPtr data, uint flags)
	{
		OpenGLDllImportBase.GetDelegateFor<glNamedBufferStorageDelegate>()(buffer, size, data, flags);
	}

	public static void glNamedBufferData(uint buffer, IntPtr size, IntPtr data, uint usage)
	{
		OpenGLDllImportBase.GetDelegateFor<glNamedBufferDataDelegate>()(buffer, size, data, usage);
	}

	public static void glNamedBufferSubData(uint buffer, IntPtr offset, IntPtr size, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glNamedBufferSubDataDelegate>()(buffer, offset, size, data);
	}

	public static void glCopyNamedBufferSubData(uint readBuffer, uint writeBuffer, IntPtr readOffset, IntPtr writeOffset, IntPtr size)
	{
		OpenGLDllImportBase.GetDelegateFor<glCopyNamedBufferSubDataDelegate>()(readBuffer, writeBuffer, readOffset, writeOffset, size);
	}

	public static void glClearNamedBufferData(uint buffer, uint internalformat, uint format, uint type, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearNamedBufferDataDelegate>()(buffer, internalformat, format, type, data);
	}

	public static void glClearNamedBufferSubData(uint buffer, uint internalformat, IntPtr offset, IntPtr size, uint format, uint type, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearNamedBufferSubDataDelegate>()(buffer, internalformat, offset, size, format, type, data);
	}

	public static void glMapNamedBuffer(uint buffer, uint access)
	{
		OpenGLDllImportBase.GetDelegateFor<glMapNamedBufferDelegate>()(buffer, access);
	}

	public static void glMapNamedBufferRange(uint buffer, IntPtr offset, IntPtr length, uint access)
	{
		OpenGLDllImportBase.GetDelegateFor<glMapNamedBufferRangeDelegate>()(buffer, offset, length, access);
	}

	public static bool glUnmapNamedBuffer(uint buffer)
	{
		return (bool)OpenGLDllImportBase.GetDelegateFor<glUnmapNamedBufferDelegate>()(buffer);
	}

	public static void glFlushMappedNamedBufferRange(uint buffer, IntPtr offset, IntPtr length)
	{
		OpenGLDllImportBase.GetDelegateFor<glFlushMappedNamedBufferRangeDelegate>()(buffer, offset, length);
	}

	public static void glGetNamedBufferParameteriv(uint buffer, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetNamedBufferParameterivDelegate>()(buffer, pname, @params);
	}

	public static void glGetNamedBufferParameteri64v(uint buffer, uint pname, Int64[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetNamedBufferParameteri64vDelegate>()(buffer, pname, @params);
	}

	public static void glGetNamedBufferPointerv(uint buffer, uint pname, IntPtr @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetNamedBufferPointervDelegate>()(buffer, pname, @params);
	}

	public static void glGetNamedBufferSubData(uint buffer, IntPtr offset, IntPtr size, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetNamedBufferSubDataDelegate>()(buffer, offset, size, data);
	}

	public static void glCreateFramebuffers(int n, uint[] framebuffers)
	{
		OpenGLDllImportBase.GetDelegateFor<glCreateFramebuffersDelegate>()(n, framebuffers);
	}

	public static void glNamedFramebufferRenderbuffer(uint framebuffer, uint attachment, uint renderbuffertarget, uint renderbuffer)
	{
		OpenGLDllImportBase.GetDelegateFor<glNamedFramebufferRenderbufferDelegate>()(framebuffer, attachment, renderbuffertarget, renderbuffer);
	}

	public static void glNamedFramebufferParameteri(uint framebuffer, uint pname, int param)
	{
		OpenGLDllImportBase.GetDelegateFor<glNamedFramebufferParameteriDelegate>()(framebuffer, pname, param);
	}

	public static void glNamedFramebufferTexture(uint framebuffer, uint attachment, uint texture, int level)
	{
		OpenGLDllImportBase.GetDelegateFor<glNamedFramebufferTextureDelegate>()(framebuffer, attachment, texture, level);
	}

	public static void glNamedFramebufferTextureLayer(uint framebuffer, uint attachment, uint texture, int level, int layer)
	{
		OpenGLDllImportBase.GetDelegateFor<glNamedFramebufferTextureLayerDelegate>()(framebuffer, attachment, texture, level, layer);
	}

	public static void glNamedFramebufferDrawBuffer(uint framebuffer, uint buf)
	{
		OpenGLDllImportBase.GetDelegateFor<glNamedFramebufferDrawBufferDelegate>()(framebuffer, buf);
	}

	public static void glNamedFramebufferDrawBuffers(uint framebuffer, int n, uint[] bufs)
	{
		OpenGLDllImportBase.GetDelegateFor<glNamedFramebufferDrawBuffersDelegate>()(framebuffer, n, bufs);
	}

	public static void glNamedFramebufferReadBuffer(uint framebuffer, uint src)
	{
		OpenGLDllImportBase.GetDelegateFor<glNamedFramebufferReadBufferDelegate>()(framebuffer, src);
	}

	public static void glInvalidateNamedFramebufferData(uint framebuffer, int numAttachments, uint[] attachments)
	{
		OpenGLDllImportBase.GetDelegateFor<glInvalidateNamedFramebufferDataDelegate>()(framebuffer, numAttachments, attachments);
	}

	public static void glInvalidateNamedFramebufferSubData(uint framebuffer, int numAttachments, uint[] attachments, int x, int y, int width, int height)
	{
		OpenGLDllImportBase.GetDelegateFor<glInvalidateNamedFramebufferSubDataDelegate>()(framebuffer, numAttachments, attachments, x, y, width, height);
	}

	public static void glClearNamedFramebufferiv(uint framebuffer, uint buffer, int drawbuffer, int[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearNamedFramebufferivDelegate>()(framebuffer, buffer, drawbuffer, value);
	}

	public static void glClearNamedFramebufferuiv(uint framebuffer, uint buffer, int drawbuffer, uint[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearNamedFramebufferuivDelegate>()(framebuffer, buffer, drawbuffer, value);
	}

	public static void glClearNamedFramebufferfv(uint framebuffer, uint buffer, int drawbuffer, float[] value)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearNamedFramebufferfvDelegate>()(framebuffer, buffer, drawbuffer, value);
	}

	public static void glClearNamedFramebufferfi(uint framebuffer, uint buffer, int drawbuffer, float depth, int stencil)
	{
		OpenGLDllImportBase.GetDelegateFor<glClearNamedFramebufferfiDelegate>()(framebuffer, buffer, drawbuffer, depth, stencil);
	}

	public static void glBlitNamedFramebuffer(uint readFramebuffer, uint drawFramebuffer, int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter)
	{
		OpenGLDllImportBase.GetDelegateFor<glBlitNamedFramebufferDelegate>()(readFramebuffer, drawFramebuffer, srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, mask, filter);
	}

	public static uint glCheckNamedFramebufferStatus(uint framebuffer, uint target)
	{
		return (uint)OpenGLDllImportBase.GetDelegateFor<glCheckNamedFramebufferStatusDelegate>()(framebuffer, target);
	}

	public static void glGetNamedFramebufferParameteriv(uint framebuffer, uint pname, int[] param)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetNamedFramebufferParameterivDelegate>()(framebuffer, pname, param);
	}

	public static void glGetNamedFramebufferAttachmentParameteriv(uint framebuffer, uint attachment, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetNamedFramebufferAttachmentParameterivDelegate>()(framebuffer, attachment, pname, @params);
	}

	public static void glCreateRenderbuffers(int n, uint[] renderbuffers)
	{
		OpenGLDllImportBase.GetDelegateFor<glCreateRenderbuffersDelegate>()(n, renderbuffers);
	}

	public static void glNamedRenderbufferStorage(uint renderbuffer, uint internalformat, int width, int height)
	{
		OpenGLDllImportBase.GetDelegateFor<glNamedRenderbufferStorageDelegate>()(renderbuffer, internalformat, width, height);
	}

	public static void glNamedRenderbufferStorageMultisample(uint renderbuffer, int samples, uint internalformat, int width, int height)
	{
		OpenGLDllImportBase.GetDelegateFor<glNamedRenderbufferStorageMultisampleDelegate>()(renderbuffer, samples, internalformat, width, height);
	}

	public static void glGetNamedRenderbufferParameteriv(uint renderbuffer, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetNamedRenderbufferParameterivDelegate>()(renderbuffer, pname, @params);
	}

	public static void glCreateTextures(uint target, int n, uint[] textures)
	{
		OpenGLDllImportBase.GetDelegateFor<glCreateTexturesDelegate>()(target, n, textures);
	}

	public static void glTextureBuffer(uint texture, uint internalformat, uint buffer)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureBufferDelegate>()(texture, internalformat, buffer);
	}

	public static void glTextureBufferRange(uint texture, uint internalformat, uint buffer, IntPtr offset, IntPtr size)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureBufferRangeDelegate>()(texture, internalformat, buffer, offset, size);
	}

	public static void glTextureStorage1D(uint texture, int levels, uint internalformat, int width)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureStorage1DDelegate>()(texture, levels, internalformat, width);
	}

	public static void glTextureStorage2D(uint texture, int levels, uint internalformat, int width, int height)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureStorage2DDelegate>()(texture, levels, internalformat, width, height);
	}

	public static void glTextureStorage3D(uint texture, int levels, uint internalformat, int width, int height, int depth)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureStorage3DDelegate>()(texture, levels, internalformat, width, height, depth);
	}

	public static void glTextureStorage2DMultisample(uint texture, int samples, uint internalformat, int width, int height, bool fixedsamplelocations)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureStorage2DMultisampleDelegate>()(texture, samples, internalformat, width, height, fixedsamplelocations);
	}

	public static void glTextureStorage3DMultisample(uint texture, int samples, uint internalformat, int width, int height, int depth, bool fixedsamplelocations)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureStorage3DMultisampleDelegate>()(texture, samples, internalformat, width, height, depth, fixedsamplelocations);
	}

	public static void glTextureSubImage1D(uint texture, int level, int xoffset, int width, uint format, uint type, IntPtr pixels)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureSubImage1DDelegate>()(texture, level, xoffset, width, format, type, pixels);
	}

	public static void glTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, IntPtr pixels)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureSubImage2DDelegate>()(texture, level, xoffset, yoffset, width, height, format, type, pixels);
	}

	public static void glTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, IntPtr pixels)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureSubImage3DDelegate>()(texture, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels);
	}

	public static void glCompressedTextureSubImage1D(uint texture, int level, int xoffset, int width, uint format, int imageSize, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glCompressedTextureSubImage1DDelegate>()(texture, level, xoffset, width, format, imageSize, data);
	}

	public static void glCompressedTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glCompressedTextureSubImage2DDelegate>()(texture, level, xoffset, yoffset, width, height, format, imageSize, data);
	}

	public static void glCompressedTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glCompressedTextureSubImage3DDelegate>()(texture, level, xoffset, yoffset, zoffset, width, height, depth, format, imageSize, data);
	}

	public static void glCopyTextureSubImage1D(uint texture, int level, int xoffset, int x, int y, int width)
	{
		OpenGLDllImportBase.GetDelegateFor<glCopyTextureSubImage1DDelegate>()(texture, level, xoffset, x, y, width);
	}

	public static void glCopyTextureSubImage2D(uint texture, int level, int xoffset, int yoffset, int x, int y, int width, int height)
	{
		OpenGLDllImportBase.GetDelegateFor<glCopyTextureSubImage2DDelegate>()(texture, level, xoffset, yoffset, x, y, width, height);
	}

	public static void glCopyTextureSubImage3D(uint texture, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height)
	{
		OpenGLDllImportBase.GetDelegateFor<glCopyTextureSubImage3DDelegate>()(texture, level, xoffset, yoffset, zoffset, x, y, width, height);
	}

	public static void glTextureParameterf(uint texture, uint pname, float param)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureParameterfDelegate>()(texture, pname, param);
	}

	public static void glTextureParameterfv(uint texture, uint pname, float[] param)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureParameterfvDelegate>()(texture, pname, param);
	}

	public static void glTextureParameteri(uint texture, uint pname, int param)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureParameteriDelegate>()(texture, pname, param);
	}

	public static void glTextureParameterIiv(uint texture, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureParameterIivDelegate>()(texture, pname, @params);
	}

	public static void glTextureParameterIuiv(uint texture, uint pname, uint[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureParameterIuivDelegate>()(texture, pname, @params);
	}

	public static void glTextureParameteriv(uint texture, uint pname, int[] param)
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureParameterivDelegate>()(texture, pname, param);
	}

	public static void glGenerateTextureMipmap(uint texture)
	{
		OpenGLDllImportBase.GetDelegateFor<glGenerateTextureMipmapDelegate>()(texture);
	}

	public static void glBindTextureUnit(uint unit, uint texture)
	{
		OpenGLDllImportBase.GetDelegateFor<glBindTextureUnitDelegate>()(unit, texture);
	}

	public static void glGetTextureImage(uint texture, int level, uint format, uint type, int bufSize, IntPtr pixels)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetTextureImageDelegate>()(texture, level, format, type, bufSize, pixels);
	}

	public static void glGetCompressedTextureImage(uint texture, int level, int bufSize, IntPtr pixels)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetCompressedTextureImageDelegate>()(texture, level, bufSize, pixels);
	}

	public static void glGetTextureLevelParameterfv(uint texture, int level, uint pname, float[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetTextureLevelParameterfvDelegate>()(texture, level, pname, @params);
	}

	public static void glGetTextureLevelParameteriv(uint texture, int level, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetTextureLevelParameterivDelegate>()(texture, level, pname, @params);
	}

	public static void glGetTextureParameterfv(uint texture, uint pname, float[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetTextureParameterfvDelegate>()(texture, pname, @params);
	}

	public static void glGetTextureParameterIiv(uint texture, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetTextureParameterIivDelegate>()(texture, pname, @params);
	}

	public static void glGetTextureParameterIuiv(uint texture, uint pname, uint[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetTextureParameterIuivDelegate>()(texture, pname, @params);
	}

	public static void glGetTextureParameteriv(uint texture, uint pname, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetTextureParameterivDelegate>()(texture, pname, @params);
	}

	public static void glCreateVertexArrays(int n, uint[] arrays)
	{
		OpenGLDllImportBase.GetDelegateFor<glCreateVertexArraysDelegate>()(n, arrays);
	}

	public static void glDisableVertexArrayAttrib(uint vaobj, uint index)
	{
		OpenGLDllImportBase.GetDelegateFor<glDisableVertexArrayAttribDelegate>()(vaobj, index);
	}

	public static void glEnableVertexArrayAttrib(uint vaobj, uint index)
	{
		OpenGLDllImportBase.GetDelegateFor<glEnableVertexArrayAttribDelegate>()(vaobj, index);
	}

	public static void glVertexArrayElementBuffer(uint vaobj, uint buffer)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexArrayElementBufferDelegate>()(vaobj, buffer);
	}

	public static void glVertexArrayVertexBuffer(uint vaobj, uint bindingindex, uint buffer, IntPtr offset, int stride)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexArrayVertexBufferDelegate>()(vaobj, bindingindex, buffer, offset, stride);
	}

	public static void glVertexArrayVertexBuffers(uint vaobj, uint first, int count, uint[] buffers, IntPtr offsets, int[] strides)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexArrayVertexBuffersDelegate>()(vaobj, first, count, buffers, offsets, strides);
	}

	public static void glVertexArrayAttribBinding(uint vaobj, uint attribindex, uint bindingindex)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexArrayAttribBindingDelegate>()(vaobj, attribindex, bindingindex);
	}

	public static void glVertexArrayAttribFormat(uint vaobj, uint attribindex, int size, uint type, bool normalized, uint relativeoffset)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexArrayAttribFormatDelegate>()(vaobj, attribindex, size, type, normalized, relativeoffset);
	}

	public static void glVertexArrayAttribIFormat(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexArrayAttribIFormatDelegate>()(vaobj, attribindex, size, type, relativeoffset);
	}

	public static void glVertexArrayAttribLFormat(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexArrayAttribLFormatDelegate>()(vaobj, attribindex, size, type, relativeoffset);
	}

	public static void glVertexArrayBindingDivisor(uint vaobj, uint bindingindex, uint divisor)
	{
		OpenGLDllImportBase.GetDelegateFor<glVertexArrayBindingDivisorDelegate>()(vaobj, bindingindex, divisor);
	}

	public static void glGetVertexArrayiv(uint vaobj, uint pname, int[] param)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetVertexArrayivDelegate>()(vaobj, pname, param);
	}

	public static void glGetVertexArrayIndexediv(uint vaobj, uint index, uint pname, int[] param)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetVertexArrayIndexedivDelegate>()(vaobj, index, pname, param);
	}

	public static void glGetVertexArrayIndexed64iv(uint vaobj, uint index, uint pname, Int64[] param)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetVertexArrayIndexed64ivDelegate>()(vaobj, index, pname, param);
	}

	public static void glCreateSamplers(int n, uint[] samplers)
	{
		OpenGLDllImportBase.GetDelegateFor<glCreateSamplersDelegate>()(n, samplers);
	}

	public static void glCreateProgramPipelines(int n, uint[] pipelines)
	{
		OpenGLDllImportBase.GetDelegateFor<glCreateProgramPipelinesDelegate>()(n, pipelines);
	}

	public static void glCreateQueries(uint target, int n, uint[] ids)
	{
		OpenGLDllImportBase.GetDelegateFor<glCreateQueriesDelegate>()(target, n, ids);
	}

	public static void glGetQueryBufferObjecti64v(uint id, uint buffer, uint pname, IntPtr offset)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetQueryBufferObjecti64vDelegate>()(id, buffer, pname, offset);
	}

	public static void glGetQueryBufferObjectiv(uint id, uint buffer, uint pname, IntPtr offset)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetQueryBufferObjectivDelegate>()(id, buffer, pname, offset);
	}

	public static void glGetQueryBufferObjectui64v(uint id, uint buffer, uint pname, IntPtr offset)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetQueryBufferObjectui64vDelegate>()(id, buffer, pname, offset);
	}

	public static void glGetQueryBufferObjectuiv(uint id, uint buffer, uint pname, IntPtr offset)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetQueryBufferObjectuivDelegate>()(id, buffer, pname, offset);
	}

	public static void glMemoryBarrierByRegion(uint barriers)
	{
		OpenGLDllImportBase.GetDelegateFor<glMemoryBarrierByRegionDelegate>()(barriers);
	}

	public static void glGetTextureSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, int bufSize, IntPtr pixels)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetTextureSubImageDelegate>()(texture, level, xoffset, yoffset, zoffset, width, height, depth, format, type, bufSize, pixels);
	}

	public static void glGetCompressedTextureSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, int bufSize, IntPtr pixels)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetCompressedTextureSubImageDelegate>()(texture, level, xoffset, yoffset, zoffset, width, height, depth, bufSize, pixels);
	}

	public static uint glGetGraphicsResetStatus()
	{
		return (uint)OpenGLDllImportBase.GetDelegateFor<glGetGraphicsResetStatusDelegate>()();
	}

	public static void glGetnCompressedTexImage(uint target, int lod, int bufSize, IntPtr pixels)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnCompressedTexImageDelegate>()(target, lod, bufSize, pixels);
	}

	public static void glGetnTexImage(uint target, int level, uint format, uint type, int bufSize, IntPtr pixels)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnTexImageDelegate>()(target, level, format, type, bufSize, pixels);
	}

	public static void glGetnUniformdv(uint program, uint location, int bufSize, double[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnUniformdvDelegate>()(program, location, bufSize, @params);
	}

	public static void glGetnUniformfv(uint program, uint location, int bufSize, float[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnUniformfvDelegate>()(program, location, bufSize, @params);
	}

	public static void glGetnUniformiv(uint program, uint location, int bufSize, int[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnUniformivDelegate>()(program, location, bufSize, @params);
	}

	public static void glGetnUniformuiv(uint program, uint location, int bufSize, uint[] @params)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnUniformuivDelegate>()(program, location, bufSize, @params);
	}

	public static void glReadnPixels(int x, int y, int width, int height, uint format, uint type, int bufSize, IntPtr data)
	{
		OpenGLDllImportBase.GetDelegateFor<glReadnPixelsDelegate>()(x, y, width, height, format, type, bufSize, data);
	}

	public static void glGetnMapdv(uint target, uint query, int bufSize, double[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnMapdvDelegate>()(target, query, bufSize, v);
	}

	public static void glGetnMapfv(uint target, uint query, int bufSize, float[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnMapfvDelegate>()(target, query, bufSize, v);
	}

	public static void glGetnMapiv(uint target, uint query, int bufSize, int[] v)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnMapivDelegate>()(target, query, bufSize, v);
	}

	public static void glGetnPixelMapfv(uint map, int bufSize, float[] values)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnPixelMapfvDelegate>()(map, bufSize, values);
	}

	public static void glGetnPixelMapuiv(uint map, int bufSize, uint[] values)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnPixelMapuivDelegate>()(map, bufSize, values);
	}

	public static void glGetnPixelMapusv(uint map, int bufSize, ushort[] values)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnPixelMapusvDelegate>()(map, bufSize, values);
	}

	public static void glGetnPolygonStipple(int bufSize, byte[] pattern)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnPolygonStippleDelegate>()(bufSize, pattern);
	}

	public static void glGetnColorTable(uint target, uint format, uint type, int bufSize, IntPtr table)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnColorTableDelegate>()(target, format, type, bufSize, table);
	}

	public static void glGetnConvolutionFilter(uint target, uint format, uint type, int bufSize, IntPtr image)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnConvolutionFilterDelegate>()(target, format, type, bufSize, image);
	}

	public static void glGetnSeparableFilter(uint target, uint format, uint type, int rowBufSize, IntPtr row, int columnBufSize, IntPtr column, IntPtr span)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnSeparableFilterDelegate>()(target, format, type, rowBufSize, row, columnBufSize, column, span);
	}

	public static void glGetnHistogram(uint target, bool reset, uint format, uint type, int bufSize, IntPtr values)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnHistogramDelegate>()(target, reset, format, type, bufSize, values);
	}

	public static void glGetnMinmax(uint target, bool reset, uint format, uint type, int bufSize, IntPtr values)
	{
		OpenGLDllImportBase.GetDelegateFor<glGetnMinmaxDelegate>()(target, reset, format, type, bufSize, values);
	}

	public static void glTextureBarrier()
	{
		OpenGLDllImportBase.GetDelegateFor<glTextureBarrierDelegate>()();
	}

	#endregion
}
