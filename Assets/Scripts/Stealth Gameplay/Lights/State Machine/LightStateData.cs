using System;
namespace IndieCade
{
    public class LightStateData : StateData<LightState>
    {
        private string _id;
        private bool _on;
        private ShapeLightStateAttribute _shape;
        private CollisionLightStateAttribute _collisionData;
        private MotionTypeLightStateAttribute _motionType;
        private ColorLightStateAttribute _color;
        private BrightnessLightStateAttribute _brightness;
        // TODO: find a way to register/play different sound effects based on state transitions

        public string Id => _id;
        public bool On => _on;
        public ShapeLightStateAttribute Shape => _shape;
        public CollisionLightStateAttribute CollisionData => _collisionData;
        public MotionTypeLightStateAttribute MotionType => _motionType;
        public ColorLightStateAttribute Color => _color;
        public BrightnessLightStateAttribute Brightness => _brightness;

        public LightStateData(LightState stateName) : base(stateName) {}

        public void SetId(string id)
        {
            _id = id;
        }

        public void RegisterOff()
        {
            _on = false;
        }

        public void RegisterOn(CollisionLightStateAttribute collisionData, MotionTypeLightStateAttribute motionType, ColorLightStateAttribute color, BrightnessLightStateAttribute brightness)
        {
            _on = true;
            _collisionData = collisionData;
            _motionType = motionType;
            _color = color;
            _brightness = brightness;
        }

        public void RegisterInitialization(ShapeLightStateAttribute shape)
        {
            _shape = shape;
        }

        // TODO: probably want a factory for this
    }
}
