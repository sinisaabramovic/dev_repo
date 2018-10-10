using UnityEngine;

public enum EquipmentType
{
    EquipmentType_Helmet,
    EquipmentType_Agility,
    EquipmentType_Weapon,
    EquipmentType_Shiled,
    EquipmentType_Armour
}

[CreateAssetMenu(fileName = "Equipable Item", menuName = "Inventory/Equipable Item")]
public class EquipableItem : Item 
{
    public int strengthBonus;
    public int agilityBouns;
    public int intelligenceBonus;
    public int dexterityBonus;

    [Space]

    public float strengthPercentBonus;
    public float agilityPercentBouns;
    public float intelligencePercentBonus;
    public float dexterityPercentBonus;

    [Space]

    public EquipmentType equipmentType;

    public void Equip(InventoryManager c)
    {
        if (strengthBonus != 0)
            c.Strength.AddModifier(new Stats.CharacterStats.StatModifier(strengthBonus,
                                                                         Stats.CharacterStats.StatModType.Flat,
                                                                         this));

        if (agilityBouns != 0)
            c.Agility.AddModifier(new Stats.CharacterStats.StatModifier(agilityBouns,
                                                                         Stats.CharacterStats.StatModType.Flat,
                                                                         this));

        if (intelligenceBonus != 0)
            c.Intelligence.AddModifier(new Stats.CharacterStats.StatModifier(intelligenceBonus,
                                                                         Stats.CharacterStats.StatModType.Flat,
                                                                         this));
        if (dexterityBonus != 0)
            c.Dexterity.AddModifier(new Stats.CharacterStats.StatModifier(dexterityBonus,
                                                                         Stats.CharacterStats.StatModType.Flat,
                                                                         this));

        if (strengthPercentBonus != 0)
            c.Strength.AddModifier(new Stats.CharacterStats.StatModifier(strengthPercentBonus,
                                                                         Stats.CharacterStats.StatModType.PercentMult,
                                                                         this));
        if (agilityPercentBouns != 0)
            c.Agility.AddModifier(new Stats.CharacterStats.StatModifier(agilityPercentBouns,
                                                                         Stats.CharacterStats.StatModType.PercentMult,
                                                                         this));
        if (intelligencePercentBonus != 0)
            c.Intelligence.AddModifier(new Stats.CharacterStats.StatModifier(intelligencePercentBonus,
                                                                         Stats.CharacterStats.StatModType.PercentMult,
                                                                         this));
        if (dexterityPercentBonus != 0)
            c.Dexterity.AddModifier(new Stats.CharacterStats.StatModifier(dexterityPercentBonus,
                                                                         Stats.CharacterStats.StatModType.PercentMult,
                                                                         this));
    }

    public void UnEquip(InventoryManager c)
    {
        c.Strength.RemoveAllModifiersFromSource(this);
        c.Agility.RemoveAllModifiersFromSource(this);
        c.Dexterity.RemoveAllModifiersFromSource(this);
        c.Intelligence.RemoveAllModifiersFromSource(this);
    }

    public override Item GetCopy()
    {
        return Instantiate(this);
    }

    public override void Destroy()
    {
        Destroy(this);
    }

}
