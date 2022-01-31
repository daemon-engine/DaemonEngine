namespace DaemonEngine.EventSystem;

public abstract class EventBase : IEvent
{
    public bool Handled { get; set; } = false;
    public abstract EventType Type { get; }
    public abstract EventCategory Category { get; }
}
