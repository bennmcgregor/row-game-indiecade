using System;
using System.Collections.Generic;

namespace IndieCade
{
    public class RowingStateProcessor<TStateEnum, TTransitionEnum> : StateProcessor<TStateEnum, TTransitionEnum, RowingStateMachineContext<TStateEnum, TTransitionEnum>>
        where TStateEnum : Enum
        where TTransitionEnum : Enum
    {
        private RowboatPlayerInputs _rowboatPlayerInputs;
        private bool _notifyInputStateMachine;

        public RowingStateProcessor(RowingStateMachineContext<TStateEnum, TTransitionEnum> context, TStateEnum stateName, List<Predicate<TTransitionEnum>> transitionFunctionList, List<TStateEnum> transitionStateList, Dictionary<TStateEnum, Action> newStateActionMap, RowboatPlayerInputs rowboatPlayerInputs, bool notifyInputStateMachine = true)
            : base(context, stateName, transitionFunctionList, transitionStateList, newStateActionMap)
        {
            _rowboatPlayerInputs = rowboatPlayerInputs;
            _notifyInputStateMachine = notifyInputStateMachine;
        }

        protected override void PostProcess()
        {
            if (_notifyInputStateMachine)
            {
                foreach (var stateMachine in _rowboatPlayerInputs.InputStateMachines.Values)
                {
                    stateMachine.Transition(InputStateMachineTransition.ON_STATE_PROCESSED);
                }
            }
        }

        // TODO(sm): add a way to run processhold logic to factory
        public virtual void ProcessHold() {}
    }
}
