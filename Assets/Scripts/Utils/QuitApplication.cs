using System;
using UnityEngine;

namespace IndieCade
{
    public class QuitApplication : MonoBehaviour
    {
        public void Quit()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
