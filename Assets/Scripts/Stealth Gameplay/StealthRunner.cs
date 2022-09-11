using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class StealthRunner : MonoBehaviour, IToggleable
    {
        public Action OnCaughtInSpotlight;
        public Action OnFailedStealthRun;

        [SerializeField] private int _catchesBeforeFailure = 3;

        private int _catchCount;
        private QuestRunner _questRunner;

        public int CatchCount => _catchCount;

        private void Awake()
        {
            _catchCount = _catchesBeforeFailure;

            OnFailedStealthRun += FailChallenge;
        }

        [Inject]
        public void Initialize(QuestRunner questRunner)
        {
            _questRunner = questRunner;
        }

        public void CaughtInSpotlight()
        {
            _catchCount--;
            OnCaughtInSpotlight?.Invoke();

            if (_catchCount == 0)
            {
                OnFailedStealthRun?.Invoke();
            }
        }

        private void FailChallenge()
        {
            // TODO: freeze rowboat inputs for a time duration (before failing the quest)
            _questRunner.CurrentQuest.FailCurrentChallenge();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
