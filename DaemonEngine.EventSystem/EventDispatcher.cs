namespace DaemonEngine.EventSystem;

public class EventDispatcher
{
    private IEvent _event;

    public EventDispatcher(IEvent @event)
    {
        _event = @event;
    }

    public bool Dispatch<TEvent>(Func<TEvent, bool> func)
        where TEvent : class, IEvent
    {
        var e = _event as TEvent;
        if(_event.Type == e?.Type)
        {
            _event.Handled |= func((TEvent)_event);
            return true;
        }

        return false;
    }
}
