using DebugConsole.UseCases.UseCaseInputs;

namespace DebugConsole.UseCases
{
    public interface IUseCaseInputBoundary
    {
        void Execute(IUseCaseInput input);
    }
}