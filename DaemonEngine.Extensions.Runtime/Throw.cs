namespace DaemonEngine.Extensions.Runtime;

public static class Throw
{
    public static void IfNull(object? obj, string parameterName)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(nameof(obj));
        }
    }
}
