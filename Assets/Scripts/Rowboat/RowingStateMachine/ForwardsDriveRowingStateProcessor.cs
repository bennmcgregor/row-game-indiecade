using System;
using Zenject;

namespace IndieCade
{
    public class ForwardsDriveRowingStateProcessor : IStateProcessor
    {
        private RowingStateMachineContext _context;
        private RowboatPhysicsController _rowboatPhysics;

        public ForwardsDriveRowingStateProcessor(RowingStateMachineContext context)
        {
            _context = context;
        }

        [Inject]
        public void Initialize(RowboatPhysicsController rowboatPhysics)
        {
            _rowboatPhysics = rowboatPhysics;
        }

        public void Process()
        {
            if (_context.CurrentTransition == RowingStateMachineTransition.FINISH)
            {
                _context.CurrentState = RowingState.FORWARDS_RECOV;

                _rowboatPhysics.StopDrive();
                _rowboatPhysics.StartRecovery(true);
            }
            else if (_context.CurrentTransition == RowingStateMachineTransition.STERN_DOWN)
            {
                // TODO: uncomment this when implementing STOP
                //_context.CurrentState = RowingState.STOP;
            }
            else if (_context.CurrentTransition == RowingStateMachineTransition.PORT_DOWN ||
                _context.CurrentTransition == RowingStateMachineTransition.PORT_HOLD)
            {
                // TODO: uncomment this when implementing SWITCH_LANE_PORT
                //_context.CurrentState = RowingState.SWITCH_LANE_PORT;
            }
            else if (_context.CurrentTransition == RowingStateMachineTransition.STAR_DOWN ||
                _context.CurrentTransition == RowingStateMachineTransition.STAR_HOLD)
            {
                // TODO: uncomment this when implementing SWITCH_LANE_STAR
                //_context.CurrentState = RowingState.SWITCH_LANE_STAR;
            }
        }
    }
}
