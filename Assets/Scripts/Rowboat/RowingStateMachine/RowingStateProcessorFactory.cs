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
        private List<Predicate<TTransitionEnum>> _holdTransitionFunctionList;
        private List<TStateEnum> _holdTransitionNewStateList;
        private Dictionary<TStateEnum, Action> _holdNewStateActionMap;

        public RowingStateProcessorFactory(TStateEnum stateName, RowingStateMachineContext<TStateEnum, TTransitionEnum> context, RowboatPlayerInputs rowboatPlayerInputs)
            : base(stateName, context)
        {
            _rowboatPlayerInputs = rowboatPlayerInputs;

            _holdTransitionFunctionList = new List<Predicate<TTransitionEnum>>();
            _holdTransitionNewStateList = new List<TStateEnum>();
            _holdNewStateActionMap = new Dictionary<TStateEnum, Action>();
        }

        public void RegisterTransition(Predicate<TTransitionEnum> pred, TStateEnum newState)
        {
            RegisterTransition(pred, newState, null);
        }

        public void RegisterTransition(TTransitionEnum transition, TStateEnum newState)
        {
            RegisterTransition(
                delegate(TTransitionEnum transitionEnum)
                {
                    return transition.Equals(transitionEnum);
                },
                newState
            );
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

        public void RegisterHoldTransition(Predicate<TTransitionEnum> pred, TStateEnum newState, Action transitionLogic)
        {
            _holdTransitionFunctionList.Add(pred);
            _holdTransitionNewStateList.Add(newState);

            if (transitionLogic != null)
            {
                _holdNewStateActionMap.Add(newState, transitionLogic);
            }
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
                _holdTransitionFunctionList,
                _holdTransitionNewStateList,
                _holdNewStateActionMap,
                _rowboatPlayerInputs,
                _notifyInputStateMachine
            );
        }
    }
}
