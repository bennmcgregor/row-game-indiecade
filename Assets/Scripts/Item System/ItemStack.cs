using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack
{
    private int quantity;
    private int itemId;
    private ItemDictionary dict;
    
    public ItemStack(int itemId, int quantity)
    {
        GameObject context = GameObject.Find("ItemSystem");
        dict = context.GetComponent<ItemDictionary>();

        this.itemId = itemId;
        this.quantity = quantity;
    }
    
    // Removes an item from an item stack, returns true if the item is removed, false if the item is not removed
    public bool RemoveItem()
    {
        if (this.quantity > 0)
        {
            this.quantity--;
            return true;
        }
        return false;
    }

    // Add an item  to an item stack, returns true if the item is added, false if the item is not added
    public void AddItem()
    {
        if (dict.GetItemInfo(this.itemId).GetMaxPerStack() > 1)
        {
            this.quantity++;
            return;
        }
    }

    public int GetItemId()
    {
        return this.itemId;
    }

    public int GetNumItems()
    {
        return this.quantity;
    }

}