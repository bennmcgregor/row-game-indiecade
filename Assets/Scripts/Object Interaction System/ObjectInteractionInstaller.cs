using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class ObjectInteractionInstaller : MonoInstaller
    {
        [SerializeField] private ObjectInteractionControl _objectInteractionControl;

        public override void InstallBindings()
        {
            Container.BindInstance(_objectInteractionControl).AsSingle();
        }
    }
}

