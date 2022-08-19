using System;
namespace IndieCade
{
    public class NorthGlobalDirectionStateProcessor : IStateProcessor
    {
        private GlobalDirectionStateMachineContext _context;

        public NorthGlobalDirectionStateProcessor(GlobalDirectionStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == GlobalDirectionStateMachineTransition.CLOCKWISE)
            {
                _context.CurrentState = GlobalDirectionState.EAST;
            }
            else
            {
                _context.CurrentState = GlobalDirectionState.WEST;
            }
        }
    }
}
