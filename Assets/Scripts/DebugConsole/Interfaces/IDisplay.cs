namespace DebugConsole.Interfaces
{
    public interface IDisplay
    {
        int ViewID { get; }
        void Display(IDebugCommand debugCommand);
    }
}