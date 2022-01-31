namespace DaemonEngine.EventSystem.Events.Window;

public class WindowCloseEvent : EventBase
{
    public override EventType Type => EventType.WindowClose;
    public override EventCategory Category => EventCategory.EventCategoryApplication;
}
