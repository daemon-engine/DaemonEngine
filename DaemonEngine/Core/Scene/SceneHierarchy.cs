using DaemonEngine.ECS;
using DaemonEngine.ECS.Components;

namespace DaemonEngine.Core.Scene;
public sealed class SceneHierarchy
{
    private readonly Scene _scene;
    private IEntity _selectedEntity;

    public SceneHierarchy(Scene scene)
    {
        _scene = scene;
        _selectedEntity = null;
    }

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

            DrawComponent("Transform", _selectedEntity, (Transform transform) =>
            {
                var position = (System.Numerics.Vector3)transform.Position;
                ImGuiNET.ImGui.DragFloat3("Position", ref position);
                transform.Position = position;

                var rotation = (System.Numerics.Vector3)transform.Rotation;
                ImGuiNET.ImGui.DragFloat3("Rotation", ref rotation);
                transform.Rotation = rotation;

                var scale = (System.Numerics.Vector3)transform.Scale;
                ImGuiNET.ImGui.DragFloat3("Scale", ref scale);
                transform.Scale = scale;
            });

            DrawComponent("Mesh Renderer", _selectedEntity, (MeshRenderer meshRenderer) =>
            {
            });

            DrawComponent("Native Script", _selectedEntity, (NativeScript nativeScript) =>
            {
                ImGuiNET.ImGui.Text($"Script: {nativeScript.Script!.GetType().Name}");
            });
        }

        ImGuiNET.ImGui.End();
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

            if(opened)
            {
                var component = entity.GetComponent<TComponent>()!;

                uiFunction(component);

                ImGuiNET.ImGui.TreePop();
            }
        }
    }
}
