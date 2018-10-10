using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryClose : MonoBehaviour {

    [SerializeField] GameObject inventoryGameObject;

    public void CloseInventoryDialog()
    {
        inventoryGameObject.SetActive(false);
    }

    public void OpenInventoryDialog()
    {
        inventoryGameObject.SetActive(true);
    }
}
