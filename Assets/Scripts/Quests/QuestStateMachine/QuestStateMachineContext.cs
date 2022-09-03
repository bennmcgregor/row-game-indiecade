using System;
namespace IndieCade
{
    public class QuestStateMachineContext
    {
        public Action OnQuestUpdated;

        private QuestState _currentState;

        public QuestStateMachineContext(QuestState initialState)
        {
            _currentState = initialState;
        }

        public QuestState CurrentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                _currentState = value;
                OnQuestUpdated?.Invoke();
            }
        }
        public QuestStateMachineTransition CurrentTransition;
    }
}
