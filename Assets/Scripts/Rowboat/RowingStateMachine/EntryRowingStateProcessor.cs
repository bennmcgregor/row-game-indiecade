using System;

namespace IndieCade
{
    public class EntryRowingStateProcessor : RowingStateProcessor
    {
        private RowboatPhysicsController _rowboatPhysics;

        public EntryRowingStateProcessor(RowingStateMachineContext context, RowboatPlayerInputs rowboatPlayerInputs, RowboatPhysicsController rowboatPhysics) : base(context, rowboatPlayerInputs)
        {
            _rowboatPhysics = rowboatPhysics;
            _notifyInputStateMachine = false;
        }

        protected override void ProcessInternal()
        {
            if (_context.CurrentTransition == RowingStateMachineTransition.ENTRY)
            {
                SetCurrentState(RowingState.FORWARDS_RECOV);

                _rowboatPhysics.StartRecovery(true);
            }
        }
    }
}
