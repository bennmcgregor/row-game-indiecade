using System;
namespace IndieCade
{
    public class QuestData : StateData<QuestState>
    {
        public Action OnChallengeUpdated;
        public Action OnChallengeFailed;
        public Action OnChallengeCompleted;

        private ChallengeStateMachine<TeaserChallenges> _challengeStateMachine;
        private QuestInitializationData _questInitializationData;

        public ChallengeInitializationData CurrentChallenge => (ChallengeInitializationData) _challengeStateMachine.CurrentData;
        public QuestInitializationData QuestInitializationData => _questInitializationData;

        public QuestData(QuestState questState, ChallengeStateMachine<TeaserChallenges> challengeStateMachine, QuestInitializationData questInitializationData)
            : base(questState)
        {
            _challengeStateMachine = challengeStateMachine;
            _questInitializationData = questInitializationData;

            _challengeStateMachine.OnStateUpdated += UpdateChallengeState;
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
            UnityEngine.Debug.Log($"Updated Challenge to {_challengeStateMachine.CurrentData.StateName}");
            OnChallengeUpdated?.Invoke();
        }
    }
}
