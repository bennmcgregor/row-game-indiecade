using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class GameInitializationStateListener : MonoBehaviour
    {
        protected SceneInitializationDataIndex _sceneInitializationDataIndex;
        protected QuestRunner _questRunner;

        [Inject]
        public void Initialize(SceneInitializationDataIndex initializationDataIndex, QuestRunner questRunner)
        {
            _sceneInitializationDataIndex = initializationDataIndex;
            _questRunner = questRunner;
        }

        private void Awake()
        {
            _questRunner.OnQuestUpdate += OnQuestUpdate;
        }

        private void Start()
        {
            OnSceneUpdate();
        }

        private void OnSceneUpdate()
        {
            InitializeScene(_sceneInitializationDataIndex.GetDataFromCurrentScene());
        }

        private void OnQuestUpdate(Quest newQuest)
        {
            newQuest.OnChallengeUpdate += OnChallengeUpdate;
            InitializeQuest(newQuest.QuestInitializationData);
        }

        private void OnChallengeUpdate(ChallengeInitializationData newChallengeData)
        {
            InitializeChallenge(newChallengeData);
        }

        protected virtual void InitializeScene(SceneInitializationData sceneInitializationData) { }
        protected virtual void InitializeQuest(QuestInitializationData questInitializationData) { }
        protected virtual void InitializeChallenge(ChallengeInitializationData challengeInitializationData) { }
    }
}
