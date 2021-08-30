using DebugConsole.Interfaces;

namespace DebugConsole.UseCases.UseCaseInputs
{
    public readonly struct CreateCommandGeneratorUseCaseInput : IUseCaseInput
    {
        public CreateCommandGeneratorUseCaseInput(int entityID, IFactory<string, IDebugCommand> commandFactory, IDisplay display)
        {
            EntityID = entityID;
            CommandFactory = commandFactory;
            Display = display;
        }

        public int EntityID { get; }
        public IFactory<string, IDebugCommand> CommandFactory { get; }
        public IDisplay Display { get; }
    }
}