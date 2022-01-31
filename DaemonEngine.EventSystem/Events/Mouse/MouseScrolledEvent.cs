namespace DaemonEngine.EventSystem.Events.Mouse;

public class MouseScrolledEvent : EventBase
{
    public MouseScrolledEvent(float xOffset, float yOffset)
    {
        XOffset = xOffset;
        YOffset = yOffset;
    }

    public float XOffset { get; set; }
    public float YOffset { get; set; }

    public override EventType Type => EventType.MouseScrolled;

    public override EventCategory Category => EventCategory.EventCategoryMouse | EventCategory.EventCategoryInput;
}
