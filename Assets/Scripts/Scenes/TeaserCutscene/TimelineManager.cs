using System;
using UnityEngine;
using UnityEngine.Playables;
using Zenject;

namespace IndieCade
{
    public class TimelineManager : MonoBehaviour
    {
        [SerializeField] private PlayableDirector _playableDirector;

        private QuestRunner _questRunner;

        [Inject]
        public void Initialize(QuestRunner questRunner, AudioManager audioManager)
        {
            _questRunner = questRunner;
        }

        private void Awake()
        {
            _playableDirector.stopped += CompleteChallenge;
        }

        private void CompleteChallenge(PlayableDirector _)
        {
            _questRunner.CurrentQuest.CompleteCurrentChallenge();
        }
    }
}
