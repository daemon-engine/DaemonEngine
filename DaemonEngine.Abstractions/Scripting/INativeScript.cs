namespace DaemonEngine.Scripting;

public interface INativeScript
{
    void Start();
    void Stop();
    void Update(float deltaTime);
}
