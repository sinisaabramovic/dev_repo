using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EquipmentPanel : MonoBehaviour {

    [SerializeField] Transform equipmentSlotsParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;

    public event Action<ItemSlot> onPointerEnterEvent;
    public event Action<ItemSlot> onPointerExitEvent;
    public event Action<ItemSlot> onRightMouseButton;
    public event Action<ItemSlot> onBeginDrag;
    public event Action<ItemSlot> onEndDrag;
    public event Action<ItemSlot> onDrag;
    public event Action<ItemSlot> onDrop;

    private void Start()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].onRightMouseButton += onRightMouseButton;
            equipmentSlots[i].onPointerEnterEvent += onPointerEnterEvent;
            equipmentSlots[i].onPointerExitEvent += onPointerExitEvent;
            equipmentSlots[i].onBeginDrag += onBeginDrag;
            equipmentSlots[i].onEndDrag += onEndDrag;
            equipmentSlots[i].onDrag += onDrag;
            equipmentSlots[i].onDrop += onDrop;
        }
    }

    private void OnValidate()
    {        
        equipmentSlots = equipmentSlotsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    public bool AddItem(EquipableItem item, out EquipableItem previousItem)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].equipmentType == item.equipmentType)
            {
                previousItem = (EquipableItem)equipmentSlots[i].Item;
                equipmentSlots[i].Item = item;
                return true;
            }
        }
        previousItem = null;
        return false;
    }

    public bool RemoveItem(EquipableItem item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].Item == item)
            {
                equipmentSlots[i].Item = null;
                return true;
            }
        }
        return false; 
    }
}
