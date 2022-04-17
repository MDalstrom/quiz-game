using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading;
using Cysharp;
using Cysharp.Threading.Tasks;

namespace QuizGame.Graphics
{
    public class MessageContainer : MonoBehaviour
    {
        [SerializeField] private Text header;
        [SerializeField] private Text description;

        public async UniTask ShowMessage(string headerText, string descriptionText)
        {
            header.text = headerText;
            description.text = descriptionText;

            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            Destroy(gameObject);
        }
    }
}
