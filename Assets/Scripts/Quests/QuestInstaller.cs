using System;
using Zenject;
using UnityEngine;

namespace IndieCade
{
    public class QuestInstaller : MonoInstaller
    {
        [SerializeField] private DialogueYarnProjectScriptableObject _dialogueYarnProjectScriptableObject;

        public override void InstallBindings()
        {
            GameQuests gameQuests = new GameQuests(_dialogueYarnProjectScriptableObject);

            Container.BindInstance(gameQuests).AsSingle();
            Container.Bind<QuestRunner>().FromNewComponentOnNewGameObject().AsSingle();
            Container.Bind<QuestStateMachine>().FromNew().AsSingle();
        }
    }
}
