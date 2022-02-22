using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;

namespace DaemonEngine.Graphics;

public interface IPrimitiveGeometric
{
    IMesh CreateMesh(float[] vertices, uint[] indices, IPipeline pipeline);
    IMesh CreateMesh(IPipeline pipeline);
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

    public IMesh CreateMesh(float[] vertices, uint[] indices, IPipeline pipeline)
    {
        var v_ = new List<float>();
        for (int i = 0; i < vertices.Length; i += 8)
        {
            v_.Add(vertices[i + 0]);
            v_.Add(vertices[i + 1]);
            v_.Add(vertices[i + 2]);

            v_.Add(0.0f);
            v_.Add(0.9f);
            v_.Add(0.9f);
        }

        vertices = v_.ToArray();
        return MeshFactory.CreateMesh(vertices, indices, pipeline);
    }

    public IMesh CreateMesh(IPipeline pipeline)
    {
        var vertices = new float[4 * (3 + 3)]
        {
            0.0f, 0.0f, 0.0f, 0.0f, 0.9f, 0.1f,
            1.0f, 0.0f, 0.0f, 0.0f, 0.9f, 0.1f,
            0.0f, 1.0f, 0.0f, 0.0f, 0.9f, 0.1f,
            1.0f, 1.0f, 0.0f, 0.0f, 0.9f, 0.1f,
        };

        var indices = new uint[6]
        {
            0, 2, 1,
            2, 3, 1
        };

        return MeshFactory.CreateMesh(vertices, indices, pipeline);
    }

    public IMesh CreateSphere(float radius, IPipeline pipeline)
    {
        radius /= 2;

        var tesselation = 16;
        var verticalSegments = tesselation;
        var horizontalSegments = tesselation * 2;

        var vertices = new List<float>();
        var indices = new uint[verticalSegments * (horizontalSegments + 1) * 6];

        // generate the first extremity points
        for (int j = 0; j < horizontalSegments; j++)
        {
            var normal = new Vector3(0.0f, -1.0f, 0.0f);

            vertices.AddRange(new[] { normal.X * radius, normal.Y * radius, normal.Z * radius, 0.0f, 0.9f, 0.1f });
        }

        // Create rings of vertices at progressively higher latitudes.
        for (int i = 1; i < verticalSegments; i++)
        {
            var latitude = (float)((i * Math.PI / verticalSegments) - Math.PI / 2.0);
            var dy = MathF.Sin(latitude);
            var dxz = MathF.Cos(latitude);

            // the first point
            var firstNormal = new Vector3(0, dy, dxz);

            //vertices.AddRange(new[] { firstNormal.X * radius, firstNormal.Y * radius, firstNormal.Z * radius, firstNormal.X, firstNormal.Y, firstNormal.Z, 0.0f, v });
            vertices.AddRange(new[] { firstNormal.X * radius, firstNormal.Y * radius, firstNormal.Z * radius, 0.0f, 0.9f, 0.1f });

            for (int j = 1; j < horizontalSegments; j++)
            {
                var longitude = (float)(j * 2.0 * Math.PI / horizontalSegments);
                var dx = MathF.Sin(longitude);
                var dz = MathF.Cos(longitude);

                dx *= dxz;
                dz *= dxz;

                var normal = new Vector3(dx, dy, dz);

                vertices.AddRange(new[] { normal.X * radius, normal.Y * radius, normal.Z * radius, 0.0f, 0.9f, 0.1f });
            }

            // the last point equal to the first point
            //vertices.AddRange(new[] { uScale, v });
        }

        // generate the end extremity points
        for (int j = 0; j <= horizontalSegments; j++)
        {
            var normal = new Vector3(0, 1, 0);

            vertices.AddRange(new[] { normal.X * radius, normal.Y * radius, normal.Z * radius, 0.0f, 0.9f, 0.1f });
        }

        // Fill the index buffer with triangles joining each pair of latitude rings.
        uint stride = (uint)horizontalSegments + 1;

        uint indexCount = 0;
        for (uint i = 0; i < verticalSegments; i++)
        {
            for (uint j = 0; j <= horizontalSegments; j++)
            {
                uint nextI = i + 1;
                uint nextJ = (j + 1) % stride;

                indices[indexCount++] = (i * stride + j);
                indices[indexCount++] = (nextI * stride + j);
                indices[indexCount++] = (i * stride + nextJ);

                indices[indexCount++] = (i * stride + nextJ);
                indices[indexCount++] = (nextI * stride + j);
                indices[indexCount++] = (nextI * stride + nextJ);
            }
        }

        return MeshFactory.CreateMesh(vertices.ToArray(), indices, pipeline);
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
