using DebugConsole.Interfaces;

namespace DebugConsole.Presenters.PresenterInputs
{
    public readonly struct CreateDebugConsolePresenterInput : IPresenterInput
    {
        public CreateDebugConsolePresenterInput(int viewID, IDisplay display)
        {
            ViewID = viewID;
            Display = display;
        }

        public int ViewID { get; }
        public IDisplay Display { get; }
    }
}