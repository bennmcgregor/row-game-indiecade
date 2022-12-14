using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class FOVController : MonoBehaviour
{
    [SerializeField] private Slider FOVSlider;
    private CinemachineVirtualCamera vCam;

    void Start()
    {
        vCam = gameObject.GetComponent(typeof(CinemachineVirtualCamera)) as CinemachineVirtualCamera;
        FOVSlider.onValueChanged.AddListener(delegate { UpdateSlider(); });
    }

    private void UpdateSlider()
    {
        vCam.m_Lens.OrthographicSize = FOVSlider.value;
    }
}
