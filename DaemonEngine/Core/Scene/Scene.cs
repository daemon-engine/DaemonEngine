using DaemonEngine.ECS;
using DaemonEngine.ECS.Components;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;
using Serilog;

namespace DaemonEngine.Core.Scene;

public class Scene
{
    public Scene(ILogger logger, IRenderer renderer)
    {
        Logger = logger;
        Renderer = renderer;
        Entities = new List<EntityBase>();
    }

    protected ILogger Logger { get; }
    protected IRenderer Renderer { get; }
    protected List<EntityBase> Entities { get; }

    public void Update(float deltaTime)
    {
        foreach (var entity in Entities)
        {
            if(entity.HasComponent<Transform>() && entity.HasComponent<MeshRenderer>())
            {
                var transform = entity.GetComponent<Transform>()!;
                var meshRenderer = entity.GetComponent<MeshRenderer>()!;

                var shader = meshRenderer.Shader;

                shader.Bind();
                shader.SetFloat3("_Material.ambient", meshRenderer.Ambient);
                shader.SetFloat3("_Material.diffuse", meshRenderer.Diffuse);
                shader.SetFloat3("_Material.specular", meshRenderer.Specular);
                shader.SetFloat("_Material.shininess", meshRenderer.Shininess * 128.0f);

                shader.SetMat4("_Model", Matrix4.Identity * Matrix4.Translate(transform.Position));

                Renderer.RenderMesh(meshRenderer!.Model.Mesh);
            }
        }
    }

    public EntityBase CreateEntity(string name)
    {
        var newEntity = new EntityBase(name);
        newEntity.AddComponent<Transform>();

        Entities.Add(newEntity);

        Logger.Information($"Created new Entity, {newEntity.Name} ({newEntity.UUID})");

        return newEntity;
    }

    public EntityBase AddEntity(EntityBase entity)
    {
        Entities.Add(entity);

        Logger.Information($"Added Entity, {entity.Name} ({entity.UUID})");

        return entity;
    }

    public void DeleteEntity(EntityBase entity)
    {
        Logger.Information($"Deleted Entity, {entity.Name} ({entity.UUID})");

        Entities.Remove(entity);
    }
}
