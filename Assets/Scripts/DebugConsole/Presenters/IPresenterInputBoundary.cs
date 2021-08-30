using DebugConsole.Presenters.PresenterInputs;

namespace DebugConsole.Presenters
{
    public interface IPresenterInputBoundary
    {
        void Execute<T>(T input) where T : IPresenterInput;
    }
}