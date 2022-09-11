using System;
using UnityEngine;

namespace IndieCade
{
    public class TestFailChallenge : MonoBehaviour
    {
        [SerializeField] private QuestUpdateEffector _questUpdateEffector;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Failed to collect water!");
            _questUpdateEffector.FailChallenge();
        }
    }
}
