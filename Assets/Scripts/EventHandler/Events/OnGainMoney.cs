using EventHandler.Interfaces;

namespace EventHandler.Events
{
    public readonly struct OnGainMoney : IEventBase
    {
        public int Number { get; }

        public OnGainMoney(int number)
        {
            Number = number;
        }
    }
}
