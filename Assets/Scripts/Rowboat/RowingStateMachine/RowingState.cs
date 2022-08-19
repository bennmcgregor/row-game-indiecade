using System;
namespace IndieCade
{
    public enum RowingState
    {
        SPIN_CW,
        SPIN_CCW,
        STOP,
        FORWARDS_RECOV,
        FORWARDS_DRIVE,
        BACKWARDS_RECOV,
        BACKWARDS_DRIVE,
        SWITCH_LANE_PORT,
        SWITCH_LANE_STAR
    }
}
