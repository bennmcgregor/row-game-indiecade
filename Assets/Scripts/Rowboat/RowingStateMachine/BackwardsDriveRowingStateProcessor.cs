using System;
using Zenject;

namespace IndieCade
{
    public class BackwardsDriveRowingStateProcessor : RowingStateProcessor
    {
        private RowboatPhysicsController _rowboatPhysics;
        private RowboatMaps _rowboatMaps;
        private GlobalDirectionStateMachine _globalDirectionStateMachine;
        // TODO(rudder): delete _hasSwitchedLane
        //private bool _hasSwitchedLane = false;

        public BackwardsDriveRowingStateProcessor(RowingStateMachineContext context, RowboatPlayerInputs rowboatPlayerInputs, RowboatPhysicsController rowboatPhysics, RowboatMaps rowboatMaps, GlobalDirectionStateMachine globalDirectionStateMachine)
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
                SetCurrentState(RowingState.BACKWARDS_RECOV);

                _rowboatPhysics.StopDrive();
                _rowboatPhysics.StartRecovery(false);

                //_hasSwitchedLane = false;
            }
            else if (_context.CurrentTransition == RowingStateMachineTransition.BOW_DOWN)
            {
                SetCurrentState(RowingState.STOP);

                _rowboatPhysics.StopDrive();
                _rowboatPhysics.StartStopBoat();

                //_hasSwitchedLane = false;
            }
            else if (_context.CurrentTransition == RowingStateMachineTransition.PORT_DOWN/* && !_hasSwitchedLane*/)
            {
                SwitchLane(false);
            }
            else if (_context.CurrentTransition == RowingStateMachineTransition.STAR_DOWN/* && !_hasSwitchedLane*/)
            {
                SwitchLane(true);
            }
        }

        public override void ProcessHold()
        {
            InputKey portKey = _rowboatMaps.GetInputKeyFromBoatAndGlobalDirection(BoatDirection.PORT, _globalDirectionStateMachine.CurrentState);
            InputKey starKey = _rowboatMaps.GetInputKeyFromBoatAndGlobalDirection(BoatDirection.STARBOARD, _globalDirectionStateMachine.CurrentState);

            if (_rowboatPlayerInputs.InputStateMachines[portKey].CurrentState == InputState.HOLD/* && !_hasSwitchedLane*/)
            {
                SwitchLane(false);
            }
            else if (_rowboatPlayerInputs.InputStateMachines[starKey].CurrentState == InputState.HOLD/* && !_hasSwitchedLane*/)
            {
                SwitchLane(true);
            }
        }

        private void SwitchLane(bool star)
        {
            //_hasSwitchedLane = true;
            _rowboatPhysics.StartSwitchLane(star);
        }
    }
}
