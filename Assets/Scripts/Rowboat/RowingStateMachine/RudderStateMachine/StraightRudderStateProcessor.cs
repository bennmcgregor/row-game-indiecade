using System;
using UnityEngine;

namespace IndieCade
{
    public class StraightRudderStateProcessor : RowingStateProcessor<RudderState, RudderStateMachineTransition>
    {
        private RowboatMaps _rowboatMaps;
        private GlobalDirectionStateMachine _globalDirectionStateMachine;
        private RowboatPhysicsController _rowboatPhysicsController;

        public StraightRudderStateProcessor(RowingStateMachineContext<RudderState, RudderStateMachineTransition> context, RowboatPlayerInputs rowboatPlayerInputs, RowboatMaps rowboatMaps, GlobalDirectionStateMachine globalDirectionStateMachine, RowboatPhysicsController rowboatPhysicsController)
            : base(context, rowboatPlayerInputs)
        {
            _rowboatMaps = rowboatMaps;
            _globalDirectionStateMachine = globalDirectionStateMachine;
            _rowboatPhysicsController = rowboatPhysicsController;
        }

        protected override void ProcessInternal()
        {
            InputKey portKey = _rowboatMaps.GetInputKeyFromBoatAndGlobalDirection(BoatDirection.PORT, _globalDirectionStateMachine.CurrentState);
            InputKey starKey = _rowboatMaps.GetInputKeyFromBoatAndGlobalDirection(BoatDirection.STARBOARD, _globalDirectionStateMachine.CurrentState);

            if (_context.CurrentTransition == RudderStateMachineTransition.PORT_DOWN ||
                (_context.CurrentTransition == RudderStateMachineTransition.STAR_UP &&
                _rowboatPlayerInputs.InputStateMachines[portKey].CurrentState == InputState.HOLD))
            {
                SetCurrentState(RudderState.PORT);

                _rowboatPhysicsController.StartTurnRudder(false);
            }
            else if (_context.CurrentTransition == RudderStateMachineTransition.STAR_DOWN ||
                (_context.CurrentTransition == RudderStateMachineTransition.PORT_UP &&
                _rowboatPlayerInputs.InputStateMachines[starKey].CurrentState == InputState.HOLD))
            {
                SetCurrentState(RudderState.STAR);

                _rowboatPhysicsController.StartTurnRudder(true);
            }
        }
    }
}
