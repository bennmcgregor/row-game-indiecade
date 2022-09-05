using System;
using UnityEngine;
using Yarn.Unity;

namespace IndieCade
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogueYarnProjectScriptableObject")]
    public class DialogueYarnProjectScriptableObject : ScriptableObject
    {
        public YarnProject TestProject;
    }


}
