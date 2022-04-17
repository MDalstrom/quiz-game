using UniRx;
using UnityEngine;
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

        public readonly ReactiveProperty<bool> isInteractable = new ReactiveProperty<bool>();
        public readonly ReactiveProperty<bool> state = new ReactiveProperty<bool>();
        public readonly ReactiveProperty<char> character = new ReactiveProperty<char>();
        public readonly Subject<CharacterButton> onClick = new Subject<CharacterButton>();

        public void Init()
        {
            character.Subscribe(_ => UpdateView()).AddTo(this);
            state.Subscribe(_ => UpdateView()).AddTo(this);
            isInteractable.Subscribe(_ => UpdateView()).AddTo(this);

            button.onClick.AsObservable().Subscribe(_ => onClick.OnNext(this)).AddTo(this);
        }

        private void UpdateView()
        {
            name = $"{character.Value} button";
            valueText.text = character.Value.ToString().ToUpper();

            button.interactable = state.Value && isInteractable.Value;

            enabledStateObject.SetActive(state.Value);
            disabledStateObject.SetActive(!state.Value);
        }
    }
}
