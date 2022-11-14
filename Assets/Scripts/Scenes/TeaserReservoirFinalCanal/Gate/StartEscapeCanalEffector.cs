using System;
using UnityEngine;
using Zenject;

namespace IndieCade
{
    public class StartEscapeCanalEffector : MonoBehaviour
    {
        [SerializeField] private WaterSourceInteractable _waterSource;
        [SerializeField] private PlayerWaterInventory _playerWaterInventory;
        [SerializeField] private ObjVerticalTranslator _gate;

        private QuestRunner _questRunner;

        [Inject]
        public void Initialize(QuestRunner questRunner)
        {
            _questRunner = questRunner;
        }

        private void Awake()
        {
            _waterSource.OnEndInteracting += OnFillingStopped;
        }

        private void OnFillingStopped()
        {
            if (_playerWaterInventory.IsFilled)
            {
                _questRunner.CurrentQuest.CompleteCurrentChallenge();
                _waterSource.Deactivate();
                _gate.TranslateReverse();
            }
        }
    }
}
