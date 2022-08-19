using System;
namespace IndieCade
{
    public class SouthGlobalDirectionStateProcessor : IStateProcessor
    {
        private GlobalDirectionStateMachineContext _context;

        public SouthGlobalDirectionStateProcessor(GlobalDirectionStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == GlobalDirectionStateMachineTransition.CLOCKWISE)
            {
                _context.CurrentState = GlobalDirectionState.WEST;
            }
            else
            {
                _context.CurrentState = GlobalDirectionState.EAST;
            }
        }
    }
}
