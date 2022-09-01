using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemStorage : MonoBehaviour
{   
    [SerializeField]
    private int numInventorySlots;
    private List<ItemStack[]> storage; // ArrayList of item storage arrays
    private ItemDictionary dict;
    private Dictionary<string, int> storageIdList; // Associates a storage object: "SceneName.ObjectName" with an inventoryId

    [SerializeField]
    private GameObject slotPrefab;

    [SerializeField]
    private GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
        dict = this.gameObject.GetComponent<ItemDictionary>();

        GameObject canvas = GameObject.Find("InventoryDisplay");
        canvas.GetComponent<Canvas>().enabled = false;

        this.storage = new List<ItemStack[]>();
        this.storage.Add(new ItemStack[numInventorySlots]);
        SceneManager.sceneLoaded += GenerateStorageIds;
    }

    // Generate storage ID's for all objects with storage capabilities on the map
    public void GenerateStorageIds(Scene scene, LoadSceneMode mode)
    {
        GameObject[] objects = (GameObject[]) GameObject.FindObjectsOfType(typeof(MonoBehaviour));
        foreach (GameObject g in objects)
        {
            ItemStorageTag gi = g.GetComponent<ItemStorageTag>();
            storageIdList.Add(scene.name + "." + g.name, storage.Count);
            storage.Add(new ItemStack[gi.GetNumSlots()]); 
        }
    }
    
    // Returns true if the item is added, and false if the item cannot be added
    public bool AddItem(int inventoryId, int itemId)
    {
        Debug.Log("add item");
        // Check each slot of the inventory to see if the item can be added
        for (int i = 0; i < this.storage[inventoryId].Length; i++)
        {
            Debug.Log("in loop");
            Debug.Log("Inventory ID: " + inventoryId + " Item Id: " + itemId);
            // If the slot has the same item, and item is stackable, and stack is less than max size, add one to stack
            if (this.storage[inventoryId][i] != null && this.storage[inventoryId][i].GetItemId() == itemId && dict.GetItemInfo(this.storage[inventoryId][i].GetItemId()).GetMaxPerStack() > 1 && dict.GetItemInfo(this.storage[inventoryId][i].GetItemId()).GetMaxPerStack() >= this.storage[inventoryId][i].GetNumItems() + 1)
            {
                Debug.Log("Add item to item stack");
                this.storage[inventoryId][i].AddItem(); // Add the item to the itemStack
                return true;
            }
            // If the slot is empty, add the item to the slot
            else if (this.storage[inventoryId][i] == null)
            {
                Debug.Log("Add item to new item stack");
                this.storage[inventoryId][i] = new ItemStack(itemId, 1); // Create new stack
                return true;
            }
        }
        Debug.Log("nothing");
        return false;
    }

    // Removes an item, returns true if the item is removed, false if the item is not removed
    public bool RemoveItem(int inventoryId, int itemId)
    {
        for (int i = 0; i < storage[inventoryId].Length; i++)
        {
            if (storage[inventoryId][i] != null && storage[inventoryId][i].GetItemId() == itemId)
            {
                storage[inventoryId][i].RemoveItem();
                if (storage[inventoryId][i].GetNumItems() == 0)
                {
                    storage[inventoryId][i] = null;
                }

                GameObject player = GameObject.Find("WalkingPlayer"); // TODO: Find a way to get the game object of the character (either the walking character or the boat game object)
                Vector3 pos = player.transform.position;
                pos.x -= 1;
                pos.y -= 1;
                GameObject groundItem = Instantiate(itemPrefab, pos, Quaternion.identity);
                groundItem.GetComponent<GameItem>().SetItemId(itemId);

                RebuildDisplay(inventoryId);

                return true;
            }
        }
        return false;
    }

    // Enable/Disable the canvas for the inventory
     public void ToggleDisplay()
     {   
        Debug.Log("toggle");
         RebuildDisplay(0);
         GameObject canvas = GameObject.Find("InventoryDisplay");
         canvas.GetComponent<Canvas>().enabled = !canvas.GetComponent<Canvas>().enabled;
     }

    // Pick up items
    public void PickUpItems()
    {
        GameObject[] gameItems = (GameObject[]) FindObjectsOfType(typeof(GameObject));
        GameObject player = GameObject.Find("WalkingPlayer"); // TODO: Find a way to get the game object of the character (either the walking character or the boat game object)

        // Find any game objects that are items within radius of the player and add them to the player's inventory
        foreach (GameObject g in gameItems)
        {
            GameItem gi = g.GetComponent<GameItem>();
            // If the object has a GameItem script
            if (gi != null)
            {
                //float distance = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(g.transform.position.x, g.transform.position.y));
                // // If distance is less than radius, add the item to the player's inventory
                //if (Mathf.Abs(distance) < gi.GetMinDistance())
                //{
                    gi.OnPickup(); // Pick up the item
                //}
            }
        }
    }

    // Build the Canvas for the inventory
    public void RebuildDisplay(int inventoryId)
    {
        // Remove all InventoryItems from the canvas
        foreach (Transform child in GameObject.Find("InventoryDisplayContent").transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (ItemStack i in storage[inventoryId])
        {
            if (i != null)
            {
                GameObject cloned = Instantiate(slotPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("InventoryDisplayContent").transform);
                cloned.transform.localPosition = Vector3.zero;
                cloned.GetComponent<StoredItem>().SetItemInfo(inventoryId, i.GetItemId(), i.GetNumItems());
            }
        }
    }
}
