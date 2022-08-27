using System;
namespace IndieCade
{
    public class EntryGlobalDirectionStateProcessor : IStateProcessor
    {
        private GlobalDirectionStateMachineContext _context;

        public EntryGlobalDirectionStateProcessor(GlobalDirectionStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == GlobalDirectionStateMachineTransition.ENTRY)
            {
                _context.CurrentState = GlobalDirectionState.EAST;
            }
        }
    }
}
