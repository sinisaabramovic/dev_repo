using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using System;
using Stats.CharacterStats;
using UnityEngine.Serialization;

[Serializable]
public class Inventory : MonoBehaviour 
{ 
    
    [SerializeField] List<Item> startingItems;
    [SerializeField] Transform itemsParent;
    [SerializeField] ItemSlot[] itemSlots;

    public event Action<ItemSlot> onPointerEnterEvent;
    public event Action<ItemSlot> onPointerExitEvent;
    public event Action<ItemSlot> onRightMouseButton;
    public event Action<ItemSlot> onBeginDrag;
    public event Action<ItemSlot> onEndDrag;
    public event Action<ItemSlot> onDrag;
    public event Action<ItemSlot> onDrop;

    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i ++)
        {
            itemSlots[i].onRightMouseButton += onRightMouseButton;
            itemSlots[i].onPointerEnterEvent += onPointerEnterEvent;
            itemSlots[i].onPointerExitEvent += onPointerExitEvent;
            itemSlots[i].onBeginDrag += onBeginDrag;
            itemSlots[i].onEndDrag += onEndDrag;
            itemSlots[i].onDrag += onDrag;
            itemSlots[i].onDrop += onDrop;
        }

        setStartingItems();
    }

    private void OnValidate()
    {
        if (itemsParent != null)
            itemSlots = itemsParent.GetComponentsInChildren<ItemSlot>();

        setStartingItems();
    }

    private void setStartingItems()
    {        
        for (int i = 0; i < startingItems.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = startingItems[i].GetCopy();
            itemSlots[i].Amount = 1;
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
            itemSlots[i].Amount = 0;
        }
    }

    public bool isInventoryFull()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == null)
            {
                return false;
            }
        }

        return true;
    }

    public int ItemCount(string itemID)
    {
        int number = 0;

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item.Id == itemID)
            {
                number++;
            }
        }

        return number;
    }

    public bool AddItem(Item item)
    {
        Analytics.CustomEvent("Player add item", new Dictionary<string, object>
        {
            {"item", item.itemName}
        });
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == null || itemSlots[i].CanAddStack(item))
            {
                itemSlots[i].Item = item;
                itemSlots[i].Amount++;
                return true;
            }
        }

        return false;
    }

    public bool RemoveItem(Item item)
    {
        Analytics.CustomEvent("Player remove item", new Dictionary<string, object>
        {
            {"item", item.itemName}
        });
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == item)
            {
                itemSlots[i].Amount--;            

                return true;
            }
        }

        return false;
    }

    public Item RemoveItem(string itemID)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            Item item = itemSlots[i].Item;
            if(item != null && item.Id == itemID)
            {
                itemSlots[i].Amount--;
    
                return item;
            }
        }

        return null;
    }
}
