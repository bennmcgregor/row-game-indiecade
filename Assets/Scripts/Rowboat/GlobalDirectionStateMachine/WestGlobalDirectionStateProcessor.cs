using System;
namespace IndieCade
{
    public class WestGlobalDirectionStateProcessor : IStateProcessor
    {
        private GlobalDirectionStateMachineContext _context;

        public WestGlobalDirectionStateProcessor(GlobalDirectionStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == GlobalDirectionStateMachineTransition.SPIN)
            {
                _context.CurrentState = GlobalDirectionState.EAST;
            }
        }
    }
}
