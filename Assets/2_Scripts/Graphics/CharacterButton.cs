using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace QuizGame.Graphics
{
    public class CharacterButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [Header("Enabled state")]
        [SerializeField] private GameObject enabledStateObject;
        [SerializeField] private Text valueText;
        [Header("Disabled state")]
        [SerializeField] private GameObject disabledStateObject;

        public UnityEvent onClick = new UnityEvent();
        private bool isInteractable;

        public char Value { get; private set; }

        public void Init(bool isInteractable)
        {
            this.isInteractable = isInteractable;

            button.onClick.AddListener(() => onClick?.Invoke());
        }

        public void UpdateView(char? value)
        {
            Value = value.GetValueOrDefault();
            name = $"{Value} button";

            button.interactable = isInteractable && value.HasValue;

            enabledStateObject.SetActive(value.HasValue);
            disabledStateObject.SetActive(!value.HasValue);

            valueText.text = value?.ToString().ToUpper();
        }
    }
}
