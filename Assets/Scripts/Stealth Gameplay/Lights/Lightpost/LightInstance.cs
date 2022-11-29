using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace IndieCade
{
    public class LightInstance : MonoBehaviour
    {
        [SerializeField] private List<Light2D> _lights;
        [SerializeField] private Collider2D _collider;

        public List<Light2D> Lights => _lights;
        public Collider2D Collider => _collider;
    }
}
