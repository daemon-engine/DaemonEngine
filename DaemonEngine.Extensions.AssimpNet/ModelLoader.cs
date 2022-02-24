using Assimp;

namespace DaemonEngine.Extensions.AssimpNet;

public class ModelLoader
{
    public static IEnumerable<ModelData> LoadModels(string filepath)
    {
        var context = new AssimpContext();
        var result = new List<ModelData>();

        var scene = context.ImportFile(filepath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);

        if (scene == default || scene.SceneFlags == SceneFlags.Incomplete || scene.RootNode == default)
        {
            return Enumerable.Empty<ModelData>();
        }

        var node = scene.RootNode;

        List<float> vertices = new();
        uint[] indices = Array.Empty<uint>();
        foreach (var child in node.Children)
        {
            vertices.Clear();
            indices = Array.Empty<uint>();

            for (int i = 0; i < child.MeshCount; i++)
            {
                var mesh = scene.Meshes[i];

                if (indices == null || indices.Length == 0)
                {
                    indices = mesh.GetUnsignedIndices();
                }

                for (int j = 0; j < mesh.VertexCount; j++)
                {
                    vertices.Add(mesh.Vertices[j].X);
                    vertices.Add(mesh.Vertices[j].Y);
                    vertices.Add(mesh.Vertices[j].Z);

                    vertices.Add(mesh.Normals[j].X);
                    vertices.Add(mesh.Normals[j].Y);
                    vertices.Add(mesh.Normals[j].Z);

                    if (mesh.HasTextureCoords(0))
                    {
                        vertices.Add(mesh.TextureCoordinateChannels[0][j].X);
                        vertices.Add(mesh.TextureCoordinateChannels[0][j].Y);
                    }
                    else
                    {
                        vertices.Add(0.0f);
                        vertices.Add(0.0f);
                    }
                }
            }

            var verticesArray = vertices.ToArray();
            result.Add(new ModelData
            {
                Vertices = verticesArray,
                Indices = indices
            });
        }

        return result;
    }

    public static (float[] vertices, uint[] indices) LoadModel(string filepath)
    {
        var context = new AssimpContext();

        var scene = context.ImportFile(filepath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);

        if (scene == default || scene.SceneFlags == SceneFlags.Incomplete || scene.RootNode == default)
        {
            return (Array.Empty<float>(), Array.Empty<uint>());
        }

        var node = scene.RootNode;

        List<float> vertices = new();
        uint[] indices = Array.Empty<uint>();
        foreach (var child in node.Children)
        {
            for (int i = 0; i < child.MeshCount; i++)
            {
                var mesh = scene.Meshes[i];

                if (indices == null || indices.Length == 0)
                {
                    indices = mesh.GetUnsignedIndices();
                }

                for (int j = 0; j < mesh.VertexCount; j++)
                {
                    vertices.Add(mesh.Vertices[j].X);
                    vertices.Add(mesh.Vertices[j].Y);
                    vertices.Add(mesh.Vertices[j].Z);

                    vertices.Add(mesh.Normals[j].X);
                    vertices.Add(mesh.Normals[j].Y);
                    vertices.Add(mesh.Normals[j].Z);

                    vertices.Add(mesh.TextureCoordinateChannels[0][j].X);
                    vertices.Add(mesh.TextureCoordinateChannels[0][j].Y);
                }
            }
        }

        var verticesArray = vertices.ToArray();
        return (verticesArray, indices);
    }
}
