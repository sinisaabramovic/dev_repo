using System;
using UnityEngine;

// commit
[CreateAssetMenu(fileName ="Item", menuName ="Inventory/Item")]
[Serializable]
public class Item : ScriptableObject {

    [SerializeField] string id;

    public string Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public string itemName;
    public Sprite icon;
    [Range(1,999)]
    public int maxStack = 1;

    private void OnValidate()
    {
        System.Guid guid = System.Guid.NewGuid();        
        id = guid.ToString();
    }

    public virtual Item GetCopy()
    {
        return this;
    }

    public virtual void Destroy()
    {        
        Destroy(this);
    }
}
