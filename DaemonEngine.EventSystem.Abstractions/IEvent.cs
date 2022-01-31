namespace DaemonEngine.EventSystem;

public enum EventType
{
	None = 0,
	WindowClose, WindowResize, WindowFocus, WindowLostFocus, WindowMoved,
	KeyPressed, KeyReleased, KeyTyped,
	MouseButtonPressed, MouseButtonReleased, MouseMoved, MouseScrolled
};

public enum EventCategory
{
	None = 0,
	EventCategoryApplication = (1 << 0),
	EventCategoryInput = (1 << 1),
	EventCategoryKeyboard = (1 << 2),
	EventCategoryMouse = (1 << 3),
	EventCategoryMouseButton = (1 << 4)
};

public interface IEvent
{
    bool Handled { get; set; }
	EventType Type { get; }
	EventCategory Category { get; }
}
