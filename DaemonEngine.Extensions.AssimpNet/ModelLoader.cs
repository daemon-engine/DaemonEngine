using Assimp;

namespace DaemonEngine.Extensions.AssimpNet;

public class ModelLoader
{
    public static (float[] vertices, uint[] indices) LoadModel(string filepath)
    {
        var context = new AssimpContext();

        var scene = context.ImportFile(filepath, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);

        if (scene == default || scene.SceneFlags == SceneFlags.Incomplete || scene.RootNode == default)
        {
            return (new float[0], new uint[0]);
        }

        var node = scene.RootNode;

        List<float> vertices = new();
        uint[] indices = null;
        foreach (var child in node.Children)
        {
            for (int i = 0; i < child.MeshCount; i++)
            {
                var mesh = scene.Meshes[i];

                if (indices == null)
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

                    vertices.Add(0.0f);
                    vertices.Add(0.0f);
                }
            }
        }

        var verticesArray = vertices.ToArray();
        return (verticesArray, indices);
    }
}
