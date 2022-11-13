using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class SpeechBubble : MonoBehaviour
    {
        [SerializeField] private string _characterName;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private TextMeshProUGUI _text;

        public string CharacterName => _characterName;
        public RectTransform RectTransform => _rectTransform;
        public TextMeshProUGUI Text => _text;

        private SpeechBubbleProvider _speechBubbleProvider;

        [Inject]
        public void Initialize(SpeechBubbleProvider speechBubbleProvider)
        {
            _speechBubbleProvider = speechBubbleProvider;
        }

        private void Awake()
        {
            _speechBubbleProvider.Register(this);
        }

        // TODO: move resize functions to here
    }
}
