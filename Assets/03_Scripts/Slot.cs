using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour
      , IPointerClickHandler
{
    public Sprite[] itemArr;
    public Image icon;
    public GameObject firstFloorPaper;
    public GameObject firstMap;
    public void AddItem(ItemManager _item)
    {
        for(int i=0; i<itemArr.Length; i++)
        {
            if (itemArr[i].name == _item.itemID.ToString())
            {
                icon.sprite = itemArr[i];
            }
        }
       // icon.sprite = Resources.Load<Sprite>("ItemIcons/1001" /*+ _item.itemID.ToString()*/) as Sprite;

    }
    public void RemoveItem()
    {
        icon.sprite = null;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (icon.sprite.name == "1003")
        {
          //  Debug.Log("Click");
            firstFloorPaper.SetActive(true);
            Inventory.instance.showInventory = !Inventory.instance.showInventory;
            Inventory.instance.inventoryUi.SetActive(Inventory.instance.showInventory);
            GameManager.instance.isControl = false;
        }
        else if (icon.sprite.name == "1012")
        {
            //Debug.Log("Click");
            firstMap.SetActive(true);
            Inventory.instance.showInventory = !Inventory.instance.showInventory;
            Inventory.instance.inventoryUi.SetActive(Inventory.instance.showInventory);
            GameManager.instance.isControl = false;

        }
    }
}
