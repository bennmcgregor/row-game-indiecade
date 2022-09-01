using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Information class for an item
[System.Serializable]
public class ItemInfo
{
    [SerializeField]
    private int itemId;
    [SerializeField]
    private int maxPerStack;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private Sprite sprite;

    public ItemInfo(int itemId, int maxPerStack)
    {
        this.itemId = itemId;
        this.maxPerStack = maxPerStack;
    }

    public int GetItemId()
    {
        return this.itemId;
    }

    public int GetMaxPerStack()
    {
        return this.maxPerStack;
    }

    public Sprite GetIcon()
    {
        return this.icon;
    }

    public string GetName()
    {
        return this.name;
    }

    public string GetDescription()
    {
        return this.description;
    }

    public Sprite GetSprite()
    {
        return this.sprite;
    }
}
