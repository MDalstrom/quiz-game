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

        public event UnityAction onClick;

        public void Init()
        {
            button.onClick.AddListener(onClick);
        }

        public void UpdateView(char? value)
        {
            enabledStateObject.SetActive(value.HasValue);
            disabledStateObject.SetActive(!value.HasValue);

            valueText.text = value?.ToString().ToUpper();
        }
    }
}
