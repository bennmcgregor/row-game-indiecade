using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
     
public class StoredItem : MonoBehaviour, IPointerClickHandler {


    private int itemId = 0; 
    private int inventoryId = 0;
    private int itemAmount = 0;
    
    private ItemStorage m_itemStorage;
    private ItemDictionary m_dict;
    private Image m_SpriteRenderer;

    [SerializeField]
    private GameObject itemName;

    [SerializeField]
    private GameObject itemQuantity;

    void Start()
    {
        GameObject context = GameObject.Find("ItemSystem");
        this.m_dict = context.GetComponent<ItemDictionary>();
        
        this.m_itemStorage = context.GetComponent<ItemStorage>();
    
        this.m_SpriteRenderer = GetComponent<Image>();
        this.m_SpriteRenderer.sprite = m_dict.GetItemInfo(this.itemId).GetSprite();
    }

    public StoredItem(int inventoryId, int itemId)
    {
        GameObject context = GameObject.Find("ItemSystem");
        this.m_dict = context.GetComponent<ItemDictionary>();
        
        this.m_itemStorage = context.GetComponent<ItemStorage>();
    
        this.m_SpriteRenderer = GetComponent<Image>();
        this.m_SpriteRenderer.sprite = m_dict.GetItemInfo(this.itemId).GetSprite();
    }

    // Update the Item Info for a stored item
    public void SetItemInfo(int inventoryId, int itemId, int itemAmount)
    {
        // For some reason the Start method occasionally gets called after SetItemId, so we need to set this again to prevent null reference errors
        this.itemId = itemId;
        this.itemAmount = itemAmount;
        this.inventoryId = inventoryId;

        GameObject context = GameObject.Find("ItemSystem");
        this.m_dict = context.GetComponent<ItemDictionary>();
        
        this.m_itemStorage = context.GetComponent<ItemStorage>();
    
        this.m_SpriteRenderer = GetComponent<Image>();
        this.m_SpriteRenderer.sprite = m_dict.GetItemInfo(this.itemId).GetSprite();


        itemName.GetComponent<TextMeshProUGUI>().text = m_dict.GetItemInfo(this.itemId).GetName();
        itemQuantity.GetComponent<TextMeshProUGUI>().text = itemAmount.ToString();

    }
    
    // On clicking on a stored item
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // TODO: Transfer between inventories
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Drop item
            m_itemStorage.RemoveItem(inventoryId, itemId); 
        }
    }
}
