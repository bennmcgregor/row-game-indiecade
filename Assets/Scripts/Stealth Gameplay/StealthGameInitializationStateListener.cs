using System;
using UnityEngine;

namespace IndieCade
{
    public class StealthGameInitializationStateListener : GameInitializationStateListener
    {
        [SerializeField] private StealthRunnerUI _stealthRunnerUI;
        [SerializeField] private StealthRunner _stealthRunner;

        protected override void InitializeChallenge(ChallengeInitializationData challengeInitializationData)
        {
            if (challengeInitializationData.HasStealthGameplay)
            {
                _stealthRunner.Activate();
                _stealthRunnerUI.Activate();
            }
            else
            {
                _stealthRunner.Deactivate();
                _stealthRunnerUI.Deactivate();
            }
        }
    }
}
