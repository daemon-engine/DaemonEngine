namespace DaemonEngine.EventSystem.Events.Window;

public class WindowFocusEvent : EventBase
{
    public WindowFocusEvent(bool focused)
    {
        Focused = focused;
    }

    public bool Focused { get; }

    public override EventType Type => EventType.WindowFocus;
    public override EventCategory Category => EventCategory.EventCategoryApplication;
}
