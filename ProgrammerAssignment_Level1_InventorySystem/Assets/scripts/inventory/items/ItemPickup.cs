using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour {

    [SerializeField] Item item;
    public Item Item
    {
        get
        {
            return item;
        }

        set
        {
            item = value;
        }
    }

    int amount = 1;
    public int Amount
    {
        get
        {
            return amount;
        }

        set
        {
            amount = value;
        }
    }

    Inventory inventory;
    [SerializeField] KeyCode itemPickupKeyCode = KeyCode.E;
    ItemInfo itemInfo;
    GameObject itemInfoDialog;
    ItemEquip itemEquip;
    private bool isInRange = false;

    private Color normalColor = Color.white;
    private Color triggerdColor = new Color(1.0f, 0.6f, 0.0f, 1.0f);

    private SpriteRenderer icon;

    private void Start()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");

        itemInfoDialog = gameManager.GetComponent<GameManager>().itemInfoDialog;
        itemEquip = gameManager.GetComponent<GameManager>().itemEquip;
        itemInfo = gameManager.GetComponent<GameManager>().itemInfo;
        inventory = gameManager.GetComponent<GameManager>().inventory;

        icon = GetComponent<SpriteRenderer>();

    }

    private void OnValidate()
    { 
        icon = GetComponent<SpriteRenderer>();

        if (Item != null)
            icon.sprite = Item.icon;
    }

    private void Update()
    {        
        if(isInRange && Input.GetKeyDown(itemPickupKeyCode))
        {                       
            if(Item != null)
            {
                itemEquip.AddItemToEquip(Item, this.gameObject);
                itemInfoDialog.SetActive(true);
                if(Item as EquipableItem)
                        itemInfo.ShowInfo((EquipableItem)Item);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            icon.color = triggerdColor;
            isInRange = true;
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        icon.color = normalColor;
        isInRange = false; 
    }

    public void RefreshIcon()
    {
        icon = GetComponent<SpriteRenderer>();
        if (Item != null)
            icon.sprite = Item.icon;
    }
}
