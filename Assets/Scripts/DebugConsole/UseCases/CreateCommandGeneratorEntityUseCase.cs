using System;
using DebugConsole.Entities;
using DebugConsole.Presenters;
using DebugConsole.Presenters.PresenterInputs;
using DebugConsole.Repositories;
using DebugConsole.UseCases.UseCaseInputs;

namespace DebugConsole.UseCases
{
    public class CreateCommandGeneratorEntityUseCase : UseCase<CommandGenerator>
    {
        public CreateCommandGeneratorEntityUseCase(IRepository<CommandGenerator> entityRepository, IPresenterInputBoundary presenter) : base(entityRepository, presenter)
        {
        }

        public override void Execute(IUseCaseInput input)
        {
            if (!(input is CreateCommandGeneratorUseCaseInput useCaseInput)) throw new ArgumentException();

            var commandGenerator = new CommandGenerator(useCaseInput.EntityID, useCaseInput.CommandFactory);
            EntityRepository.Add(commandGenerator);
            Presenter.Execute(new CreateDebugConsolePresenterInput(useCaseInput.EntityID, useCaseInput.Display));
        }
    }
}