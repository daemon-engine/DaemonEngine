namespace DaemonEngine.EventSystem.Events.Key;
public class KeyReleasedEvent : KeyEventBase
{
    public KeyReleasedEvent(int keyCode) 
        : base(keyCode)
    {
    }

    public override EventType Type => EventType.KeyReleased;
}
