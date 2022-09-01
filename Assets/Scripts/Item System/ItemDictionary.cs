using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    [SerializeField]
    private ItemInfo[] itemList;

    // Get the information for an item
    public ItemInfo GetItemInfo(int itemId)
    {
        if (itemId >= itemList.Length || itemId < 0)
        {
            //throw new Exception("The itemId " + itemId + " does not exist.");
        }
        return itemList[itemId];
    }
}