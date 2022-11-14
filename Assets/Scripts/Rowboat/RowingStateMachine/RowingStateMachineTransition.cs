using System;
namespace IndieCade
{
    public enum RowingStateMachineTransition
    {
        ENTRY,
        BOW_NONE,
        BOW_DOWN,
        BOW_HOLD,
        BOW_UP,
        STERN_NONE,
        STERN_DOWN,
        STERN_HOLD,
        STERN_UP,
        SHIFT_NONE,
        SHIFT_DOWN,
        SHIFT_HOLD,
        SHIFT_UP,
        FINISH_DRIVE,
        FINISH_SWITCH_LANE,
        FINISH_SPIN
    }
}
