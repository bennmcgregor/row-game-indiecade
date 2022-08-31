using UnityEngine;

namespace IndieCade
{
    public class ButtonSceneChangeEffector : WorldMapSceneChangeEffector
    {
        public void OnClick()
        {
            ActivateScene();
        }
    }
}