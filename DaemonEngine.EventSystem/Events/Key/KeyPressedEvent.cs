namespace DaemonEngine.EventSystem.Events.Key;

public class KeyPressedEvent : KeyEventBase
{
    public KeyPressedEvent(int keyCode, int repeatCount) 
        : base(keyCode)
    {
        RepeatCount = repeatCount;
    }

    public int RepeatCount { get; }

    public override EventType Type => EventType.KeyPressed;
}
