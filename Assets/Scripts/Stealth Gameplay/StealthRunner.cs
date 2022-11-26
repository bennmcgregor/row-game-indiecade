using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class StealthRunner : MonoBehaviour, IActivateable
    {
        public Action OnLivesUpdated;
        public Action OnFailedStealthRun;

        [SerializeField] private int _catchesBeforeFailure = 3;

        private int _catchCount;
        private QuestRunner _questRunner;

        public int CatchCount => _catchCount;

        private void Awake()
        {
            OnFailedStealthRun += FailChallenge;
        }

        private void Start()
        {
            UpdateCatchCount(_catchesBeforeFailure);
        }

        [Inject]
        public void Initialize(QuestRunner questRunner)
        {
            _questRunner = questRunner;
        }

        public void CaughtInSpotlight()
        {
            UpdateCatchCount(_catchCount - 1);

            if (_catchCount == 0)
            {
                OnFailedStealthRun?.Invoke();
            }
        }

        private void FailChallenge()
        {
            // TODO: freeze rowboat inputs for a time duration (before failing the quest)
            _questRunner.CurrentQuest.RestartCurrentChallenge();
            UpdateCatchCount(_catchesBeforeFailure);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void ResetCatchCount()
        {
            UpdateCatchCount(_catchesBeforeFailure);
        }

        private void UpdateCatchCount(int catchCount)
        {
            _catchCount = catchCount;
            OnLivesUpdated?.Invoke();
        }
    }
}
