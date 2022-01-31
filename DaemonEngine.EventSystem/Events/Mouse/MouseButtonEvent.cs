namespace DaemonEngine.EventSystem.Events.Mouse;

public abstract class MouseButtonEvent : EventBase
{
    public MouseButtonEvent(int mouseButton)
    {
        MouseButton = mouseButton;
    }

    public int MouseButton { get; }

    public abstract override EventType Type { get; }

    public override EventCategory Category => EventCategory.EventCategoryMouse | EventCategory.EventCategoryInput | EventCategory.EventCategoryMouseButton;
}
