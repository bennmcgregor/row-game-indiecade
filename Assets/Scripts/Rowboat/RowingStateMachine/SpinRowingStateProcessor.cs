using System;
using Zenject;

namespace IndieCade
{
    public class SpinRowingStateProcessor : RowingStateProcessor<RowingState, RowingStateMachineTransition>
    {
        private RowboatAnimator _rowboatAnimator;
        private GlobalDirectionStateMachine _globalDirectionStateMachine;

        public SpinRowingStateProcessor(RowingStateMachineContext<RowingState, RowingStateMachineTransition> context, RowboatPlayerInputs rowboatPlayerInputs, RowboatAnimator rowboatAnimator, GlobalDirectionStateMachine globalDirectionStateMachine)
            : base(context, rowboatPlayerInputs)
        {
            _rowboatAnimator = rowboatAnimator;
            _globalDirectionStateMachine = globalDirectionStateMachine;
        }

        protected override void ProcessInternal()
        {
            if (_context.CurrentTransition == RowingStateMachineTransition.FINISH_SPIN)
            {
                SetCurrentState(RowingState.STOP);

                _rowboatAnimator.StopSpin();
                _globalDirectionStateMachine.Transition(GlobalDirectionStateMachineTransition.SPIN);
            }
        }
    }
}
