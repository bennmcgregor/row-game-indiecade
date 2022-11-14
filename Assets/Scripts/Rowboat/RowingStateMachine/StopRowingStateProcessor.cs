using System;
using Zenject;

namespace IndieCade
{
    public class StopRowingStateProcessor : RowingStateProcessor<RowingState, RowingStateMachineTransition>
    {
        private RowboatPhysicsController _rowboatPhysics;
        private RowboatMaps _rowboatMaps;
        private GlobalDirectionStateMachine _globalDirectionStateMachine;
        private RowboatAnimator _rowboatAnimator;

        public StopRowingStateProcessor(RowingStateMachineContext<RowingState, RowingStateMachineTransition> context, RowboatPlayerInputs rowboatPlayerInputs, RowboatPhysicsController rowboatPhysics, RowboatMaps rowboatMaps, GlobalDirectionStateMachine globalDirectionStateMachine, RowboatAnimator rowboatAnimator)
            : base(context, rowboatPlayerInputs)
        {
            _rowboatPhysics = rowboatPhysics;
            _rowboatMaps = rowboatMaps;
            _globalDirectionStateMachine = globalDirectionStateMachine;
            _rowboatAnimator = rowboatAnimator;
        }

        protected override void ProcessInternal()
        {
            if (TransitionToForwardsRecovery())
            {
                SetCurrentState(RowingState.FORWARDS_RECOV);

                _rowboatPhysics.EndStopBoat();
                _rowboatPhysics.StartRecovery(true);
            }
            else if (TransitionToBackwardsRecovery())
            {
                SetCurrentState(RowingState.BACKWARDS_RECOV);

                _rowboatPhysics.EndStopBoat();
                _rowboatPhysics.StartRecovery(false);
            }
            else if (_context.CurrentTransition == RowingStateMachineTransition.SHIFT_DOWN &&
                _rowboatPhysics.CanTransitionFromStopped())
            {
                SetCurrentState(RowingState.SPIN);

                _rowboatPhysics.EndStopBoat();
                _rowboatAnimator.StartSpin();
            }
        }

        private bool TransitionToForwardsRecovery()
        {
            // get inputKey based on boat direction
            InputKey sternKey = _rowboatMaps.GetInputKeyFromBoatAndGlobalDirection(BoatDirection.STERN, _globalDirectionStateMachine.CurrentState);

            return (_context.CurrentTransition == RowingStateMachineTransition.BOW_DOWN &&
                _rowboatPlayerInputs.InputStateMachines[sternKey].CurrentState == InputState.NONE &&
                _rowboatPhysics.CanTransitionFromStopped())
                ||
                (_context.CurrentTransition == RowingStateMachineTransition.STERN_UP &&
                (_context.PreviousState == RowingState.FORWARDS_RECOV || _context.PreviousState == RowingState.FORWARDS_DRIVE) &&
                !_rowboatPhysics.CanTransitionFromStopped());
        }

        private bool TransitionToBackwardsRecovery()
        {
            InputKey bowKey = _rowboatMaps.GetInputKeyFromBoatAndGlobalDirection(BoatDirection.BOW, _globalDirectionStateMachine.CurrentState);

            return (_context.CurrentTransition == RowingStateMachineTransition.STERN_DOWN &&
                _rowboatPlayerInputs.InputStateMachines[bowKey].CurrentState == InputState.NONE &&
                _rowboatPhysics.CanTransitionFromStopped())
                ||
                (_context.CurrentTransition == RowingStateMachineTransition.BOW_UP &&
                (_context.PreviousState == RowingState.BACKWARDS_RECOV || _context.PreviousState == RowingState.BACKWARDS_DRIVE) &&
                !_rowboatPhysics.CanTransitionFromStopped());
        }
    }
}
