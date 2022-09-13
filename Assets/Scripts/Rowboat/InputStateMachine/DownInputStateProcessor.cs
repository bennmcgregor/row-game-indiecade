using System;
namespace IndieCade
{
    public class DownInputStateProcessor : IStateProcessor
    {
        private InputStateMachineContext _context;

        public DownInputStateProcessor(InputStateMachineContext context)
        {
            _context = context;
        }

        public void Process()
        {
            if (_context.CurrentTransition == InputStateMachineTransition.ON_RELEASE)
            {
                _context.CurrentState = InputState.UP;
            } else if (_context.CurrentTransition == InputStateMachineTransition.ON_STATE_PROCESSED)
            {
                _context.CurrentState = InputState.HOLD;
            }
        }
    }
}
