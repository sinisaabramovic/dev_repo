using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class ItemEquip : MonoBehaviour {

    [SerializeField] Inventory inventory;
    [SerializeField] GameObject inventoryObject;

    private GameObject gameObjectItem;
    private Item _item;

    public void AddItemToEquip(Item item, GameObject paramGameObjectItem)
    {
        if(item != null)
            _item = item;
        gameObjectItem = paramGameObjectItem;
    }

    public void EquipItem()
    {
        if(_item != null && !inventory.isInventoryFull())
        {
            Analytics.CustomEvent("Player take item",
                                  new Dictionary<string, object>
                                    {
                                        {"item", _item.itemName},
                                    });
            inventoryObject.SetActive(true);
            inventory.AddItem(_item.GetCopy());
            _item = null;
            Destroy(gameObjectItem);
            closeDialog();      
        }
        else
        {
            Analytics.CustomEvent("Inventory is full, player try take item: ");
            Debug.Log("Inventory is full!");
            closeDialog();
        }
    }

    public void closeDialog()
    {
        gameObject.SetActive(false);
    }
}
