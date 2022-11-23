using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class QuestRunner : MonoBehaviour
    {
        public Action OnQuestUpdate;

        private QuestStateMachine _questStateMachine;

        public QuestData CurrentQuest => (QuestData) _questStateMachine.CurrentData;

        [Inject]
        public void Initialize(QuestStateMachine questStateMachine)
        {
            _questStateMachine = questStateMachine;
        }

        private void Start()
        {
            _questStateMachine.OnStateUpdated += UpdateQuestState;
        }

        public void CompleteCurrentQuest()
        {
            _questStateMachine.Transition(QuestStateMachineTransition.NEXT_QUEST);
        }

        private void UpdateQuestState()
        {
            OnQuestUpdate?.Invoke();
        }
    }
}
