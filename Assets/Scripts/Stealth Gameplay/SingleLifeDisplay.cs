using System;
using UnityEngine;
using UnityEngine.UI;

namespace IndieCade
{
    public class SingleLifeDisplay : MonoBehaviour
    {
        [SerializeField] private Image _onImage;
        [SerializeField] private Image _offImage;

        public void SetOnImage(Sprite sprite)
        {
            _onImage.sprite = sprite;
        }

        public void SetOffImage(Sprite sprite)
        {
            _offImage.sprite = sprite;
        }
    }
}
