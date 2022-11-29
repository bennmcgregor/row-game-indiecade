using System;
namespace IndieCade
{
    public class LightStateData : StateData<LightState>
    {
        private string _id;
        private bool _on;
        private bool _detectingCollisions;
        private MotionTypeLightStateAttribute _motionType; // abstract class
        private ShapeLightStateAttribute _shape; // abstract class
        private ColorLightStateAttribute _color; // class
        private float _brightness;
        // TODO: find a way to register/play different sound effects based on state transitions

        public string Id => _id;
        public bool On => _on;
        public bool DetectingCollisions => _detectingCollisions;
        public MotionTypeLightStateAttribute MotionType => _motionType;
        public ShapeLightStateAttribute Shape => _shape;
        public ColorLightStateAttribute Color => _color;
        public float Brightness => _brightness;

        public LightStateData(LightState stateName) : base(stateName) {}

        public void SetId(string id)
        {
            _id = id;
        }

        public void RegisterOff()
        {
            _on = false;
        }

        public void RegisterOn(bool detectingCollisions, MotionTypeLightStateAttribute motionType, ShapeLightStateAttribute shape, ColorLightStateAttribute color, float brightness)
        {
            _on = true;
            _detectingCollisions = detectingCollisions;
            _motionType = motionType;
            _shape = shape;
            _color = color;
            _brightness = brightness;
        }

        // TODO: probably want a factory for this
    }
}
