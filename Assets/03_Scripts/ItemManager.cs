using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]

public class ItemManager
{
    public string itemName;         // 아이템의 이름
    public int itemID;              // 아이템의 고유번호
    public string itemDes;          // 아이템의 설명
    public Sprite itemIcon;      // 아이템의 아이콘(2D)

    public ItemManager()
    {

    }

    public ItemManager(string name, int id, string desc)
    // 아이템의 필요한 속성을 모두 위에 적어줍니다.(다른곳에서 받아올 예정)
    {
        itemName = name;
        // 윗 줄과 같이 모두 연결해줍니다.
        itemID = id;
        itemDes = desc;      
        itemIcon = Resources.Load<Sprite>("ItemIcons/" + id.ToString())as Sprite;
        
    }

    public void Show()
    {
        Debug.Log( "name= " + itemName + " id= " + itemID.ToString() + " itemDes= " + itemDes+ " ItemIcons= ItemIcons/" + itemID.ToString());
    }
}
