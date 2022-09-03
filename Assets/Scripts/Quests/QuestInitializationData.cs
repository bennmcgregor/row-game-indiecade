using System;
namespace IndieCade
{
    public class QuestInitializationData
    {
        private QuestState _stateName;

        public QuestState StateName => _stateName;

        public QuestInitializationData(QuestState questState)
        {
            _stateName = questState;
        }
    }
}
