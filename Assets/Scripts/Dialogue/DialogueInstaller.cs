using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class DialogueInstaller : MonoInstaller
    {
        [SerializeField] private DialogueControl _dialogueControl;

        public override void InstallBindings()
        {
            Container.BindInstance(_dialogueControl).AsSingle();
        }
    }
}
