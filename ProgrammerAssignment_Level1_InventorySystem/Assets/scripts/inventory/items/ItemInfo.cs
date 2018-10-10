using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ItemInfo : MonoBehaviour {

    [SerializeField] Text itemNameText;
    [SerializeField] Text itemSlotText;
    [SerializeField] Text itemStatsText;

    private StringBuilder stringBuilder = new StringBuilder();

    public void ShowInfo(EquipableItem item)
    {
        itemNameText.text = item.itemName;
        itemSlotText.text = item.equipmentType.ToString();

        stringBuilder.Length = 0;
        addStats(item.strengthBonus, "Str.");
        addStats(item.intelligenceBonus, "Int.");
        addStats(item.agilityBouns, "A.");
        addStats(item.dexterityBonus, "Dex.");

        addStats(item.strengthPercentBonus, "Str.", true);
        addStats(item.intelligencePercentBonus, "Int.", true);
        addStats(item.agilityPercentBouns, "A.", true);
        addStats(item.dexterityPercentBonus, "Dex.", true);

        itemStatsText.text = stringBuilder.ToString();

        gameObject.SetActive(true);
    }

    public void HideInfo()
    {
        gameObject.SetActive(false);
    }

    private void addStats(float value, string statName, bool isPrecent = false)
    {
        if(value != 0)
        {
            if (stringBuilder.Length > 0)
                stringBuilder.AppendLine();
            
            if (value > 0)
                stringBuilder.Append("+");

            if(isPrecent)
            {
                stringBuilder.Append(value * 100);
                stringBuilder.Append("% ");
            }
            else
            {
                stringBuilder.Append(value);
                stringBuilder.Append(" ");
            }

            stringBuilder.Append(statName);
        }
    }

}
