using System;
using UnityEngine;

namespace IndieCade
{
    public class TestCompleteChallenge : MonoBehaviour
    {
        [SerializeField] private QuestUpdateEffector _questUpdateEffector;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Collected water!");
            _questUpdateEffector.CompleteChallenge();
        }
    }
}
