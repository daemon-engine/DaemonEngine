namespace DaemonEngine.EventSystem.Events.Window;

public class WindowResizeEvent : EventBase
{
    public WindowResizeEvent(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public int Width { get; }
    public int Height { get; }

    public override EventType Type => EventType.WindowResize;

    public override EventCategory Category => EventCategory.EventCategoryApplication;
}
