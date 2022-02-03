using DaemonEngine.GraphicsLibraryFramework.DllImport;

namespace DaemonEngine.Windows.Inputs;

public enum MouseButton : int
{
    BUTTON1 = GlfwConstants.GLFW_MOUSE_BUTTON_1,       // 0
    BUTTON2 = GlfwConstants.GLFW_MOUSE_BUTTON_2,       // 1
    BUTTON3 = GlfwConstants.GLFW_MOUSE_BUTTON_3,       // 2
    BUTTON4 = GlfwConstants.GLFW_MOUSE_BUTTON_4,       // 3
    BUTTON5 = GlfwConstants.GLFW_MOUSE_BUTTON_5,       // 4
    BUTTON6 = GlfwConstants.GLFW_MOUSE_BUTTON_6,       // 5
    BUTTON7 = GlfwConstants.GLFW_MOUSE_BUTTON_7,       // 6
    BUTTON8 = GlfwConstants.GLFW_MOUSE_BUTTON_8,       // 7
    BUTTON_LAST = GlfwConstants.GLFW_MOUSE_BUTTON_LAST,    // GLFW_MOUSE_BUTTON_8;
    BUTTON_LEFT = GlfwConstants.GLFW_MOUSE_BUTTON_LEFT,    // GLFW_MOUSE_BUTTON_1;
    BUTTON_RIGHT = GlfwConstants.GLFW_MOUSE_BUTTON_RIGHT,   // GLFW_MOUSE_BUTTON_2;
    BUTTON_MIDDLE = GlfwConstants.GLFW_MOUSE_BUTTON_MIDDLE,  // GLFW_MOUSE_BUTTON_3;
}
