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

        private List<Predicate<TTransitionEnum>> _holdTransitionFunctionList;
        private List<TStateEnum> _holdTransitionNewStateList;
        private Dictionary<TStateEnum, Action> _holdNewStateActionMap;

        public RowingStateProcessor(RowingStateMachineContext<TStateEnum, TTransitionEnum> context, TStateEnum stateName, List<Predicate<TTransitionEnum>> transitionFunctionList, List<TStateEnum> transitionStateList, Dictionary<TStateEnum, Action> newStateActionMap, List<Predicate<TTransitionEnum>> holdTransitionFunctionList, List<TStateEnum> holdTransitionNewStateList, Dictionary<TStateEnum, Action> holdNewStateActionMap, RowboatPlayerInputs rowboatPlayerInputs, bool notifyInputStateMachine = true)
            : base(context, stateName, transitionFunctionList, transitionStateList, newStateActionMap)
        {
            _rowboatPlayerInputs = rowboatPlayerInputs;
            _notifyInputStateMachine = notifyInputStateMachine;

            _holdTransitionFunctionList = holdTransitionFunctionList;
            _holdTransitionNewStateList = holdTransitionNewStateList;
            _holdNewStateActionMap = holdNewStateActionMap;
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

        public void ProcessHold()
        {
            UnityEngine.Debug.Log($"Called ProcessHold {_context.CurrentTransition}");
            for (int i = 0; i < _holdTransitionFunctionList.Count; i++)
            {
                if (_holdTransitionFunctionList[i](_context.CurrentTransition))
                {
                    TStateEnum newState = _holdTransitionNewStateList[i];
                    SetCurrentState(newState);

                    if (_holdNewStateActionMap.ContainsKey(newState))
                    {
                        _holdNewStateActionMap[newState]?.Invoke();
                    }

                    break;
                }
            }
        }
    }
}
