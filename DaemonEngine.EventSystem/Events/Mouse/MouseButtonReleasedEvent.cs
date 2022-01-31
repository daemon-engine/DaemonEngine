namespace DaemonEngine.EventSystem.Events.Mouse;
public class MouseButtonReleasedEvent : MouseButtonEvent
{
    public MouseButtonReleasedEvent(int mouseButton) 
        : base(mouseButton)
    {
    }

    public override EventType Type => EventType.MouseButtonReleased;
}
