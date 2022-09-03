using System;
namespace IndieCade
{
    public class Quest
    {
        public Action<ChallengeInitializationData> OnChallengeUpdate;

        private QuestState _questState;
        private ChallengeStateMachine _challengeStateMachine;
        private QuestInitializationData _questInitializationData;

        public QuestState QuestState => _questState;
        public ChallengeInitializationData CurrentChallenge => _challengeStateMachine.CurrentChallenge;
        public QuestInitializationData QuestInitializationData => _questInitializationData;

        public Quest(QuestState questState, ChallengeStateMachine challengeStateMachine, QuestInitializationData questInitializationData)
        {
            _questState = questState;
            _challengeStateMachine = challengeStateMachine;
            _questInitializationData = questInitializationData;

            _challengeStateMachine.Context.OnChallengeUpdated += UpdateChallengeState;
        }

        public void CompleteCurrentChallenge()
        {
            _challengeStateMachine.Transition(ChallengeStateMachineTransition.COMPLETED);
        }

        public void FailCurrentChallenge()
        {
            _challengeStateMachine.Transition(ChallengeStateMachineTransition.FAILED);
        }

        public void UpdateChallengeState()
        {
            OnChallengeUpdate?.Invoke(CurrentChallenge);
        }
    }
}
