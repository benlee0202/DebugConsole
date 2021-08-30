using System.Collections.Generic;
using DebugConsole.Interfaces;

namespace DebugConsole.Repositories
{
    public class DisplayRepository : IRepository<IDisplay>
    {
        private readonly Dictionary<int, IDisplay> _displayMap = new Dictionary<int, IDisplay>();
        
        public void Add(IDisplay item)
        {
            _displayMap.Add(item.ViewID, item);
        }

        public void Remove(IDisplay item)
        {
            throw new System.NotImplementedException();
        }

        public IDisplay GetItem(int serialNumber)
        {
            return _displayMap[serialNumber];
        }

        public void Edit(IDisplay item)
        {
            throw new System.NotImplementedException();
        }

        public List<IDisplay> GetAllItems()
        {
            throw new System.NotImplementedException();
        }
    }
}