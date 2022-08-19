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
            if (_context.CurrentTransition == InputStateMachineTransition.ON_KEY)
            {
                _context.CurrentState = InputState.DOWN;
            }
            else
            {
                _context.CurrentState = InputState.NONE;
            }
        }
    }
}
