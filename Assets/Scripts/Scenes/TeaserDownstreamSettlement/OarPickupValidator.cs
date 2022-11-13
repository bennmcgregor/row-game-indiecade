using System;

namespace IndieCade
{
    public class OarPickupValidator : InteractionValidator
    {
        private const int _kOarsToPickUp = 2;

        private int _oarsPickedUp = 0;

        public void PickupOar()
        {
            _oarsPickedUp++;
        }

        public override bool Validate()
        {
            return _oarsPickedUp == _kOarsToPickUp;
        }
    }
}
