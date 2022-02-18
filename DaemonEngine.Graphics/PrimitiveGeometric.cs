using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;

namespace DaemonEngine.Graphics;

public interface IPrimitiveGeometric
{
    IMesh CreateCube(Vector3 scale, IPipeline pipeline);
}

internal sealed class PrimitiveGeometric : IPrimitiveGeometric
{
    public PrimitiveGeometric(IMeshFactory meshFactory)
    {
        MeshFactory = meshFactory;
    }

    private IMeshFactory MeshFactory { get; }

    public IMesh CreateCube(Vector3 scale, IPipeline pipeline)
    {
        scale /= 2;

        var vertices = new float[8 * (3 + 3)]
        {
             1.0f * scale.X,  1.0f * scale.Y, -1.0f * scale.Z, 1.0f, 1.0f, 1.0f, // 0
             1.0f * scale.X, -1.0f * scale.Y, -1.0f * scale.Z, 1.0f, 1.0f, 1.0f, // 1
             1.0f * scale.X,  1.0f * scale.Y,  1.0f * scale.Z, 1.0f, 1.0f, 1.0f, // 2
             1.0f * scale.X, -1.0f * scale.Y,  1.0f * scale.Z, 1.0f, 1.0f, 1.0f, // 3

            -1.0f * scale.X,  1.0f * scale.Y, -1.0f * scale.Z, 1.0f, 1.0f, 1.0f, // 4
            -1.0f * scale.X, -1.0f * scale.Y, -1.0f * scale.Z, 1.0f, 1.0f, 1.0f, // 5
            -1.0f * scale.X,  1.0f * scale.Y,  1.0f * scale.Z, 1.0f, 1.0f, 1.0f, // 6
            -1.0f * scale.X, -1.0f * scale.Y,  1.0f * scale.Z, 1.0f, 1.0f, 1.0f, // 7
        };

        var indices = new uint[12 * 3]
        {
            // RIGHT
            0, 1, 2,
            1, 2, 3,
            
            // LEFT
            4, 5, 6,
            5, 6, 7,
            
            // FRONT
            2, 6, 3,
            6, 3, 7,

            // BACK
            0, 4, 1,
            4, 1, 5,

            // UP
            0, 4, 2,
            4, 2, 6,

            // BOTTOM
            1, 5, 3,
            5, 3, 7,
        };

        return MeshFactory.CreateMesh(vertices, indices, pipeline);
    }
}
