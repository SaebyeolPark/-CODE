using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Sprite[] itemArr;
    public Image icon;
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
}
