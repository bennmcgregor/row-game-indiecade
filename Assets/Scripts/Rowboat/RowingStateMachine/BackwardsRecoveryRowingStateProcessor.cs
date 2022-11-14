using System;
using Zenject;

namespace IndieCade
{
    public class BackwardsRecoveryRowingStateProcessor : RowingStateProcessor<RowingState, RowingStateMachineTransition>
    {
        private RowboatPhysicsController _rowboatPhysics;
        // TODO(rudder): remove commented parts
        //private RowboatMaps _rowboatMaps;
        //private GlobalDirectionStateMachine _globalDirectionStateMachine;

        public BackwardsRecoveryRowingStateProcessor(RowingStateMachineContext<RowingState, RowingStateMachineTransition> context, RowboatPlayerInputs rowboatPlayerInputs, RowboatPhysicsController rowboatPhysics, RowboatMaps rowboatMaps, GlobalDirectionStateMachine globalDirectionStateMachine)
            : base(context, rowboatPlayerInputs)
        {
            _rowboatPhysics = rowboatPhysics;
            //_rowboatMaps = rowboatMaps;
            //_globalDirectionStateMachine = globalDirectionStateMachine;
        }

        protected override void ProcessInternal()
        {
            if (_context.CurrentTransition == RowingStateMachineTransition.STERN_DOWN)
            {
                SetCurrentState(RowingState.BACKWARDS_DRIVE);
                _rowboatPhysics.StopRecovery();
                _rowboatPhysics.StartDrive(false);

            }
            else if (_context.CurrentTransition == RowingStateMachineTransition.BOW_DOWN)
            {
                SetCurrentState(RowingState.STOP);

                _rowboatPhysics.StopRecovery();
                _rowboatPhysics.StartStopBoat();
            }
            //else if (_context.CurrentTransition == RowingStateMachineTransition.PORT_DOWN)
            //{
            //    SwitchLane(false);
            //}
            //else if (_context.CurrentTransition == RowingStateMachineTransition.STAR_DOWN)
            //{
            //    SwitchLane(true);
            //}
        }

        //public override void ProcessHold()
        //{
        //    InputKey portKey = _rowboatMaps.GetInputKeyFromBoatAndGlobalDirection(BoatDirection.PORT, _globalDirectionStateMachine.CurrentState);
        //    InputKey starKey = _rowboatMaps.GetInputKeyFromBoatAndGlobalDirection(BoatDirection.STARBOARD, _globalDirectionStateMachine.CurrentState);

        //    if (_rowboatPlayerInputs.InputStateMachines[portKey].CurrentState == InputState.HOLD)
        //    {
        //        SwitchLane(false);
        //    }
        //    else if (_rowboatPlayerInputs.InputStateMachines[starKey].CurrentState == InputState.HOLD)
        //    {
        //        SwitchLane(true);
        //    }
        //}

        //private void SwitchLane(bool star)
        //{
        //    _rowboatPhysics.StartSwitchLane(star);
        //}
    }
}
