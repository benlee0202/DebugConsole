using System;
using System.Collections.Generic;
using DebugConsole.Entities;
using DebugConsole.Interfaces;
using DebugConsole.Presenters;
using DebugConsole.Repositories;
using DebugConsole.UseCases;

namespace DebugConsole.Utilities
{
    public class UseCaseFactory<TEntity> : IFactory<Type, IUseCaseInputBoundary> where TEntity : Entity
    {
        private readonly Dictionary<Type, IUseCaseInputBoundary> _inputBoundaryMap =
            new Dictionary<Type, IUseCaseInputBoundary>();
        private readonly IRepository<TEntity> _repository;
        private readonly IPresenterInputBoundary _presenterInputBoundary;

        public UseCaseFactory(IRepository<TEntity> repository, IPresenterInputBoundary presenterInputBoundary)
        {
            _repository = repository;
            _presenterInputBoundary = presenterInputBoundary;
        }

        public IUseCaseInputBoundary GetProduct(Type type)
        {
            if (_inputBoundaryMap.TryGetValue(type, out var inputBoundary)) return inputBoundary;
            if (!(Activator.CreateInstance(type, _repository, _presenterInputBoundary) is IUseCaseInputBoundary
                newInputBoundary)) throw new ArgumentException();

            _inputBoundaryMap.Add(type, newInputBoundary);
            return newInputBoundary;
        }
    }
}
