using System;
using DebugConsole.Entities;
using DebugConsole.Presenters;
using DebugConsole.Presenters.PresenterInputs;
using DebugConsole.Repositories;
using DebugConsole.UseCases.UseCaseInputs;

namespace DebugConsole.UseCases
{
    public class ExecuteCommandUseCase : UseCase<CommandGenerator>
    {
        public ExecuteCommandUseCase(IRepository<CommandGenerator> entityRepository, IPresenterInputBoundary presenter) : base(entityRepository, presenter)
        {
        }

        public override void Execute(IUseCaseInput input)
        {
            if (!(input is ExecuteCommandUseCaseInput useCaseInput)) throw new ArgumentException();

            var commandGenerator = EntityRepository.GetItem(useCaseInput.EntityID);
            var command = commandGenerator.GetCommand(useCaseInput.CommandToken);
            
            Presenter.Execute(new ExecuteCommandPresenterInput(useCaseInput.EntityID, command, useCaseInput.Arguments));
        }
    }
}