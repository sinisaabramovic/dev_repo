using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;
using Stats.CharacterStats;
           
public class InventoryManager : MonoBehaviour {

    public CharacterStat Strength;
    public CharacterStat Agility;
    public CharacterStat Intelligence;
    public CharacterStat Dexterity;

    public GameObject pickableItemTemplate;

    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] DropPanel dropPanel;
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemInfo itemInfo;
    [SerializeField] Image draggableItem;

    private ItemSlot draggedSlot;
    private Transform player;

    private void OnValidate()
    {
        if (itemInfo == null)
            itemInfo = FindObjectOfType<ItemInfo>();
    }

    private void Awake()
    {
        statPanel.SetStat(Strength, Agility, Intelligence, Dexterity);
        statPanel.UpdateStatValues();

        inventory.onRightMouseButton += Equip;
        equipmentPanel.onRightMouseButton += unEquip;

        inventory.onPointerEnterEvent += showItemInfo;
        equipmentPanel.onPointerEnterEvent += showItemInfo;

        inventory.onPointerExitEvent += hideItemInfo;
        equipmentPanel.onPointerExitEvent += hideItemInfo;

        inventory.onBeginDrag += beginDrag;
        equipmentPanel.onBeginDrag += beginDrag;

        inventory.onEndDrag += endDrag;
        equipmentPanel.onEndDrag += endDrag;

        inventory.onDrag += Drag;
        equipmentPanel.onDrag += Drag;

        inventory.onDrop += Drop;
        equipmentPanel.onDrop += Drop;

        dropPanel.onDrop += dropOut;

    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Equip(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.Item as EquipableItem;
        if(equipableItem != null){
            Equip(equipableItem);
        }
    }

    private void unEquip(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.Item as EquipableItem;
        if (equipableItem != null)
        {
            unEquip(equipableItem);
        }
    }

    private void showItemInfo(ItemSlot itemSlot)
    {
        EquipableItem equipableItem = itemSlot.Item as EquipableItem;
        if (equipableItem != null)
        {
            itemInfo.ShowInfo(equipableItem);
        } 
    }

    private void hideItemInfo(ItemSlot itemSlot)
    {
        itemInfo.HideInfo();
    }

    private void beginDrag(ItemSlot itemSlot)
    {
        if(itemSlot.Item != null)
        {
            draggedSlot = itemSlot;
            draggableItem.sprite = itemSlot.Item.icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }

    private void endDrag(ItemSlot itemSlot)
    {
        draggableItem.enabled = false;
        draggedSlot = null;
    }

    private void Drag(ItemSlot itemSlot)
    {
        if(draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;    
        }
    }

    private void Drop(ItemSlot dropItem)
    {

        if (draggedSlot == null)
        {
            return;
        }

        if(dropItem.CanAddStack(draggedSlot.Item))
        {
            addStacks(dropItem);
        }

        else if(dropItem.canReciveItem(draggedSlot.Item) && draggedSlot.canReciveItem(dropItem.Item))
        {
            swapItems(dropItem);
        }
    }

    private void swapItems(ItemSlot dropItem)
    {
        EquipableItem dragItem = draggedSlot.Item as EquipableItem;
        EquipableItem dropSlotItem = dropItem.Item as EquipableItem;

        if (draggedSlot is EquipmentSlot)
        {
            if (dragItem != null)
                dragItem.UnEquip(this);
            if (dropSlotItem != null)
                dropSlotItem.Equip(this);
        }

        if (dropItem is EquipmentSlot)
        {
            if (dragItem != null)
                dragItem.Equip(this);
            if (dropSlotItem != null)
                dropSlotItem.UnEquip(this);
        }

        statPanel.UpdateStatValues();

        Item draggedItem = draggedSlot.Item;
        int draggedItemAmount = draggedSlot.Amount;

        draggedSlot.Item = dropItem.Item;
        draggedSlot.Amount = dropItem.Amount;

        dropItem.Item = draggedItem;
        dropItem.Amount = draggedItemAmount;

        Analytics.CustomEvent("Player swaps items ", new Dictionary<string, object>
        {
            {"item 1", dropItem.Item.itemName},
            {"item 2", draggedItem.itemName}
        });
    }

    private void addStacks(ItemSlot dropItem)
    {
        int numAddableStack = dropItem.Item.maxStack - dropItem.Amount;
        int stacksToAdd = Mathf.Min(numAddableStack, draggedSlot.Amount);

        dropItem.Amount += stacksToAdd;
        draggedSlot.Amount -= stacksToAdd;
    }

    public void Equip(EquipableItem item)
    {
        if(inventory.RemoveItem(item))
        {
            EquipableItem previousItem;
            if(equipmentPanel.AddItem(item, out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    previousItem.UnEquip(this);
                    statPanel.UpdateStatValues();
                }

                item.Equip(this);
                statPanel.UpdateStatValues();
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }

    public void unEquip(EquipableItem item)
    {
        Analytics.CustomEvent("Player unEquip item", new Dictionary<string, object>
        {
            {"item droped out", item.itemName}
        });

        if(!inventory.isInventoryFull() && equipmentPanel.RemoveItem(item))
        {
            item.UnEquip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }

    private void dropOut(ItemSlot itemSlot)
    {

        Analytics.CustomEvent("Player drop out item", new Dictionary<string, object>
        {
            {"item droped out", draggedSlot.Item.itemName}
        });

        GameObject clone = Instantiate(pickableItemTemplate, player.position, player.rotation);
        clone.GetComponent<ItemPickup>().Item = draggedSlot.Item;
        clone.GetComponent<ItemPickup>().RefreshIcon();

        inventory.RemoveItem(draggedSlot.Item);
        equipmentPanel.RemoveItem((EquipableItem)draggedSlot.Item);

    }
}
