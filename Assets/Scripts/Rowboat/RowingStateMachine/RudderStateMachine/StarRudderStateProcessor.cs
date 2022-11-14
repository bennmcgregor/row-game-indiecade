using System;
using UnityEngine;

namespace IndieCade
{
    public class StarRudderStateProcessor : RowingStateProcessor<RudderState, RudderStateMachineTransition>
    {
        private RowboatPhysicsController _rowboatPhysicsController;

        public StarRudderStateProcessor(RowingStateMachineContext<RudderState, RudderStateMachineTransition> context, RowboatPlayerInputs rowboatPlayerInputs, RowboatPhysicsController rowboatPhysicsController)
            : base(context, rowboatPlayerInputs)
        {
            _rowboatPhysicsController = rowboatPhysicsController;
        }

        protected override void ProcessInternal()
        {
            if (_context.CurrentTransition == RudderStateMachineTransition.STAR_UP ||
                _context.CurrentTransition == RudderStateMachineTransition.PORT_DOWN)
            {
                SetCurrentState(RudderState.STRAIGHT);

                _rowboatPhysicsController.EndTurnRudder(true);
                _rowboatPhysicsController.EndTurnRudder(false);
            }
        }
    }
}
