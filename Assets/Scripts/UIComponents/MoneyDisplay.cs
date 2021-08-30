using EventHandler.Events;
using EventHandler.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UIComponents
{
    public class MoneyDisplay : MonoBehaviour, IEventBusUser
    {
        private IEventBus _eventBus;
        private Text _text;
        private int _number;
        
        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            _eventBus.Subscribe<OnGainMoney>(Display);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<OnGainMoney>(Display);
        }

        private void Display(OnGainMoney eventMessage)
        {
            _number += eventMessage.Number;
            _text.text = $"Money: {_number.ToString()}";
        }

        public void SetEventBus(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
    }
}
