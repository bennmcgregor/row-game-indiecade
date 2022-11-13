using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class GameStateListener : MonoBehaviour
    {
        protected SceneInitializationDataIndex _sceneInitializationDataIndex;
        protected QuestRunner _questRunner;

        private List<QuestState> _playedQuests;
        private List<string> _playedChallenges;

        [Inject]
        public void Initialize(SceneInitializationDataIndex initializationDataIndex, QuestRunner questRunner)
        {
            _sceneInitializationDataIndex = initializationDataIndex;
            _questRunner = questRunner;
        }

        protected virtual void Awake()
        {
            _questRunner.OnQuestUpdate += OnQuestUpdate;
            _questRunner.CurrentQuest.OnChallengeUpdated += OnChallengeUpdated;
            _questRunner.CurrentQuest.OnChallengeFailed += OnChallengeFailed;
            _questRunner.CurrentQuest.OnChallengeCompleted += OnChallengeCompleted;

            _playedQuests = new List<QuestState>();
            _playedChallenges = new List<string>();
            _playedQuests.Add(_questRunner.CurrentQuest.QuestState);
            _playedChallenges.Add(_questRunner.CurrentQuest.CurrentChallenge.StateName);
        }

        private void Start()
        {
            OnSceneEnter();
        }

        // TODO: add OnSceneExit handling

        private void OnSceneEnter()
        {
            InitializeScene(_sceneInitializationDataIndex.GetDataFromCurrentScene());
            SaveSceneOnSceneEnter(_sceneInitializationDataIndex.GetDataFromCurrentScene());
            InitializeQuest(_questRunner.CurrentQuest.QuestInitializationData);
            InitializeChallenge(_questRunner.CurrentQuest.CurrentChallenge);
        }

        private void OnQuestUpdate(Quest newQuest)
        {
            if (_playedQuests.Contains(newQuest.QuestState))
            {
                InitializeScene(_sceneInitializationDataIndex.GetDataFromCurrentScene());
            } else
            {
                _playedQuests.Add(newQuest.QuestState);
            }

            SaveSceneOnQuestUpdated(_sceneInitializationDataIndex.GetDataFromCurrentScene());
            InitializeQuest(newQuest.QuestInitializationData);
            InitializeChallenge(newQuest.CurrentChallenge);

            newQuest.OnChallengeUpdated += OnChallengeUpdated;
        }

        private void OnChallengeUpdated(ChallengeInitializationData newChallengeData)
        {
            if (_playedChallenges.Contains(newChallengeData.StateName))
            {
                InitializeScene(_sceneInitializationDataIndex.GetDataFromCurrentScene());
            }
            else
            {
                _playedChallenges.Add(newChallengeData.StateName);
            }

            SaveSceneOnChallengeUpdated(_sceneInitializationDataIndex.GetDataFromCurrentScene());
            InitializeChallenge(newChallengeData);

            _questRunner.CurrentQuest.OnChallengeUpdated += OnChallengeUpdated;
        }

        protected virtual void SaveSceneOnSceneEnter(SceneInitializationData sceneInitializationData) { }
        protected virtual void SaveSceneOnQuestUpdated(SceneInitializationData sceneInitializationData) { }
        protected virtual void SaveSceneOnChallengeUpdated(SceneInitializationData sceneInitializationData) { }
        protected virtual void InitializeScene(SceneInitializationData sceneInitializationData) { }
        protected virtual void InitializeQuest(QuestInitializationData questInitializationData) { }
        protected virtual void InitializeChallenge(ChallengeInitializationData challengeInitializationData) { }
        protected virtual void OnChallengeCompleted() { }
        protected virtual void OnChallengeFailed() { }
    }
}
