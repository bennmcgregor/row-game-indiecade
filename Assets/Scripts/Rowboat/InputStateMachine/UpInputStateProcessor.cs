using System;
namespace IndieCade
{
    public class UpInputStateProcessor : IStateProcessor
    {
        private InputStateMachineContext _context;

        public UpInputStateProcessor(InputStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == InputStateMachineTransition.ON_PRESS)
            {
                _context.CurrentState = InputState.DOWN;
            }
            else if (_context.CurrentTransition == InputStateMachineTransition.ON_STATE_PROCESSED)
            {
                _context.CurrentState = InputState.NONE;
            }
        }
    }
}
