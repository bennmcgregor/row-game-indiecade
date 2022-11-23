using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class QuestUpdateEffector : MonoBehaviour
    {
        private QuestRunner _questRunner;

        [Inject]
        public void Initialize(QuestRunner questRunner)
        {
            _questRunner = questRunner;
        }

        public void FailChallenge()
        {
            _questRunner.CurrentQuest.RestartCurrentChallenge();
        }

        public void CompleteChallenge()
        {
            _questRunner.CurrentQuest.CompleteCurrentChallenge();
        }
    }
}