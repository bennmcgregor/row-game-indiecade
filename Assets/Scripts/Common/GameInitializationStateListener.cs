using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class GameInitializationStateListener : MonoBehaviour
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

        private void Awake()
        {
            _questRunner.OnQuestUpdate += OnQuestUpdate;
            _questRunner.CurrentQuest.OnChallengeUpdate += OnChallengeUpdate;

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
            SaveScene(_sceneInitializationDataIndex.GetDataFromCurrentScene());
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

            SaveScene(_sceneInitializationDataIndex.GetDataFromCurrentScene());
            InitializeQuest(newQuest.QuestInitializationData);
            InitializeChallenge(newQuest.CurrentChallenge);

            newQuest.OnChallengeUpdate += OnChallengeUpdate;
        }

        private void OnChallengeUpdate(ChallengeInitializationData newChallengeData)
        {
            if (_playedChallenges.Contains(newChallengeData.StateName))
            {
                InitializeScene(_sceneInitializationDataIndex.GetDataFromCurrentScene());
            }
            else
            {
                _playedChallenges.Add(newChallengeData.StateName);
            }

            SaveScene(_sceneInitializationDataIndex.GetDataFromCurrentScene());
            InitializeChallenge(newChallengeData);

            _questRunner.CurrentQuest.OnChallengeUpdate += OnChallengeUpdate;
        }

        protected virtual void SaveScene(SceneInitializationData sceneInitializationData) { }
        protected virtual void InitializeScene(SceneInitializationData sceneInitializationData) { }
        protected virtual void InitializeQuest(QuestInitializationData questInitializationData) { }
        protected virtual void InitializeChallenge(ChallengeInitializationData challengeInitializationData) { }
    }
}
