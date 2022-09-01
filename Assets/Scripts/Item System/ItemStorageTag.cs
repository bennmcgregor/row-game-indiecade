using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Place this tag on an object if you want it to be a storage container
public class ItemStorageTag : MonoBehaviour
{
    [SerializeField]
    private int numSlots;

    public int GetNumSlots()
    {
        return numSlots;
    }
}
