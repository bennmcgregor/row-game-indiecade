using System;
using UnityEngine;
namespace IndieCade
{
    public class PlayerSpawnPoint : MonoBehaviour
    {
        public void SetPlayerPositionAndRotation(Transform player)
        {
            player.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }
}
