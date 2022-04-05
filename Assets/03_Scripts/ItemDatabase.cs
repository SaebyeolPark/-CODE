using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<ItemManager> itemList = new List<ItemManager>();
    //public Sprite[] sp = Resources.LoadAll<Sprite>("ItemIcons/" + "34x34icons180709");
    // public Sprite[] sprites;

    void Start()
    {
        //sprites = Resources.LoadAll<Sprite>("ItemIcons/34x34icons180709");

        // items.Add(new Item(이름, 아이템아이디, 설명));
        itemList.Add(new ItemManager("휴대폰", 1000, "This sword is normal style sword"));
        itemList.Add(new ItemManager("가위", 1001, "This sword is normal style sword"));
        itemList.Add(new ItemManager("풀", 1002, "This sword is normal style sword"));
        itemList.Add(new ItemManager("종이", 1003, "This sword is normal style sword"));
        itemList.Add(new ItemManager("명찰", 1004, "This sword is normal style sword"));
        itemList.Add(new ItemManager("양호실 열쇠", 1005, "This sword is normal style sword"));
        itemList.Add(new ItemManager("미술실 열쇠", 1006, "This sword is normal style sword"));
        itemList.Add(new ItemManager("미술실 창고 열쇠", 1007, "This sword is normal style sword"));
        itemList.Add(new ItemManager("이름표", 1008, "This sword is normal style sword"));
        itemList.Add(new ItemManager("주사기", 1009, "This sword is normal style sword"));
        itemList.Add(new ItemManager("주사기피", 1010, "This sword is normal style sword"));
        itemList.Add(new ItemManager("물병", 1011, "This sword is normal style sword"));
        itemList.Add(new ItemManager("지도", 1012, "This sword is normal style sword"));

    }

}
