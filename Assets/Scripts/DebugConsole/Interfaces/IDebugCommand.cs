namespace DebugConsole.Interfaces
{
    public interface IDebugCommand
    { 
        string CommandToken { get; }
        string CommandLog { get; }
        void Execute(string[] args);
    }
}