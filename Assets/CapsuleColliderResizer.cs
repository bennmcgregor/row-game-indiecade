using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapsuleColliderResizer : MonoBehaviour
{
    private CapsuleCollider2D capsuleCollider;
    [SerializeField] Slider EraserSizeSliderX;
    [SerializeField] Slider EraserSizeSliderY;

    void Start()
    {
        capsuleCollider = gameObject.GetComponent(typeof(CapsuleCollider2D)) as CapsuleCollider2D;
        EraserSizeSliderX.onValueChanged.AddListener(delegate { UpdateSlider(); });
        EraserSizeSliderY.onValueChanged.AddListener(delegate { UpdateSlider(); });
    }

    private void UpdateSlider()
    {
        capsuleCollider.size = new Vector2(EraserSizeSliderX.value, EraserSizeSliderY.value);
    }
}
