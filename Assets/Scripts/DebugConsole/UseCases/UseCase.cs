using DebugConsole.Entities;
using DebugConsole.Presenters;
using DebugConsole.Repositories;
using DebugConsole.UseCases.UseCaseInputs;

namespace DebugConsole.UseCases
{
    public abstract class UseCase<TEntity> : IUseCaseInputBoundary where TEntity : Entity
    {
        protected readonly IRepository<TEntity> EntityRepository;
        protected readonly IPresenterInputBoundary Presenter;

        protected UseCase(IRepository<TEntity> entityRepository, IPresenterInputBoundary presenter)
        {
            EntityRepository = entityRepository;
            Presenter = presenter;
        }
        public abstract void Execute(IUseCaseInput input);
    }
}