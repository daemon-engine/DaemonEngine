using DaemonEngine.Core.Inputs;
using DaemonEngine.Core.Layer;
using DaemonEngine.EventSystem;
using DaemonEngine.EventSystem.Events.Key;
using DaemonEngine.EventSystem.Events.Mouse;
using DaemonEngine.EventSystem.Events.Window;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Windows;
using DaemonEngine.Windows.Inputs;
using ImGuiNET;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Numerics;

namespace DaemonEngine.Core.ImGui;
public class ImGuiLayer : LayerBase
{
    private ImGuiController _imGuiController;

    public ImGuiLayer(string name, IServiceProvider serviceProvider)
        : base(name, serviceProvider)
    {
        Logger = ServiceProvider.GetRequiredService<ILogger>();
        GraphicsFactory = ServiceProvider.GetRequiredService<IGraphicsFactory>();
    }

    protected ILogger Logger { get; }
    protected IGraphicsFactory GraphicsFactory { get; }

    public override void OnStart()
    {
        var window = ServiceProvider.GetRequiredService<IWindow>();
        var input = ServiceProvider.GetRequiredService<IInput>();

        _imGuiController = new ImGuiController(Logger, GraphicsFactory, input, window.Width, window.Height);

        SetKeyMappings();
    }

    private void SetKeyMappings()
    {
        _imGuiController.MapImGuiKey(ImGuiKey.Tab, (int)Keycode.TAB);
        _imGuiController.MapImGuiKey(ImGuiKey.LeftArrow, (int)Keycode.LEFT);
        _imGuiController.MapImGuiKey(ImGuiKey.RightArrow, (int)Keycode.RIGHT);
        _imGuiController.MapImGuiKey(ImGuiKey.UpArrow, (int)Keycode.UP);
        _imGuiController.MapImGuiKey(ImGuiKey.DownArrow, (int)Keycode.DOWN);
        _imGuiController.MapImGuiKey(ImGuiKey.PageUp, (int)Keycode.PAGE_UP);
        _imGuiController.MapImGuiKey(ImGuiKey.PageDown, (int)Keycode.PAGE_DOWN);
        _imGuiController.MapImGuiKey(ImGuiKey.Home, (int)Keycode.HOME);
        _imGuiController.MapImGuiKey(ImGuiKey.End, (int)Keycode.END);
        _imGuiController.MapImGuiKey(ImGuiKey.Delete, (int)Keycode.DELETE);
        _imGuiController.MapImGuiKey(ImGuiKey.Backspace, (int)Keycode.BACKSPACE);
        _imGuiController.MapImGuiKey(ImGuiKey.Enter, (int)Keycode.ENTER);
        _imGuiController.MapImGuiKey(ImGuiKey.Escape, (int)Keycode.ESCAPE);
        _imGuiController.MapImGuiKey(ImGuiKey.A, (int)Keycode.A);
        _imGuiController.MapImGuiKey(ImGuiKey.C, (int)Keycode.C);
        _imGuiController.MapImGuiKey(ImGuiKey.V, (int)Keycode.V);
        _imGuiController.MapImGuiKey(ImGuiKey.X, (int)Keycode.X);
        _imGuiController.MapImGuiKey(ImGuiKey.Y, (int)Keycode.Y);
        _imGuiController.MapImGuiKey(ImGuiKey.Z, (int)Keycode.Z);
    }

    public override void OnShutdown()
    {
        _imGuiController.Dispose();
    }

    public void Start()
    {
        _imGuiController.BeginImGuiFrame();
    }

    public void End()
    {
        _imGuiController.Render();
    }

    public override void OnUpdate(float deltaTime)
    {
        _imGuiController.SetPerFrameImGuiData(deltaTime);

        _imGuiController.UpdateImGuiMousePosition(Input.GetMousePosition());

        _imGuiController.UpdateImGuiMouseButtonDownState((int)MouseButton.BUTTON_LEFT, Input.IsMouseButtonPressed(MouseButton.BUTTON_LEFT));
        _imGuiController.UpdateImGuiMouseButtonDownState((int)MouseButton.BUTTON_RIGHT, Input.IsMouseButtonPressed(MouseButton.BUTTON_RIGHT));
        _imGuiController.UpdateImGuiMouseButtonDownState((int)MouseButton.BUTTON_MIDDLE, Input.IsMouseButtonPressed(MouseButton.BUTTON_MIDDLE));

        _imGuiController.UpdateImGuiControlKeyDownState(Input.IsKeyPressed(Keycode.LEFT_CONTROL) || Input.IsKeyPressed(Keycode.RIGHT_CONTROL));
        _imGuiController.UpdateImGuiAltKeyDownState(Input.IsKeyPressed(Keycode.LEFT_ALT) || Input.IsKeyPressed(Keycode.RIGHT_ALT));
        _imGuiController.UpdateImGuiShiftKeyDownState(Input.IsKeyPressed(Keycode.LEFT_SHIFT) || Input.IsKeyPressed(Keycode.RIGHT_SHIFT));
        _imGuiController.UpdateImGuiSuperKeyDownState(Input.IsKeyPressed(Keycode.LEFT_SUPER) || Input.IsKeyPressed(Keycode.RIGHT_SUPER));

        foreach (Keycode key in Enum.GetValues(typeof(Keycode)))
        {
            if (key == Keycode.UNKNOWN)
            {
                continue;
            }

            _imGuiController.UpdateImGuiKeyDownState((int)key, Input.IsKeyPressed(key));
        }

        _imGuiController.UpdateImGuiPressedCharacters();
    }

    public override void OnEvent(IEvent e)
    {
        EventDispatcher dispatcher = new(e);
        dispatcher.Dispatch<WindowResizeEvent>(OnWindowResizeEvent);
        dispatcher.Dispatch<KeyTypedEvent>(OnKeyTypedEvent);
        dispatcher.Dispatch<MouseScrolledEvent>(OnMouseScrollEvent);
    }

    private bool OnWindowResizeEvent(WindowResizeEvent e)
    {
        _imGuiController.ResizeImGuiContext(e.Width, e.Height);
        return true;
    }

    private bool OnKeyTypedEvent(KeyTypedEvent e)
    {
        _imGuiController.PressKey((char)e.KeyCode);
        return true;
    }

    private bool OnMouseScrollEvent(MouseScrolledEvent e)
    {
        var mouseScrollOffset = new Vector2(e.XOffset, e.YOffset);

        _imGuiController.UpdateMouseScroll(mouseScrollOffset);

        return true;
    }
}
