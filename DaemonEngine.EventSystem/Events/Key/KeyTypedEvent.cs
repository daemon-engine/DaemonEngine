namespace DaemonEngine.EventSystem.Events.Key;

public class KeyTypedEvent : KeyEventBase
{
    public KeyTypedEvent(int keyCode) 
        : base(keyCode)
    {
    }

    public override EventType Type => EventType.KeyTyped;
}
