using EventHandler.Interfaces;

namespace UIComponents
{
    public interface IEventBusUser
    {
        void SetEventBus(IEventBus eventBus);
    }
}
