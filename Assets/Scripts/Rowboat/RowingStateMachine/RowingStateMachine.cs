using System;
using System.Collections;
using System.Collections.Generic;

namespace IndieCade
{
    public class RowingStateMachine
    {
        private RowingStateMachineContext _context;
        private Dictionary<RowingState, RowingStateProcessor> _stateProcessors;

        // current state
        public RowingState CurrentState => _context.CurrentState;
        public Dictionary<RowingState, RowingStateProcessor> StateProcessors => _stateProcessors;

        public RowingStateMachine(RowboatPhysicsController rowboatPhysicsController, RowboatPlayerInputs rowboatPlayerInputs, RowboatMaps rowboatMaps, GlobalDirectionStateMachine globalDirectionStateMachine, RowboatAnimator rowboatAnimator)
        {
            _context = new RowingStateMachineContext(RowingState.ENTRY);

            _stateProcessors = new Dictionary<RowingState, RowingStateProcessor>
            {
                { RowingState.ENTRY, new EntryRowingStateProcessor(_context, rowboatPlayerInputs, rowboatPhysicsController) },
                { RowingState.FORWARDS_RECOV, new ForwardsRecoveryRowingStateProcessor(_context, rowboatPlayerInputs, rowboatPhysicsController) },
                { RowingState.FORWARDS_DRIVE, new ForwardsDriveRowingStateProcessor(_context, rowboatPlayerInputs, rowboatPhysicsController, rowboatMaps, globalDirectionStateMachine) },
                { RowingState.BACKWARDS_RECOV, new BackwardsRecoveryRowingStateProcessor(_context, rowboatPlayerInputs, rowboatPhysicsController) },
                { RowingState.BACKWARDS_DRIVE, new BackwardsDriveRowingStateProcessor(_context, rowboatPlayerInputs, rowboatPhysicsController, rowboatMaps, globalDirectionStateMachine) },
                { RowingState.STOP, new StopRowingStateProcessor(_context, rowboatPlayerInputs, rowboatPhysicsController, rowboatMaps, globalDirectionStateMachine, rowboatAnimator) },
                { RowingState.SPIN, new SpinRowingStateProcessor(_context, rowboatPlayerInputs, rowboatAnimator, globalDirectionStateMachine) }
            };

            foreach (var processor in _stateProcessors.Values)
            {
                processor.OnStateUpdated += ProcessHold;
            }

            rowboatPhysicsController.OnDriveFinished += () => Transition(RowingStateMachineTransition.FINISH_DRIVE);
            rowboatPhysicsController.OnSwitchLaneFinished += () => Transition(RowingStateMachineTransition.FINISH_SWITCH_LANE);

            rowboatAnimator.OnSpinFinished += () => Transition(RowingStateMachineTransition.FINISH_SPIN);
        }

        public void Transition(RowingStateMachineTransition transition)
        {
            _context.CurrentTransition = transition;
            ProcessState();
        }

        private void ProcessState()
        {
            _stateProcessors[_context.CurrentState].Process();
        }

        private void ProcessHold()
        {
            _stateProcessors[_context.CurrentState].ProcessHold();
        }
    }
}
