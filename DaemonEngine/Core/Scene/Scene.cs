using DaemonEngine.ECS;
using DaemonEngine.ECS.Components;
using DaemonEngine.EventSystem;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;
using DaemonEngine.Physics;
using Serilog;

namespace DaemonEngine.Core.Scene;

public class Scene
{
    public Scene(ILogger logger, IRenderer renderer, IPhysics physics)
    {
        Logger = logger;
        Renderer = renderer;
        Physics = physics;

        Entities = new List<IEntity>();
    }

    protected ILogger Logger { get; }
    protected IRenderer Renderer { get; }
    protected IPhysics Physics { get; }
    internal List<IEntity> Entities { get; }

    //private PhysicsBody[] PhysicsBodyEntityBuffer { get; set; }

    public void RuntimeStart()
    {
        // TODO: make faster...
        var scriptEntities = Entities.Where(entity => entity.HasComponent<NativeScript>());
        foreach (var entity in scriptEntities)
        {
            entity.GetComponent<NativeScript>()!.Script!.Start();
        }

        // Setup physics bodies
        var rigidbodyEntities = Entities.Where(entity => entity.HasComponent<Rigidbody>() && entity.HasComponent<BoxCollider>());
        //PhysicsBodyEntityBuffer = new PhysicsBody[rigidbodyEntities.Count()];
        //int physicsBodyEntityBufferIndex = 0;
        foreach (var entity in rigidbodyEntities)
        {
            var transform = entity.GetComponent<Transform>()!;
            var rigidbody = entity.GetComponent<Rigidbody>()!;
            var boxCollider = entity.GetComponent<BoxCollider>()!;

            var physicsBodyOptions = new PhysicsBodyOptions
            {
                Position = transform.Position,
                BodyType = (PhysicsBodyType)rigidbody.Type,
                Mass = rigidbody.Mass,
                Shape = boxCollider.Shape,
                ColliderSize = boxCollider.Size
            };
            var physicsBody = Physics.CreateBody(physicsBodyOptions);

            //PhysicsBodyEntityBuffer[physicsBodyEntityBufferIndex++] = physicsBody;
            rigidbody.PhysicsBody = physicsBody;
        }
    }

    public void RuntimeUpdate(float deltaTime)
    {
        // TODO: make faster...
        var scripts = Entities.Where(entity => entity.HasComponent<NativeScript>());
        foreach (var script in scripts)
        {
            script.GetComponent<NativeScript>()!.Script!.Update(deltaTime);
        }

        // Physics
        Physics.Step();
        var rigidbodyEntities = Entities.Where(entity => entity.HasComponent<Rigidbody>());
        foreach (var entity in rigidbodyEntities)
        {
            var rigidbody = entity.GetComponent<Rigidbody>()!;
            if (rigidbody.Type == RigidbodyType.Static)
            {
                continue;
            }

            var transform = entity.GetComponent<Transform>()!;

            var bodyRef = Physics.GetBodyReference(rigidbody.PhysicsBody);
            transform.Position = ((BepuPhysics.BodyReference)bodyRef).Pose.Position;
        }

        // Rendering
        var cameraEntity = Entities.Where(entity => entity.HasComponent<Camera>()).SingleOrDefault()!;
        var primaryCamera = cameraEntity.GetComponent<Camera>();

        var skyLight = Entities.Where(entity => entity.HasComponent<SkyLight>()).SingleOrDefault()!;

        if (primaryCamera != null && primaryCamera.Primary)
        {
            Renderer.BeginScene(primaryCamera.MainCamera);
            Renderer.Clear(ClearMask.ColorBufferBit | ClearMask.DepthBufferBit);
            Renderer.ClearColor(0.3f, 0.4f, 0.8f, 1.0f);

            var entities = Entities.Where(entity => entity.HasComponent<MeshRenderer>());
            foreach (var entity in entities)
            {
                var transform = entity.GetComponent<Transform>()!;
                var meshRenderer = entity.GetComponent<MeshRenderer>()!;

                var shader = meshRenderer.Shader;

                shader.Bind();
                shader.SetFloat3("_Material.ambient", meshRenderer.Ambient);
                shader.SetFloat3("_Material.diffuse", meshRenderer.Diffuse);
                shader.SetFloat3("_Material.specular", meshRenderer.Specular);
                shader.SetFloat("_Material.shininess", meshRenderer.Shininess * 128.0f);

                var model = Matrix4.Identity
                    * Matrix4.Scale(transform.Scale)
                    * Matrix4.Rotate(transform.Rotation)
                    * Matrix4.Translate(transform.Position);

                shader.SetMat4("_Model", model);

                Renderer.RenderMesh(meshRenderer!.Model.Mesh);
            }

            // Render colliders
            if (Physics.ShowColliders)
            {
                Physics.RenderColliders();
            }

            Renderer.EndScene();
        }
    }

    public void RuntimeEvent(IEvent e)
    {
        // TODO: make faster...
        var scripts = Entities.Where(entity => entity.HasComponent<NativeScript>());
        foreach (var script in scripts)
        {
            script.GetComponent<NativeScript>()!.Script!.OnEvent(e);
        }
    }

    public void RuntimeStop()
    {
        // TODO: make faster...
        var scripts = Entities.Where(entity => entity.HasComponent<NativeScript>());
        foreach (var script in scripts)
        {
            script.GetComponent<NativeScript>()!.Script!.Stop();
        }

        Entities.Clear();
    }

    public IEntity CreateEntity(string name)
    {
        var newEntity = new EntityBase(name);
        newEntity.AddComponent<Transform>();

        Entities.Add(newEntity);

        Logger.Information($"Created new Entity, {newEntity.Name} ({newEntity.UUID})");

        return newEntity;
    }

    public IEntity AddEntity(IEntity entity)
    {
        Entities.Add(entity);

        Logger.Information($"Added Entity, {entity.Name} ({entity.UUID})");

        return entity;
    }

    public void DeleteEntity(IEntity entity)
    {
        Logger.Information($"Deleted Entity, {entity.Name} ({entity.UUID})");

        Entities.Remove(entity);
    }
}
