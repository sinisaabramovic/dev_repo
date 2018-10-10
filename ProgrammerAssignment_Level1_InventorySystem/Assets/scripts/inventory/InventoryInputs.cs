using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInputs : MonoBehaviour {
    
    [SerializeField] GameObject inventoryGameObject;
    [SerializeField] GameObject itemInfoGameObject;
    [SerializeField] KeyCode[] inventoryKeyCodes;
    [SerializeField] KeyCode[] itemInfoKeyCodes;
    [SerializeField] GameObject hudInventoryButton;

	void Update () 
    {
        for (int i = 0; i < inventoryKeyCodes.Length; i++)
        {
            if(Input.GetKeyDown(inventoryKeyCodes[i]))
            {
                inventoryGameObject.SetActive(!inventoryGameObject.activeSelf);
                break;
            }                    
        }

        for (int i = 0; i < itemInfoKeyCodes.Length; i++)
        {
            if (Input.GetKeyDown(itemInfoKeyCodes[i]))
            {
                itemInfoGameObject.SetActive(false);
                break;
            }
        }


        hudInventoryButton.SetActive(!inventoryGameObject.activeSelf);
	}
      
}
