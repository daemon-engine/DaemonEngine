using DaemonEngine.Graphics.OpenGL.DllImport.Enums;
using DaemonEngine.OpenGL.DllImport.Enums;
using System.Runtime.InteropServices;
using System.Text;

namespace DaemonEngine.OpenGL.DllImport;

public static class GL
{
    public static void CheckGLError(string title)
    {
        var error = GL.GetError();
        if (error != GLError.NoError)
        {
            Console.WriteLine($"({error}) {title}");
        }
    }

    #region Texture methods
    public static void TexParameteri(uint target, uint pname, uint param)
    {
        OpenGLDllImport.glTexParameteri(target, pname, param);
    }

    public static void GenerateMipmap(uint target)
    {
        OpenGLDllImport.glGenerateMipmap(target);
    }

    public static void TextureSubImage2D(uint texture, int level, int xOffset, int yOffset, int width, int height, uint format, uint type, IntPtr pixels)
    {
        OpenGLDllImport.glTextureSubImage2D(texture, level, xOffset, yOffset, width, height, format, type, pixels);
    }

    public static void TextureStorage2D(uint texture, int levels, uint internalFormat, int width, int height)
    {
        OpenGLDllImport.glTextureStorage2D(texture, levels, internalFormat, width, height);
    }

    public static void TexImage2D(uint target, int level, uint internalFormat, int width, int height, int border, uint format, uint type, byte[] pixels)
    {
        IntPtr pixelsPtr = Marshal.AllocHGlobal(pixels.Length);
        Marshal.Copy(pixels, 0, pixelsPtr, pixels.Length);

        OpenGLDllImport.glTexImage2D(target, level, internalFormat, width, height, border, format, type, pixelsPtr);
    }

    public static void ActiveTexture(uint textureSlot)
    {
        OpenGLDllImport.glActiveTexture(textureSlot);
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

    #region Uniform methods
    public static void UniformMatrix4fv(uint location, int count, bool transpose, float[] matrix)
    {
        OpenGLDllImport.glUniformMatrix4fv(location, count, transpose, matrix);
    }

    public static void Uniform1i(uint location, int value)
    {
        OpenGLDllImport.glUniform1i(location, value);
    }

    public static void Uniform1f(uint location, float value)
    {
        OpenGLDllImport.glUniform1f(location, value);
    }

    public static void Uniform2f(uint location, float v0, float v1)
    {
        OpenGLDllImport.glUniform2f(location, v0, v1);
    }

    public static void Uniform3f(uint location, float v0, float v1, float v2)
    {
        OpenGLDllImport.glUniform3f(location, v0, v1, v2);
    }

    public static uint GetUniformLocation(uint program, string name)
    {
        return OpenGLDllImport.glGetUniformLocation(program, name);
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

    public static GLError GetError()
    {
        return (GLError)OpenGLDllImport.glGetError();
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

    public static void CreateVertexArrays(int count, ref uint[] arrays)
    {
        OpenGLDllImport.glCreateVertexArrays(count, arrays);
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
    public static void NamedBufferData(uint buffer, IntPtr size, IntPtr data, GLBufferUsage bufferUsage)
    {
        OpenGLDllImport.glNamedBufferData(buffer, size, data, (uint)bufferUsage);
    }

    public static void NamedBufferSubData(uint buffer, IntPtr offset, IntPtr size, IntPtr data)
    {
        OpenGLDllImport.glNamedBufferSubData(buffer, offset, size, data);
    }

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

    public static void CreateBuffers(int v, ref uint[] ids)
    {
        OpenGLDllImport.glCreateBuffers(v, ids);
    }

    public static void GenBuffers(int count, ref uint[] buffers)
    {
        OpenGLDllImport.glGenBuffers(count, buffers);
    }
    #endregion

    #region Blend methods
    public static void BlendEquation(uint mode)
    {
        OpenGLDllImport.glBlendEquation(mode);
    }

    public static void BlendFunc(uint sfactor, uint dfactor)
    {
        OpenGLDllImport.glBlendFunc(sfactor, dfactor);
    }
    #endregion

    public static void VertexArrayAttribFormat(uint vao, uint attribIndex, int size, uint type, bool normalized, uint relativeOffset)
    {
        OpenGLDllImport.glVertexArrayAttribFormat(vao, attribIndex, size, type, normalized, relativeOffset);
    }

    public static void VertexArrayAttribBinding(uint vao, uint attribIndex, uint bindingIndex)
    {
        OpenGLDllImport.glVertexArrayAttribBinding(vao, attribIndex, bindingIndex);
    }

    public static void EnableVertexArrayAttrib(uint vao, uint index)
    {
        OpenGLDllImport.glEnableVertexArrayAttrib(vao, index);
    }

    public static void VertexArrayVertexBuffer(uint vao, uint bindingIndex, uint vbo, IntPtr offset, int stride)
    {
        OpenGLDllImport.glVertexArrayVertexBuffer(vao, bindingIndex, vbo, offset, stride);
    }

    public static void VertexArrayElementBuffer(uint vao, uint ebo)
    {
        OpenGLDllImport.glVertexArrayElementBuffer(vao, ebo);
    }

    public static void Scissor(int x, int y, int width, int height)
    {
        OpenGLDllImport.glScissor(x, y, width, height);
    }

    public static void Enable(GLCapabilities capabilities)
    {
        Enable((uint)capabilities);
    }

    public static void Enable(uint capabilities)
    {
        OpenGLDllImport.glEnable(capabilities);
    }

    public static void Disable(GLCapabilities capabilities)
    {
        Disable((uint)capabilities);
    }

    public static void Disable(uint capabilities)
    {
        OpenGLDllImport.glDisable(capabilities);
    }

    public static void ClearColor(float red, float green, float blue, float alpha)
    {
        OpenGLDllImport.glClearColor(red, green, blue, alpha);
    }

    public static void Clear(GLClearMask mask)
    {
        Clear((uint)mask);
    }

    public static void Clear(uint mask)
    {
        OpenGLDllImport.glClear(mask);
    }

    public static void Viewport(int x, int y, int width, int height)
    {
        OpenGLDllImport.glViewport(x, y, width, height);
    }

    public static void DrawElementsBaseVertex(uint mode, int count, uint type, IntPtr indices, int baseVertex)
    {
        OpenGLDllImport.glDrawElementsBaseVertex(mode, count, type, indices, baseVertex);
    }

    public static string GetString(uint name)
    {
        unsafe
        {
            var result = OpenGLDllImport.glGetString(name);
            return new string(result);
        }
    }
}
