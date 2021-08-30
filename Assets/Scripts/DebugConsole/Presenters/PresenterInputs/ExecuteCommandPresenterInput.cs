using DebugConsole.Interfaces;

namespace DebugConsole.Presenters.PresenterInputs
{
    public readonly struct ExecuteCommandPresenterInput : IPresenterInput
    {
        public ExecuteCommandPresenterInput(int viewID, IDebugCommand debugCommand, string[] arguments)
        {
            ViewID = viewID;
            DebugCommand = debugCommand;
            Arguments = arguments;
        }

        public int ViewID { get; }
        public IDebugCommand DebugCommand { get; }
        public string[] Arguments { get; }
    }
}