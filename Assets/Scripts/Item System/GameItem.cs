using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    [SerializeField]
    private int itemId;
    private ItemDictionary m_dict;
    private SpriteRenderer m_SpriteRenderer;

    void Start()
    {
        // Find the item dictionary
        GameObject context = GameObject.Find("ItemSystem");
        m_dict = context.GetComponent<ItemDictionary>();

        // Set the item sprite image
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = m_dict.GetItemInfo(this.itemId).GetSprite();
    }

    // Set the ID of the item
    public void SetItemId(int itemId)
    {
        this.itemId = itemId;
    }
    
    // If able, add the item to the inventory
    public void OnPickup()
    {
        GameObject player = GameObject.Find("ItemSystem");
        if (player.GetComponent<ItemStorage>().AddItem(0, this.itemId)) // Picked up items are always added to the player inventory
        {
            Destroy(this.gameObject);
        }
    }

    // TODO: Run script when item is used. This script should be put into the dictionary.
    public void OnUse()
    {
        
    }

    // Pickup the item when it is stepped on
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter item");
        OnPickup();
    }
}
