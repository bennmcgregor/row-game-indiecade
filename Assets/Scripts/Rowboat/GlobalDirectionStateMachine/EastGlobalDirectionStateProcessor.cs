using System;
namespace IndieCade
{
    public class EastGlobalDirectionStateProcessor : IStateProcessor
    {
        private GlobalDirectionStateMachineContext _context;

        public EastGlobalDirectionStateProcessor(GlobalDirectionStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == GlobalDirectionStateMachineTransition.CLOCKWISE)
            {
                _context.CurrentState = GlobalDirectionState.SOUTH;
            }
            else
            {
                _context.CurrentState = GlobalDirectionState.NORTH;
            }
        }
    }
}
