using System;
namespace IndieCade
{
    public class NoneShapeLightStateAttribute : ShapeLightStateAttribute
    {
        public NoneShapeLightStateAttribute() : base(LightShape.NONE) {}
    }

}
