using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnItemInventory : ButttonAbstract
{
    [SerializeField] protected TextMeshProUGUI txtItemName;
    [SerializeField] protected TextMeshProUGUI txtItemCount;
    [SerializeField] protected Image image;

    [SerializeField] protected ItemInventory itemInventory;
    public ItemInventory ItemInventory => itemInventory;

    protected virtual void FixedUpdate()
    {
        this.ItemUpdating();
    }

    public override void OnClick()
    {
        //TODO: open item detail
        //UIItemDetail.Instance.Open(item);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemName();
        this.LoadItemCount();
        this.LoadImage();
    }

    protected virtual void LoadItemName()
    {
        if (this.txtItemName != null) return;
        this.txtItemName = transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTexts", gameObject);
    }

    protected virtual void LoadItemCount()
    {
        if (this.txtItemCount != null) return;
        this.txtItemCount = transform.Find("ItemCount").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadTexts", gameObject);
    }
    protected virtual void LoadImage()
    {
        if (this.image != null) return;
        this.image = transform.Find("Image").GetComponent<Image>();
        Debug.Log(transform.name + " : LoadImage ", gameObject);
    }

    public virtual void SetItem(ItemInventory itemInventory)
    {
        this.itemInventory = itemInventory;
    }


    protected virtual void ItemUpdating()
    {
        this.txtItemName.text = this.itemInventory.GetItemName();
        this.txtItemCount.text = this.itemInventory.itemCount.ToString();
        this.image.sprite = this.itemInventory.ItemProfile.sprite;
        if (this.itemInventory.itemCount == 0) Destroy(gameObject);
    }
}
