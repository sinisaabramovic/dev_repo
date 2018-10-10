using UnityEngine;

public class EquipmentSlot : ItemSlot {

    public EquipmentType equipmentType;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = equipmentType.ToString() + " Slot";
    }

    public override bool canReciveItem(Item item)
    {
        if (item == null)
            return true;

        EquipableItem equipableItem = item as EquipableItem;

        return equipableItem != null && equipableItem.equipmentType == equipmentType;

    }
}
