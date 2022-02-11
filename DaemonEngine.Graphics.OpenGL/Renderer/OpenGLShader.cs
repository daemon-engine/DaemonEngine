using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;
using DaemonEngine.OpenGL.DllImport;
using DaemonEngine.OpenGL.DllImport.Enums;
using Serilog;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

enum ShaderType
{
    None = 0,
    Vertex = 1,
    Fragment = 2
};

internal class OpenGLShader : ShaderBase
{
    private readonly uint _id;
    private IDictionary<string, uint> _uniformLocations;

    public OpenGLShader(ILogger logger, string filepath)
        : base(logger)
    {
        var shaderSources = ReadFile(filepath);
        var vertexSource = shaderSources[ShaderType.Vertex];
        var fragmentSource = shaderSources[ShaderType.Fragment];

        var vertexShader = CreateShader(GLShaderType.VertexShader, vertexSource);
        var fragmentShader = CreateShader(GLShaderType.FragmentShader, fragmentSource);
        _id = CreateShaderProgram(vertexShader, fragmentShader);

        GetActiveUniforms(filepath);
    }
    public OpenGLShader(ILogger logger, string vertexShaderSource, string fragmentShaderSource)
        : base(logger)
    {
        var vertexShader = CreateShader(GLShaderType.VertexShader, vertexShaderSource);
        var fragmentShader = CreateShader(GLShaderType.FragmentShader, fragmentShaderSource);
        _id = CreateShaderProgram(vertexShader, fragmentShader);

        GetActiveUniforms("ImGui");
    }

    protected override void GetActiveUniforms(string shaderName)
    {
        var activeUniformsCount = GL.GetProgramiv(_id, GLConstants.GL_ACTIVE_UNIFORMS);
        _uniformLocations = new Dictionary<string, uint>(activeUniformsCount);

        for (uint i = 0; i < activeUniformsCount; i++)
        {
            GL.GetActiveUniform(_id, i, 128, out _, out _, out _, out string name);

            var location = GL.GetUniformLocation(_id, name);

            _uniformLocations[name] = location;
        }

        Logger.Information($"Loaded shader '{shaderName}' with {activeUniformsCount} active uniforms");
    }

    public override void Bind()
    {
        GL.UseProgram(_id);
    }

    public override void Unbind()
    {
        GL.UseProgram(0);
    }

    public override void SetInt(string name, int value)
    {
        var location = GetUniformLocation(name);

        GL.Uniform1i(location, value);
    }

    public override void SetFloat(string name, float value)
    {
        var location = GetUniformLocation(name);

        GL.Uniform1f(location, value);
    }

    public override void SetFloat2(string name, Vector2 values)
    {
        SetFloat2(name, values.X, values.Y);
    }

    public override void SetFloat2(string name, float v0, float v1)
    {
        var location = GetUniformLocation(name);

        GL.Uniform2f(location, v0, v1);
    }

    public override void SetFloat3(string name, Vector3 values)
    {
        SetFloat3(name, values.X, values.Y, values.Z);
    }

    public override void SetFloat3(string name, float v0, float v1, float v2)
    {
        var location = GetUniformLocation(name);

        GL.Uniform3f(location, v0, v1, v2);
    }

    public override void SetFloat4(string name, Vector4 values)
    {
        SetFloat4(name, values.X, values.Y, values.Z, values.W);
    }

    public override void SetFloat4(string name, float v0, float v1, float v2, float v3)
    {
        var location = GetUniformLocation(name);

        GL.Uniform4f(location, v0, v1, v2, v3);
    }

    public override void SetMat4(string name, Matrix4 matrix, bool transpose = false)
    {
        var location = GetUniformLocation(name);

        float[] mat =
        {
            matrix.Row0.X, matrix.Row0.Y, matrix.Row0.Z, matrix.Row0.W,
            matrix.Row1.X, matrix.Row1.Y, matrix.Row1.Z, matrix.Row1.W,
            matrix.Row2.X, matrix.Row2.Y, matrix.Row2.Z, matrix.Row2.W,
            matrix.Row3.X, matrix.Row3.Y, matrix.Row3.Z, matrix.Row3.W
        };

        GL.UniformMatrix4fv(location, 1, transpose, mat);
    }

    private uint GetUniformLocation(string name)
    {
        if (!_uniformLocations.ContainsKey(name))
        {
            Logger.Warning($"Shader doesn't have an uniform location at the name: {name}");
            return 0;
        }

        return _uniformLocations[name];
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
            Logger.Warning($"Failed to link Shader Program {program}\n{infoLog}");
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
            Logger.Warning($"Failed to compile Shader {shader} ({type})\n{infoLog}");
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
