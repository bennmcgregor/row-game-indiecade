using System;
namespace IndieCade
{
    public class StateData<TStateEnum>
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
