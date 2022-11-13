using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieCade
{
    public class GateSoundEffectCoordinator : MonoBehaviour
    {
        [SerializeField] private ObjVerticalTranslator _gate;
        [SerializeField] private StopOnEventSoundEffect _gateSoundFX;

        private void Awake()
        {
            _gate.OnStartForwardTranslation += _gateSoundFX.Play;
            _gate.OnEndForwardTranslation += _gateSoundFX.InvokeStopPlayingAction;
        }
    }
}
