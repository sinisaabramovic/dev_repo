using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Stats.CharacterStats;

public class StatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    private CharacterStat stat;
    private string name;

    public CharacterStat Stat
    {
        get
        {
            return stat;
        }

        set
        {
            stat = value;
            UpdateStatsValue();
        }
    }

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
            nameText.text = name.ToLower();
        }
    }

    [SerializeField] Text nameText;
    [SerializeField] Text valueText;
    [SerializeField] StatInfo statInfo;

    private void OnValidate()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        nameText = texts[0];
        valueText = texts[1];

        if(statInfo == null)
        {
            statInfo = FindObjectOfType<StatInfo>();
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        statInfo.showInfo(stat, name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        statInfo.HideInfo();
    }

    public void UpdateStatsValue()
    {
        valueText.text = stat.Value.ToString();
    }

}
