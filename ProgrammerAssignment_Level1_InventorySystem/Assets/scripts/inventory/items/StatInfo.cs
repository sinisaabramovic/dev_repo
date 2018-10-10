using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Stats.CharacterStats;

public class StatInfo : MonoBehaviour 
{
    [SerializeField] Text statNameText;
    [SerializeField] Text statModifierLabelText;
    [SerializeField] Text statModifierText;

    private StringBuilder stringBuilder = new StringBuilder();

    public void showInfo(CharacterStat stat, string statName)
    {
        statNameText.text = getStatTopText(stat, statName);
        statModifierText.text = getStatModifierText(stat);
        

        gameObject.SetActive(true);
    }

    public void HideInfo()
    {
        gameObject.SetActive(false);
    }

    private string getStatTopText(CharacterStat stat, string statName)
    {
        stringBuilder.Length = 0;
        stringBuilder.Append(statName);
        stringBuilder.Append(" ");
        stringBuilder.Append(stat.Value);

        if(stat.Value != stat.BaseValue)
        {
            stringBuilder.Append(" (");
            stringBuilder.Append(stat.BaseValue);

            if (stat.Value > stat.BaseValue)
                stringBuilder.Append("+");

            stringBuilder.Append(System.Math.Round(stat.Value - stat.BaseValue, 2));
            stringBuilder.Append(")");
        }

        return stringBuilder.ToString();
    }

    private string getStatModifierText(CharacterStat stat)
    {
        stringBuilder.Length = 0;

        foreach(StatModifier mod in stat.StatModifiers)
        {
            if (stringBuilder.Length > 0)
                stringBuilder.AppendLine();
            
            if(mod.Value > 0){
                stringBuilder.Append("+");
            }

            if(mod.Type == StatModType.Flat)
            {
                stringBuilder.Append(mod.Value);
            }
            else
            {
                stringBuilder.Append(mod.Value * 100);
                stringBuilder.Append("%");
            }

            EquipableItem item = mod.Source as EquipableItem;

            if(item != null)
            {
                stringBuilder.Append(" ");
                stringBuilder.Append(item.itemName);
            }
            else
            {
                Debug.LogError("Modifier is not an Equipable item");
            }
        }

        return stringBuilder.ToString();
    }
    
}
