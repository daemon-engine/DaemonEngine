namespace DaemonEngine.EventSystem.Events.Mouse;

public class MouseMovedEvent : EventBase
{
    public MouseMovedEvent(float mouseX, float mouseY)
    {
        MouseX = mouseX;
        MouseY = mouseY;
    }

    public float MouseX { get; set; }
    public float MouseY { get; set; }

    public override EventType Type => EventType.MouseMoved;

    public override EventCategory Category => EventCategory.EventCategoryMouse | EventCategory.EventCategoryInput;
}
