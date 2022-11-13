using System;
using System.Collections;
using UnityEngine;
using Yarn.Unity;
using TMPro;
using Zenject;

namespace IndieCade
{
    public class SimpleSpeechBubbleLineView : DialogueViewBase
    {
        [SerializeField] private float _appearanceTime = 0.5f;
        [SerializeField] private float _disappearanceTime = 0.5f;
        [SerializeField] private bool _waitForInput;

        private Coroutine _currentAnimation;
        private Action _advanceHandler = null;
        private SpeechBubbleProvider _speechBubbleProvider;
        private SpeechBubble _currentSpeechBubble;

        private float Scale
        {
            set => _currentSpeechBubble.RectTransform.localScale = new Vector3(value, value, value);
        }

        [Inject]
        private void Initialize(SpeechBubbleProvider speechBubbleProvider)
        {
            _speechBubbleProvider = speechBubbleProvider;
        }

        private void Start()
        {

            foreach (var speechBubble in _speechBubbleProvider.All)
            {
                _currentSpeechBubble = speechBubble;
                Scale = 0;
            }
        }

        public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            if (gameObject.activeInHierarchy == false)
            {
                onDialogueLineFinished();
                return;
            }

            Debug.Log($"{this.name} running line {dialogueLine.TextID}");

            _currentSpeechBubble = _speechBubbleProvider.GetSpeechBubble(dialogueLine.CharacterName);
            Scale = 0;
            _currentSpeechBubble.Text.text = dialogueLine.TextWithoutCharacterName.Text;

            _advanceHandler = requestInterrupt;

            _currentAnimation = this.Tween(
                0f, 1f,
                _appearanceTime,
                (from, to, t) => Scale = Mathf.Lerp(from, to, t),
                () => {
                    Debug.Log($"{this.name} finished presenting {dialogueLine.TextID}");
                    _currentAnimation = null;

                    if (_waitForInput)
                    {
                        _advanceHandler = requestInterrupt;
                    }
                    else
                    {
                        _advanceHandler = null;
                        onDialogueLineFinished();
                    }
                }
            );
        }

        public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
        {
            if (gameObject.activeInHierarchy == false)
            {
                onDialogueLineFinished();
                return;
            }

            _advanceHandler = null;

            Debug.Log($"{this.name} was interrupted while presenting {dialogueLine.TextID}");

            if (_currentAnimation != null)
            {
                StopCoroutine(_currentAnimation);
                _currentAnimation = null;
            }

            Scale = 1f;

            onDialogueLineFinished();
        }

        public override void DismissLine(Action onDismissalComplete)
        {
            if (gameObject.activeInHierarchy == false)
            {
                onDismissalComplete();
                return;
            }

            Debug.Log($"{this.name} dismissing line");

            if (_currentAnimation != null)
            {
                StopCoroutine(_currentAnimation);
                _currentAnimation = null;
            }

            _advanceHandler = () =>
            {
                if (_currentAnimation != null)
                {
                    StopCoroutine(_currentAnimation);
                    _currentAnimation = null;
                }
                _advanceHandler = null;
                onDismissalComplete();
                Scale = 0f;
            };

            _currentAnimation = this.Tween(
                1f, 0f,
                _disappearanceTime,
                (from, to, t) => Scale = Mathf.Lerp(from, to, t),
                () => {
                    _advanceHandler = null;
                    Debug.Log($"{this.name} finished dismissing line");
                    _currentAnimation = null;
                    onDismissalComplete();
                });
        }

        public override void UserRequestedViewAdvancement()
        {
            _advanceHandler?.Invoke();
        }
    }
}
