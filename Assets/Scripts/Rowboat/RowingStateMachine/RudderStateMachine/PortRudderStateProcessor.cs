using System;
using UnityEngine;

namespace IndieCade
{
    public class PortRudderStateProcessor : RowingStateProcessor<RudderState, RudderStateMachineTransition>
    {
        private RowboatPhysicsController _rowboatPhysicsController;

        public PortRudderStateProcessor(RowingStateMachineContext<RudderState, RudderStateMachineTransition> context, RowboatPlayerInputs rowboatPlayerInputs, RowboatPhysicsController rowboatPhysicsController)
            : base(context, rowboatPlayerInputs)
        {
            _rowboatPhysicsController = rowboatPhysicsController;
        }

        protected override void ProcessInternal()
        {
            if (_context.CurrentTransition == RudderStateMachineTransition.PORT_UP ||
                _context.CurrentTransition == RudderStateMachineTransition.STAR_DOWN)
            {
                SetCurrentState(RudderState.STRAIGHT);

                _rowboatPhysicsController.EndTurnRudder(true);
                _rowboatPhysicsController.EndTurnRudder(false);
            }
        }
    }
}
