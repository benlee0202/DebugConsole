using DebugConsole.Interfaces;
using EventHandler.Events;
using EventHandler.Interfaces;
using UIComponents;
using UnityEngine;
using UnityEngine.UI;

namespace DebugConsole.Views
{
    public class DebugConsoleWithOneLine : MonoBehaviour, IDisplay, IEventBusUser
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Text commandLog;
        private IEventBus _eventBus;
        public int ViewID => GetInstanceID();
        
        private CanvasGroup _canvasGroup;
        private bool _isDisplay;

        private bool IsDisplay
        {
            get => _isDisplay;
            set
            {
                if (value)
                {
                    _canvasGroup.alpha = 1.0f;
                    _canvasGroup.interactable = true;
                    inputField.text = string.Empty;
                    commandLog.text = string.Empty;
                    inputField.ActivateInputField();
                }
                else
                {
                    _canvasGroup.alpha = 0.0f;
                    _canvasGroup.interactable = false;
                }

                _isDisplay = value;
            }
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                IsDisplay = !IsDisplay;
            }
        }

        public void Display(IDebugCommand debugCommand)
        {
            commandLog.text = debugCommand.CommandLog;
        }

        public void SetEventBus(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        
        public void SubmitCommand()
        {
            _eventBus.Publish(new OnInputCommand(inputField.text));
            inputField.text = string.Empty;
            inputField.ActivateInputField();
        }
    }
}
