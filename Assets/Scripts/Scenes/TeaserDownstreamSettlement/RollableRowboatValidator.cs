using System;
namespace IndieCade
{
    public class RollableRowboatValidator : InteractionValidator
    {
        private bool _hasRolledRowboat = false;

        public void RollRowboat()
        {
            _hasRolledRowboat = true;
        }

        public override bool Validate()
        {
            return _hasRolledRowboat;
        }
    }
}
