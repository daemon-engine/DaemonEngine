using DaemonEngine.Extensions.OpenGL.Enums;
using System.Runtime.InteropServices;
using System.Text;

namespace DaemonEngine.Extensions.OpenGL;

public static class GL
{
    #region Texture methods
    public static void TexParameteri(uint target, uint pname, uint param)
    {
        OpenGLDllImport.glTexParameteri(target, pname, param);
    }

    public static void GenerateMipmap(uint target)
    {
        OpenGLDllImport.glGenerateMipmap(target);
    }

    public static void TexImage2D(uint target, int level, uint internalFormat, int width, int height, int border, uint format, uint type, byte[] pixels)
    {
        IntPtr pixelsPtr = Marshal.AllocHGlobal(pixels.Length);
        Marshal.Copy(pixels, 0, pixelsPtr, pixels.Length);

        OpenGLDllImport.glTexImage2D(target, level, internalFormat, width, height, border, format, type, pixelsPtr);

        //unsafe
        //{
        //    IntPtr pixelPtr = IntPtr.Zero;
        //    fixed (byte* p = pixels)
        //    {
        //        pixelPtr = (IntPtr)p;
        //    }
        //    OpenGLDllImport.glTexImage2D(target, level, internalFormat, width, height, border, format, type, pixelPtr);
        //}
    }

    public static void BindTexture(uint target, uint texture)
    {
        OpenGLDllImport.glBindTexture(target, texture);
    }

    public static void GenTextures(int count, ref uint[] textures)
    {
        OpenGLDllImport.glGenTextures(count, textures);
    }
    #endregion

    #region Shader Program methods
    public static string GetProgramInfoLog(uint program, int bufferSize, ref int length)
    {
        byte[] buffer = new byte[bufferSize];
        OpenGLDllImport.glGetProgramInfoLog(program, bufferSize, ref length, buffer);
        return Encoding.UTF8.GetString(buffer);
    }

    public static int GetProgramiv(uint program, uint pname)
    {
        int output = 1;
        OpenGLDllImport.glGetProgramiv(program, pname, new int[] { output });
        return output;
    }

    public static void UseProgram(uint program)
    {
        OpenGLDllImport.glUseProgram(program);
    }

    public static void ValidateProgram(uint program)
    {
        OpenGLDllImport.glValidateProgram(program);
    }

    public static void LinkProgram(uint program)
    {
        OpenGLDllImport.glLinkProgram(program);
    }

    public static void AttachShader(uint program, uint shader)
    {
        OpenGLDllImport.glAttachShader(program, shader);
    }

    public static uint CreateProgram()
    {
        return OpenGLDllImport.glCreateProgram();
    }
    #endregion

    #region Shader methods
    public static string GetShaderInfoLog(uint shader, int bufferSize, ref int length)
    {
        byte[] buffer = new byte[bufferSize];
        OpenGLDllImport.glGetShaderInfoLog(shader, bufferSize, ref length, buffer);
        return Encoding.UTF8.GetString(buffer);
    }

    public static int GetShaderiv(uint shader, uint pname)
    {
        int output = 1;
        OpenGLDllImport.glGetShaderiv(shader, pname, ref output);
        return output;
    }

    public static void DeleteShader(uint shader)
    {
        OpenGLDllImport.glDeleteShader(shader);
    }

    public static uint CreateShader(GLShaderType type)
    {
        return OpenGLDllImport.glCreateShader((uint)type);
    }

    public static void ShaderSource(uint shader, int count, string[] source, int[] length)
    {
        OpenGLDllImport.glShaderSource(shader, count, source, length);
    }

    public static void CompileShader(uint shader)
    {
        OpenGLDllImport.glCompileShader(shader);
    }
    #endregion

    #region Draw Calls
    public static void DrawElements(uint mode, int count, uint type)
    {
        OpenGLDllImport.glDrawElements(mode, count, type, IntPtr.Zero);
    }

    public static void DrawElements(uint mode, int count, uint type, IntPtr indices)
    {
        OpenGLDllImport.glDrawElements(mode, count, type, indices);
    }

    public static void DrawArrays(uint mode, int first, int count)
    {
        OpenGLDllImport.glDrawArrays(mode, first, count);
    }
    #endregion

    #region Vertex Array methods
    public static void BindVertexArray(uint array)
    {
        OpenGLDllImport.glBindVertexArray(array);
    }

    public static void GenVertexArrays(int count, ref uint[] arrays)
    {
        OpenGLDllImport.glGenVertexArrays(count, arrays);
    }

    public static void VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, int offset)
    {
        OpenGLDllImport.glVertexAttribPointer(index, size, type, normalized, stride, (IntPtr)offset);
    }

    public static void EnableVertexAttribArray(uint index)
    {
        OpenGLDllImport.glEnableVertexAttribArray(index);
    }
    #endregion

    #region Buffer methods
    public static void BufferData(uint type, int size, uint[] indices, GLBufferUsage usage)
    {
        OpenGLDllImport.glBufferData(type, size, indices, (uint)usage);
    }

    public static void BufferData(uint type, int size, float[] vertices, GLBufferUsage usage)
    {
        OpenGLDllImport.glBufferData(type, size, vertices, (uint)usage);
    }

    public static void BindBuffer(uint type, uint buffer)
    {
        OpenGLDllImport.glBindBuffer(type, buffer);
    }

    public static void GenBuffers(int count, ref uint[] buffers)
    {
        OpenGLDllImport.glGenBuffers(count, buffers);
    }
    #endregion

    public static void ClearColor(float red, float green, float blue, float alpha)
    {
        OpenGLDllImport.glClearColor(red, green, blue, alpha);
    }

    public static void Clear(GLClearMask mask)
    {
        OpenGLDllImport.glClear((uint)mask);
    }

    public static void Clear(uint mask)
    {
        OpenGLDllImport.glClear(mask);
    }

    public static void Viewport(int x, int y, int width, int height)
    {
        OpenGLDllImport.glViewport(x, y, width, height);
    }
}
