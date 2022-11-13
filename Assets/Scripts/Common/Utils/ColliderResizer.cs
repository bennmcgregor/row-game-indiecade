using System;
using UnityEngine;

namespace IndieCade
{
    public class ColliderResizer : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _boxCollider2D;
        [SerializeField] private RectTransform _rectTransform;

        public void Resize()
        {
            var rect = _rectTransform.rect;
            _boxCollider2D.size = new Vector2(rect.width, rect.height);
        }

        // TEMP
        private void Update()
        {
            Resize();
        }
    }
}
