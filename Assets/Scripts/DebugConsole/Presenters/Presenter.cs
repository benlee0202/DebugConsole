using DebugConsole.Presenters.PresenterInputs;

namespace DebugConsole.Presenters
{
    public abstract class Presenter : IPresenterInputBoundary
    {
        public abstract void Execute<TInput>(TInput input) where TInput : IPresenterInput;
    }
}