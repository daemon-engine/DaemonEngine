using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;

namespace DaemonEngine.Graphics;

public interface IPrimitiveGeometric
{
    IMesh CreateSphere(float radius, IPipeline pipeline);
    IMesh CreateCube(Vector3 size, IPipeline pipeline);
}

internal sealed class PrimitiveGeometric : IPrimitiveGeometric
{
    public PrimitiveGeometric(IMeshFactory meshFactory)
    {
        MeshFactory = meshFactory;
    }

    private IMeshFactory MeshFactory { get; }

    public IMesh CreateSphere(float radius, IPipeline pipeline)
    {
        radius /= 2;

        var tesselation = 16;
        var verticalSegments = tesselation;
        var horizontalSegments = tesselation * 2;



        throw new NotImplementedException();
    }

    public IMesh CreateCube(Vector3 size, IPipeline pipeline)
    {
        size /= 2;

        var vertices = new float[8 * (3 + 3)]
        {
             1.0f * size.X,  1.0f * size.Y, -1.0f * size.Z, 0.0f, 0.9f, 0.1f, // 0
             1.0f * size.X, -1.0f * size.Y, -1.0f * size.Z, 0.0f, 0.9f, 0.1f, // 1
             1.0f * size.X,  1.0f * size.Y,  1.0f * size.Z, 0.0f, 0.9f, 0.1f, // 2
             1.0f * size.X, -1.0f * size.Y,  1.0f * size.Z, 0.0f, 0.9f, 0.1f, // 3

            -1.0f * size.X,  1.0f * size.Y, -1.0f * size.Z, 0.0f, 0.9f, 0.1f, // 4
            -1.0f * size.X, -1.0f * size.Y, -1.0f * size.Z, 0.0f, 0.9f, 0.1f, // 5
            -1.0f * size.X,  1.0f * size.Y,  1.0f * size.Z, 0.0f, 0.9f, 0.1f, // 6
            -1.0f * size.X, -1.0f * size.Y,  1.0f * size.Z, 0.0f, 0.9f, 0.1f, // 7
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
