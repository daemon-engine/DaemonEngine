namespace DaemonEngine.EventSystem.Events.Mouse;

public class MouseButtonPressedEvent : MouseButtonEvent
{
    public MouseButtonPressedEvent(int mouseButton) 
        : base(mouseButton)
    {
    }

    public override EventType Type => EventType.MouseButtonPressed;
}
