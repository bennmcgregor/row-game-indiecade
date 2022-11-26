using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfigSliderDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string _name;
    [SerializeField] private Slider _slider;

    // Update is called once per frame
    void Update()
    {
        _text.text = $"{_name}: {_slider.value}";
    }
}
