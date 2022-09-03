using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class QuestRunner : MonoBehaviour
    {
        public Action<Quest> OnQuestUpdate;

        private QuestStateMachine _questStateMachine;

        public Quest CurrentQuest => _questStateMachine.CurrentQuest;

        [Inject]
        public void Initialize(QuestStateMachine questStateMachine)
        {
            _questStateMachine = questStateMachine;
        }

        private void Start()
        {
            _questStateMachine.Context.OnQuestUpdated += UpdateQuestAndChallengeState;
            _questStateMachine.Transition(QuestStateMachineTransition.ENTRY);
        }

        public void CompleteCurrentQuest()
        {
            _questStateMachine.Transition(QuestStateMachineTransition.NEXT_QUEST);
        }

        private void UpdateQuestAndChallengeState()
        {
            UpdateQuestState();
            _questStateMachine.CurrentQuest.UpdateChallengeState();
        }

        private void UpdateQuestState()
        {
            OnQuestUpdate?.Invoke(CurrentQuest);
        }
    }
}
