using System;
namespace IndieCade
{
    public class HoldInputStateProcessor : IStateProcessor
    {
        private InputStateMachineContext _context;

        public HoldInputStateProcessor(InputStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == InputStateMachineTransition.ON_KEY)
            {
                _context.CurrentState = InputState.UP;
            }
        }
    }
}
