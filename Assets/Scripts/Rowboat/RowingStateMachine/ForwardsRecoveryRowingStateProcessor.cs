using System;
using Zenject;

namespace IndieCade
{
    public class ForwardsRecoveryRowingStateProcessor : RowingStateProcessor
    {
        private RowboatPhysicsController _rowboatPhysics;

        public ForwardsRecoveryRowingStateProcessor(RowingStateMachineContext context, RowboatPlayerInputs rowboatPlayerInputs, RowboatPhysicsController rowboatPhysics) : base(context, rowboatPlayerInputs)
        {
            _rowboatPhysics = rowboatPhysics;
        }

        protected override void ProcessInternal()
        {
            if (_context.CurrentTransition == RowingStateMachineTransition.BOW_DOWN)
            {
                SetCurrentState(RowingState.FORWARDS_DRIVE);
                _rowboatPhysics.StopRecovery();
                _rowboatPhysics.StartDrive(true);

            }
            else if (_context.CurrentTransition == RowingStateMachineTransition.STERN_DOWN)
            {
                SetCurrentState(RowingState.STOP);

                _rowboatPhysics.StopRecovery();
                _rowboatPhysics.StartStopBoat();
            }
        }
    }
}
