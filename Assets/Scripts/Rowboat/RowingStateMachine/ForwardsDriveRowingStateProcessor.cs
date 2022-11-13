using System;
using Zenject;

namespace IndieCade
{
    public class ForwardsDriveRowingStateProcessor : RowingStateProcessor
    {
        private RowboatPhysicsController _rowboatPhysics;
        private RowboatMaps _rowboatMaps;
        private GlobalDirectionStateMachine _globalDirectionStateMachine;

        public ForwardsDriveRowingStateProcessor(RowingStateMachineContext context, RowboatPlayerInputs rowboatPlayerInputs, RowboatPhysicsController rowboatPhysics, RowboatMaps rowboatMaps, GlobalDirectionStateMachine globalDirectionStateMachine)
            : base(context, rowboatPlayerInputs)
        {
            _rowboatPhysics = rowboatPhysics;
            _rowboatMaps = rowboatMaps;
            _globalDirectionStateMachine = globalDirectionStateMachine;
        }

        protected override void ProcessInternal()
        {
            if (_context.CurrentTransition == RowingStateMachineTransition.FINISH_DRIVE)
            {
                SetCurrentState(RowingState.FORWARDS_RECOV);

                _rowboatPhysics.StopDrive();
                _rowboatPhysics.StartRecovery(true);
            }
            else if (_context.CurrentTransition == RowingStateMachineTransition.STERN_DOWN)
            {
                SetCurrentState(RowingState.STOP);

                _rowboatPhysics.StopDrive();
                _rowboatPhysics.StartStopBoat();
            }
            else if (_context.CurrentTransition == RowingStateMachineTransition.PORT_DOWN)
            {
                SwitchLane(false);
            }
            else if (_context.CurrentTransition == RowingStateMachineTransition.STAR_DOWN)
            {
                SwitchLane(true);
            }
        }

        public override void ProcessHold()
        {
            InputKey portKey = _rowboatMaps.GetInputKeyFromBoatAndGlobalDirection(BoatDirection.PORT, _globalDirectionStateMachine.CurrentState);
            InputKey starKey = _rowboatMaps.GetInputKeyFromBoatAndGlobalDirection(BoatDirection.STARBOARD, _globalDirectionStateMachine.CurrentState);

            if (_rowboatPlayerInputs.InputStateMachines[portKey].CurrentState == InputState.HOLD)
            {
                SwitchLane(false);
            }
            else if (_rowboatPlayerInputs.InputStateMachines[starKey].CurrentState == InputState.HOLD)
            {
                SwitchLane(true);
            }
        }

        private void SwitchLane(bool star)
        {
            _rowboatPhysics.StartSwitchLane(star);
        }
    }
}
