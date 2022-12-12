using System;
namespace IndieCade
{
    public class StateDataBase {}

    public class StateData<TStateEnum> : StateDataBase
        where TStateEnum : Enum
    {
        private TStateEnum _stateName;

        public TStateEnum StateName => _stateName;

        public StateData(TStateEnum stateName)
        {
            _stateName = stateName;
        }
    }
}
