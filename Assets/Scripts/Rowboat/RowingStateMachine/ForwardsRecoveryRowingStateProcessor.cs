using System;
using Zenject;

namespace IndieCade
{
    public class ForwardsRecoveryRowingStateProcessor : IStateProcessor
    {
        private RowingStateMachineContext _context;
        private RowboatPhysicsController _rowboatPhysics;

        public ForwardsRecoveryRowingStateProcessor(RowingStateMachineContext context)
        {
            _context = context;
        }

        [Inject]
        public void Initialize(RowboatPhysicsController rowboatPhysics)
        {
            _rowboatPhysics = rowboatPhysics;
        }

        public void Process()
        {
            if (_context.CurrentTransition == RowingStateMachineTransition.BOW_DOWN)
            {
                _context.CurrentState = RowingState.FORWARDS_DRIVE;
                // TODO: begin drive physics (coroutine can trigger state transition??)
            }
            else if (_context.CurrentTransition == RowingStateMachineTransition.STERN_DOWN)
            {
                // TODO: uncomment this when implementing STOP
                //_context.CurrentState = RowingState.STOP;
            }
        }
    }
}
