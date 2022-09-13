using System;
using UnityEngine;
using Yarn.Unity;

namespace IndieCade
{
    public abstract class CommandRegistrar : MonoBehaviour
    {
        [SerializeField] protected DialogueRunner _dialogueRunner;

        private void Awake()
        {
            RegisterCommand();
        }

        protected abstract void RegisterCommand();
    }
}
