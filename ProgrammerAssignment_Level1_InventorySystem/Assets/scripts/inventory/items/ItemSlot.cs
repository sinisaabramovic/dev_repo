using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler {

    [SerializeField] Text amountText;
    [SerializeField] private Image image;

    public event Action<ItemSlot> onPointerEnterEvent;
    public event Action<ItemSlot> onPointerExitEvent;
    public event Action<ItemSlot> onRightMouseButton;
    public event Action<ItemSlot> onBeginDrag;
    public event Action<ItemSlot> onEndDrag;
    public event Action<ItemSlot> onDrag;
    public event Action<ItemSlot> onDrop;

    private Color normalColor = Color.white;
    private Color disabledColor = new Color(1, 1, 1, 0);

    private Item item;
    public Item Item
    {
        get
        {
            return item;
        }

        set
        {
            item = value;
            if(item == null){
                image.color = disabledColor;
            }else{
                image.sprite = item.icon;
                image.enabled = true;
                image.color = normalColor;
            }
        }
    }

    public int Amount
    {
        get
        {
            return _amount;
        }

        set
        {
            _amount = value;

            if (_amount < 0) _amount = 0;
            if (_amount == 0) Item = null;

            if(amountText != null)
            {
                amountText.enabled = (item != null && _amount > 1);
                if (amountText.enabled)
                {
                    amountText.text = _amount.ToString();
                }
            }       
        }
    }

    private int _amount;

    private void Awake()
    {
        if (amountText == null)
            amountText = GetComponentInChildren<Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {            
            if(onRightMouseButton != null)
            {                   
                onRightMouseButton(this);
            }
        }
    }

    public virtual bool CanAddStack(Item item, int amount = 1)
    {
        bool returnValue = Item != null && Item.Id == item.Id && Amount + amount <= item.maxStack;        
        return returnValue;
    }

    public virtual bool canReciveItem(Item item)
    {
        return true;
    }

    protected virtual void OnValidate()
    {
        if (image == null)
            image = GetComponent<Image>();
        if (amountText == null)
            amountText = GetComponentInChildren<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (onPointerEnterEvent != null)
            onPointerEnterEvent(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onPointerExitEvent != null)
            onPointerExitEvent(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null)
            onBeginDrag(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDrag != null)
            onEndDrag(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
            onDrag(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (onDrop != null)
            onDrop(this);
    }

    public void Destroy()
    {
        
    }
}
