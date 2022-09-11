using System;
namespace IndieCade
{
    public class NoneInputStateProcessor : IStateProcessor
    {
        private InputStateMachineContext _context;

        public NoneInputStateProcessor(InputStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == InputStateMachineTransition.ON_PRESS)
            {
                _context.CurrentState = InputState.DOWN;
            }
        }
    }
}
