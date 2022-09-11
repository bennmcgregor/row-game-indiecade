﻿using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class StartStealWaterEffector : MonoBehaviour
    {
        [SerializeField] private Gate _gate;
        [SerializeField] private CanalGateInteractable _canalGateInteractable;

        private QuestRunner _questRunner;

        [Inject]
        public void Initialize(QuestRunner questRunner)
        {
            _questRunner = questRunner;
        }

        private void Awake()
        {
            _gate.OnOpenGate += OnOpenGate;
        }

        private void OnOpenGate()
        {
            _questRunner.CurrentQuest.CompleteCurrentChallenge();
            _canalGateInteractable.gameObject.SetActive(false);
        }
    }
}
