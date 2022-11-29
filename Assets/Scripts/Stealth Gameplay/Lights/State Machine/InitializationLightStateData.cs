using System;
namespace IndieCade
{
    public class InitializationLightStateData
    {
        private string _id;
        private ShapeLightStateAttribute _shape;

        public string Id => _id;
        public ShapeLightStateAttribute Shape => _shape;

        public InitializationLightStateData(ShapeLightStateAttribute shape, string id)
        {
            _shape = shape;
            _id = id;
        }
    }
}
