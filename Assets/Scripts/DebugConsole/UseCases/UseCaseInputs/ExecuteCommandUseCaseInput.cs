namespace DebugConsole.UseCases.UseCaseInputs
{
    public readonly struct ExecuteCommandUseCaseInput : IUseCaseInput
    {
        public ExecuteCommandUseCaseInput(int entityID, string commandToken, string[] arguments)
        {
            EntityID = entityID;
            CommandToken = commandToken;
            Arguments = arguments;
        }

        public int EntityID { get; }
        public string CommandToken { get; }
        public string[] Arguments { get; }
    }
}