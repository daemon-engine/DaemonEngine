namespace DaemonEngine.EventSystem.Events.Key;

public abstract class KeyEventBase : EventBase
{
    public KeyEventBase(int keyCode)
    {
        KeyCode = keyCode;
    }

    public int KeyCode { get; }

    public abstract override EventType Type { get; }

    public override EventCategory Category => EventCategory.EventCategoryKeyboard | EventCategory.EventCategoryInput;
}
