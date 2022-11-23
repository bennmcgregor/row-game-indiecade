using System;
namespace IndieCade
{
    public class QuestData<TChallengeState> : StateData<QuestState>, IQuestData
        where TChallengeState : Enum
    {
        public Action OnChallengeUpdated;
        public Action OnChallengeFailed;
        public Action OnChallengeCompleted;

        private ChallengeStateMachine<TChallengeState> _challengeStateMachine;
        private QuestInitializationData _questInitializationData;

        public ChallengeInitializationData<TChallengeState> CurrentChallenge => (ChallengeInitializationData<TChallengeState>) _challengeStateMachine.CurrentData;
        public QuestInitializationData QuestInitializationData => _questInitializationData;

        public QuestData(QuestState questState, ChallengeStateMachine<TChallengeState> challengeStateMachine, QuestInitializationData questInitializationData)
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
