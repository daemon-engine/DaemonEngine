using DaemonEngine.ECS;
using DaemonEngine.ECS.Components;
using DaemonEngine.Mathematics;
using Serilog;

namespace DaemonEngine.Core.Scene;
public sealed class SceneHierarchy
{
    private readonly Scene _scene;
    private IEntity _selectedEntity;

    public SceneHierarchy(Scene scene, ILogger logger)
    {
        _scene = scene;
        Logger = logger;

        _selectedEntity = null;
    }

    protected ILogger Logger { get; }

    public void OnGUI()
    {
        ImGuiNET.ImGui.Begin("Scene Hierarchy");

        foreach (var entity in _scene.Entities)
        {
            var flags = ImGuiNET.ImGuiTreeNodeFlags.SpanAvailWidth | ImGuiNET.ImGuiTreeNodeFlags.OpenOnArrow;

            var opened = ImGuiNET.ImGui.TreeNodeEx(entity.UUID.ToString(), flags, entity.Name);
            if (ImGuiNET.ImGui.IsItemClicked())
            {
                _selectedEntity = entity;
            }
            if (opened)
            {
                ImGuiNET.ImGui.TreePop();
            }
        }

        ImGuiNET.ImGui.End();

        DrawPropertiesPanel();
    }

    private void DrawPropertiesPanel()
    {
        ImGuiNET.ImGui.Begin("Properties");

        if (_selectedEntity != null)
        {
            ImGuiNET.ImGui.Text($"UUID: {_selectedEntity.UUID}");
            ImGuiNET.ImGui.Text($"Name: {_selectedEntity.Name}");

            DrawComponents();
        }

        ImGuiNET.ImGui.End();
    }

    private void DrawComponents()
    {
        DrawComponent("Transform", _selectedEntity, (Transform transform) =>
        {
            var position = (System.Numerics.Vector3)transform.Position;
            ImGuiNET.ImGui.DragFloat3("Position", ref position, 0.1f, -1000.0f, 1000.0f);
            transform.Position = position;

            var rotation = (System.Numerics.Vector3)transform.EulerAngles;
            ImGuiNET.ImGui.DragFloat3("Rotation", ref rotation, 0.1f, -360.0f, 360.0f);
            transform.Rotation = Quaternion.Euler(rotation);

            var scale = (System.Numerics.Vector3)transform.Scale;
            ImGuiNET.ImGui.DragFloat3("Scale", ref scale);
            transform.Scale = scale;
        });

        DrawComponent("Rigidbody", _selectedEntity, (Rigidbody rigidbody) =>
        {
            var rigidbodyTypes = new string[] { "Static", "Dynamic", "Kinematic" };
            var selectedType = rigidbody.Type.ToString();

            var changed = false;

            var opened = ImGuiNET.ImGui.BeginCombo("Type", rigidbody.Type.ToString());
            if (opened)
            {
                for (int i = 0; i < rigidbodyTypes.Length; i++)
                {
                    if (ImGuiNET.ImGui.Selectable(rigidbodyTypes[i]))
                    {
                        selectedType = rigidbodyTypes[i];
                        changed = true;
                    }
                }

                ImGuiNET.ImGui.EndCombo();
            }

            if (changed)
            {
                Logger.Information($"Rigidbody type changed to {rigidbody.Type}");
            }

            var mass = rigidbody.Mass;
            ImGuiNET.ImGui.DragFloat("Mass", ref mass);
            rigidbody.Mass = mass;

            if (ImGuiNET.ImGui.TreeNode("Debugging"))
            {
                var linearVelocity = (System.Numerics.Vector3)rigidbody.LinearVelocity;
                ImGuiNET.ImGui.DragFloat3("Linear Velocity", ref linearVelocity);

                var angularVelocity = (System.Numerics.Vector3)rigidbody.AngularVelocity;
                ImGuiNET.ImGui.DragFloat3("Angular Velocity", ref angularVelocity);

                ImGuiNET.ImGui.TreePop();
            }
        });

        DrawComponent("Box Collider", _selectedEntity, (BoxCollider boxCollider) =>
        {
            var offset = (System.Numerics.Vector3)boxCollider.Offset;
            ImGuiNET.ImGui.DragFloat3("Offset", ref offset, 0.1f);
            boxCollider.Offset = offset;

            var size = (System.Numerics.Vector3)boxCollider.Size;
            ImGuiNET.ImGui.DragFloat3("Size", ref size, 0.1f);
            boxCollider.Size = size;
        });

        DrawComponent("Sphere Collider", _selectedEntity, (SphereCollider sphereCollider) =>
        {
            var offset = (System.Numerics.Vector3)sphereCollider.Offset;
            ImGuiNET.ImGui.DragFloat3("Offset", ref offset, 0.1f);
            sphereCollider.Offset = offset;

            var radius = sphereCollider.Radius;
            ImGuiNET.ImGui.DragFloat("Radius", ref radius, 0.1f);
            sphereCollider.Radius = radius;
        });

        DrawComponent("Mesh Collider", _selectedEntity, (MeshCollider meshCollider) =>
        {
            var size = (System.Numerics.Vector3)meshCollider.Size;
            ImGuiNET.ImGui.DragFloat3("Size", ref size, 0.1f);
            meshCollider.Size = size;
        });

        DrawComponent("Mesh Renderer", _selectedEntity, (MeshRenderer meshRenderer) =>
        {
        });

        DrawComponent("Native Script", _selectedEntity, (NativeScript nativeScript) =>
        {
            ImGuiNET.ImGui.Text($"Script: {nativeScript.ScriptName}");
        });
    }

    private static void DrawComponent<TComponent>(string name, IEntity entity, Action<TComponent> uiFunction)
        where TComponent : class, IComponent
    {
        const ImGuiNET.ImGuiTreeNodeFlags flags =
            ImGuiNET.ImGuiTreeNodeFlags.DefaultOpen |
            ImGuiNET.ImGuiTreeNodeFlags.Framed |
            ImGuiNET.ImGuiTreeNodeFlags.SpanAvailWidth |
            ImGuiNET.ImGuiTreeNodeFlags.AllowItemOverlap |
            ImGuiNET.ImGuiTreeNodeFlags.FramePadding;

        if (entity.HasComponent<TComponent>())
        {
            var opened = ImGuiNET.ImGui.TreeNodeEx("##dummy_id", flags, name);

            if (opened)
            {
                var component = entity.GetComponent<TComponent>()!;

                uiFunction(component);

                ImGuiNET.ImGui.TreePop();
            }
        }
    }
}
