using System;

namespace EventHandler.Interfaces
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(Action<TEvent> action) where TEvent : IEventBase;
        void Subscribe(Type type, Action<IEventBase> action);
        void Unsubscribe<TEvent>(Action<TEvent> action) where TEvent : IEventBase;
        void Unsubscribe(Type type, Action<IEventBase> action);
        void Publish(IEventBase eventBase);
    }
}