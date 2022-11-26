using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class RowingStateProcessorFactory<TStateEnum, TTransitionEnum> : StateProcessorFactory<TStateEnum, TTransitionEnum, RowingStateMachineContext<TStateEnum, TTransitionEnum>, RowingStateProcessor<TStateEnum, TTransitionEnum>>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
    {
        private RowboatPlayerInputs _rowboatPlayerInputs;
        private bool _notifyInputStateMachine = true;

        public RowingStateProcessorFactory(TStateEnum stateName, RowingStateMachineContext<TStateEnum, TTransitionEnum> context, RowboatPlayerInputs rowboatPlayerInputs)
            : base(stateName, context)
        {
            _rowboatPlayerInputs = rowboatPlayerInputs;
        }

        public void RegisterTransition(Predicate<TTransitionEnum> pred, TStateEnum newState)
        {
            RegisterTransition(pred, newState, null);
        }

        public void RegisterTransition(TTransitionEnum transition, TStateEnum newState, Action transitionLogic)
        {
            RegisterTransition(
                delegate (TTransitionEnum transitionEnum)
                {
                    return transition.Equals(transitionEnum);
                },
                newState,
                transitionLogic
            );
        }

        public void SetNotifyInputStateMachine(bool value)
        {
            _notifyInputStateMachine = value;
        }

        public override RowingStateProcessor<TStateEnum, TTransitionEnum> Make()
        {
            return new RowingStateProcessor<TStateEnum, TTransitionEnum>(
                _context,
                _stateName,
                _transitionFunctionList,
                _transitionNewStateList,
                _newStateActionMap,
                _rowboatPlayerInputs,
                _notifyInputStateMachine
            );
        }
    }
}
