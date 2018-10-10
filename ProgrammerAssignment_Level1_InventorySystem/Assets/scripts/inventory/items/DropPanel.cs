using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPanel : MonoBehaviour {

    public event Action<ItemSlot> onDrop;
    [SerializeField] ItemSlot itemSlot;
	void Start () {
        itemSlot.onDrop += onDrop;
	}	
}
