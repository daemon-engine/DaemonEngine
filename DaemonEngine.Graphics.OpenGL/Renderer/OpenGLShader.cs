using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;
using DaemonEngine.OpenGL.DllImport;
using DaemonEngine.OpenGL.DllImport.Enums;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

enum ShaderType
{
    None = 0,
    Vertex = 1,
    Fragment = 2
};

internal class OpenGLShader : IShader
{
    private readonly uint _id;

    public OpenGLShader(string filepath)
    {
        var shaderSources = ReadFile(filepath);
        var vertexSource = shaderSources[ShaderType.Vertex];
        var fragmentSource = shaderSources[ShaderType.Fragment];

        var vertexShader = CreateShader(GLShaderType.VertexShader, vertexSource);
        var fragmentShader = CreateShader(GLShaderType.FragmentShader, fragmentSource);
        _id = CreateShaderProgram(vertexShader, fragmentShader);
    }
    public OpenGLShader(string vertexShaderSource, string fragmentShaderSource)
    {
        var vertexShader = CreateShader(GLShaderType.VertexShader, vertexShaderSource);
        var fragmentShader = CreateShader(GLShaderType.FragmentShader, fragmentShaderSource);
        _id = CreateShaderProgram(vertexShader, fragmentShader);
    }

    public void Bind()
    {
        GL.UseProgram(_id);
    }

    public void Unbind()
    {
        GL.UseProgram(0);
    }

    public void SetInt(string name, int value)
    {
        GL.Uniform1i(GL.GetUniformLocation(_id, name), value);
    }

    public void SetFloat(string name, float value)
    {
        GL.Uniform1f(GL.GetUniformLocation(_id, name), value);
    }

    public void SetFloat2(string name, Vector2 values)
    {
        SetFloat2(name, values.X, values.Y);
    }

    public void SetFloat2(string name, float v0, float v1)
    {
        GL.Uniform2f(GL.GetUniformLocation(_id, name), v0, v1);
    }

    public void SetFloat3(string name, Vector3 values)
    {
        SetFloat3(name, values.X, values.Y, values.Z);
    }

    public void SetFloat3(string name, float v0, float v1, float v2)
    {
        GL.Uniform3f(GL.GetUniformLocation(_id, name), v0, v1, v2);
    }

    public void SetFloat4(string name, Vector4 values)
    {
        SetFloat4(name, values.X, values.Y, values.Z, values.W);
    }

    public void SetFloat4(string name, float v0, float v1, float v2, float v3)
    {
        GL.Uniform4f(GL.GetUniformLocation(_id, name), v0, v1, v2, v3);
    }

    public void SetMat4(string name, Matrix4 matrix, bool transpose = false)
    {
        float[] mat =
        {
            matrix.Row0.X, matrix.Row0.Y, matrix.Row0.Z, matrix.Row0.W,
            matrix.Row1.X, matrix.Row1.Y, matrix.Row1.Z, matrix.Row1.W,
            matrix.Row2.X, matrix.Row2.Y, matrix.Row2.Z, matrix.Row2.W,
            matrix.Row3.X, matrix.Row3.Y, matrix.Row3.Z, matrix.Row3.W
        };

        GL.UniformMatrix4fv(GL.GetUniformLocation(_id, name), 1, transpose, mat);
    }

    private uint CreateShaderProgram(uint vertexShader, uint fragmentShader)
    {
        var program = GL.CreateProgram();
        GL.AttachShader(program, vertexShader);
        GL.AttachShader(program, fragmentShader);
        GL.ValidateProgram(program);
        GL.LinkProgram(program);

        var success = GL.GetProgramiv(program, GLConstants.GL_LINK_STATUS);
        if (success != GLConstants.GL_TRUE)
        {
            int length = 0;
            var infoLog = GL.GetProgramInfoLog(program, 512, ref length);
            System.Diagnostics.Debugger.Break();
            //Logger.Warning($"Failed to link Shader Program {program}\n{infoLog}");
        }

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);
        return program;
    }

    private uint CreateShader(GLShaderType type, string source)
    {
        var shader = GL.CreateShader(type);
        GL.ShaderSource(shader, 1, new string[] { source }, null);
        GL.CompileShader(shader);

        var success = GL.GetShaderiv(shader, GLConstants.GL_COMPILE_STATUS);
        if (success != GLConstants.GL_TRUE)
        {
            int length = 0;
            var infoLog = GL.GetShaderInfoLog(shader, 512, ref length);
            System.Diagnostics.Debugger.Break();
            //Logger.Warning($"Failed to compile Shader {shader}\n{infoLog}");
        }

        return shader;
    }

    private IDictionary<ShaderType, string> ReadFile(string filepath)
    {
        var lines = File.ReadAllLines(filepath);
        var shaderType = ShaderType.None;
        IDictionary<ShaderType, string> shaderSources = new Dictionary<ShaderType, string>();

        var typeKeyword = "#type";
        var shaderSource = "";
        foreach (var line in lines)
        {
            if (line.Contains(typeKeyword))
            {
                if (!string.IsNullOrWhiteSpace(shaderSource))
                {
                    shaderSources[shaderType] = shaderSource;
                    shaderSource = string.Empty;
                }

                var type = line.Substring(typeKeyword.Length + 1, line.Length - (typeKeyword.Length + 1));
                type = string.Concat(type[0].ToString().ToUpper(), type.AsSpan(1));
                shaderType = (ShaderType)Enum.Parse(typeof(ShaderType), type);
                shaderSources.Add(shaderType, "");
                continue;
            }

            shaderSource += line + "\n";
        }

        shaderSources[shaderType] = shaderSource;
        return shaderSources;
    }
}
