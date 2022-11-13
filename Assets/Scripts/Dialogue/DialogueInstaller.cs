using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class DialogueInstaller : MonoInstaller
    {
        [SerializeField] private DialogueControl _dialogueControl;
        [SerializeField] private SpeechBubbleProvider _speechBubbleProvider;

        public override void InstallBindings()
        {
            Container.BindInstance(_dialogueControl).AsSingle();
            Container.BindInstance(_speechBubbleProvider).AsSingle();
        }
    }
}
