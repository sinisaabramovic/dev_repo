using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stats.CharacterStats;
public class StatPanel : MonoBehaviour {

    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames;

    private CharacterStat[] stats;

    private void OnValidate()
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();
        UpdateStatNames();
    }

    public void SetStat(params CharacterStat[] characterStats)
    {
        stats = characterStats;

        if(stats.Length > statDisplays.Length)
        {
            Debug.LogError("Not Enough Stats to Displays");
            return;
        }

        for (int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].gameObject.SetActive(i < statDisplays.Length);

            if(i < stats.Length)
                statDisplays[i].Stat = stats[i];
        }
    }

    public void UpdateStatValues()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].UpdateStatsValue();
        }
    }

    public void UpdateStatNames()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statDisplays[i].Name = statNames[i];
        }
    }
}
