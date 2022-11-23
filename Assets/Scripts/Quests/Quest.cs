using System;
namespace IndieCade
{
    public class Quest : StateData<QuestState>
    {
        public Action<ChallengeInitializationData> OnChallengeUpdated;
        public Action OnChallengeFailed;
        public Action OnChallengeCompleted;

        private ChallengeStateMachine _challengeStateMachine;
        private QuestInitializationData _questInitializationData;

        public ChallengeInitializationData CurrentChallenge => _challengeStateMachine.CurrentChallenge;
        public QuestInitializationData QuestInitializationData => _questInitializationData;

        public Quest(QuestState questState, ChallengeStateMachine challengeStateMachine, QuestInitializationData questInitializationData)
            : base(questState)
        {
            _challengeStateMachine = challengeStateMachine;
            _questInitializationData = questInitializationData;

            _challengeStateMachine.Context.OnChallengeUpdated += UpdateChallengeState;
        }

        public void CompleteCurrentChallenge()
        {
            UnityEngine.Debug.Log("CompleteCurrentChallenge");
            OnChallengeCompleted?.Invoke();
            _challengeStateMachine.Transition(ChallengeStateMachineTransition.COMPLETED);
        }

        public void FailCurrentChallenge()
        {
            OnChallengeFailed?.Invoke();
            _challengeStateMachine.Transition(ChallengeStateMachineTransition.FAILED);
        }

        public void UpdateChallengeState()
        {
            UnityEngine.Debug.Log($"Updated Challenge to {CurrentChallenge.StateName}");
            OnChallengeUpdated?.Invoke(CurrentChallenge);
        }
    }
}
