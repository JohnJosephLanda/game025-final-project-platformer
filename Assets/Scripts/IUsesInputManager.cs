public interface IUsesInputManager
{
    void SetInputManagerDependency(IInputManager InputManager);
    void OnInteraction();
}