using DebugConsole.Interfaces;
using EventHandler.Events;
using EventHandler.Interfaces;
using UIComponents;
using UnityEngine;
using UnityEngine.UI;

namespace DebugConsole.Views
{
    public class DebugConsoleDisplayWithScrollView : MonoBehaviour, IDisplay, IEventBusUser
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private InputField inputField;
        [SerializeField] private Text historyLog;
        private IEventBus _eventBus;
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
        
        public int ViewID => GetInstanceID();

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
            historyLog.text += $"{debugCommand.CommandLog}\n";
            scrollRect.normalizedPosition = new Vector2(0, 0);
        }

        public void SubmitCommand()
        {
            _eventBus.Publish(new OnInputCommand(inputField.text));
        }

        public void SetEventBus(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
    }
}