using System.Collections.Generic;
using DebugConsole.Entities;

namespace DebugConsole.Repositories
{
    public class CommandGeneratorRepository : IRepository<CommandGenerator>
    {
        private readonly Dictionary<int, CommandGenerator> _commandGeneratorMap =
            new Dictionary<int, CommandGenerator>();

        public void Add(CommandGenerator item)
        {
            _commandGeneratorMap.Add(item.EntityID, item);
        }

        public void Remove(CommandGenerator item)
        {
            throw new System.NotImplementedException();
        }

        public CommandGenerator GetItem(int serialNumber)
        {
            if (_commandGeneratorMap.TryGetValue(serialNumber, out var commandGenerator)) return commandGenerator;

            throw new KeyNotFoundException();
        }

        public void Edit(CommandGenerator item)
        {
            throw new System.NotImplementedException();
        }

        public List<CommandGenerator> GetAllItems()
        {
            throw new System.NotImplementedException();
        }
    }
}