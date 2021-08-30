using System;
using EventHandler.Events;
using EventHandler.Interfaces;

namespace DebugConsole.Commands
{
    public class GainMoneyDebugCommand : DebugCommandBase
    {
        public GainMoneyDebugCommand(IEventBus eventBus) : base(eventBus)
        {
        }

        public override string CommandToken => "GainMoney";
        public override string CommandLog { get; protected set; }

        public override void Execute(string[] args)
        {
            try
            {
                if (int.TryParse(args[0], out var number))
                {
                    CommandLog = $"Gain Money: {number}";
                    EventBus.Publish(new OnGainMoney(number));
                }
                else
                {
                    CommandLog = "It is not a valid number";
                }
            }
            catch (IndexOutOfRangeException)
            {
                CommandLog = "GainMoney Number shouldn't be null";
            }
        }
    }
}
