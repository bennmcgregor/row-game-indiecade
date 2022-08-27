using System;
namespace IndieCade
{
    public class EntryInputStateProcessor : IStateProcessor
    {
        private InputStateMachineContext _context;

        public EntryInputStateProcessor(InputStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == InputStateMachineTransition.ENTRY)
            {
                _context.CurrentState = InputState.NONE;
            }
        }
    }
}
