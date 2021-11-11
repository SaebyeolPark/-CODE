using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory instance = null;

    zFoxVirtualPad vpad;
    public GameObject inventoryUi;
    private Slot[] slots;

    private List<ItemManager> inventory;
    public Transform tf; //slot 부모객체
    private ItemDatabase db;

    public bool showInventory = false;
    // I버튼을 누르면 활성화/비활성화 되는 부울 변수

    private void Awake()
    {
          if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(instance);
        
    }
    // Use this for initialization
    void Start()
    {
        instance = this;
       

        slots = tf.GetComponentsInChildren<Slot>();
        db = FindObjectOfType<ItemDatabase>();
        inventory = new List<ItemManager>();
        inventory.Add(new ItemManager("휴대폰", 1000, "This sword is normal style sword"));
        Debug.Log(db.itemList.Count);

        /*
        db = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        for (int i = 0;8i < db.items.Count; i++)
        // 반복문을 이용하여 전체 인벤토리에 저장토록 합니다.
        {
            if (db.items[i] != null)
            {
                inventory[i] = db.items[i];
                
                // 디비의 아이템칸에 비어있지 않다면, 저장
            }
            else
            {
                // 디비의 아이템칸이 비어있다면 다른 행동을 하도록 유도합니다.

            }
        }*/
    }
    public List<ItemManager> SaveItem()
    {
        return inventory;
    }
    public void LoadItem(List<ItemManager> itemList)
    {
        inventory = itemList;

        
    }

    public void GetItem(int itemID)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].itemID == itemID)
            {
              //  inventory.RemoveAt(i);
                return;
            }
        }
        for (int i = 0; i < db.itemList.Count; i++)
        // 반복문을 이용하여 전체 인벤토리에 저장토록 합니다.
        {
                      
            if (db.itemList[i].itemID == itemID)
            {
                inventory.Add(db.itemList[i]);
                return;
                // 디비의 아이템칸에 비어있지 않다면, 저장
            }
            
        }
    } 
    public void RemoveItem(int itemID)
    {
        for(int i=0; i<inventory.Count; i++)
        {
            if (inventory[i].itemID == itemID)
            {
                inventory.RemoveAt(i);
                return;
            }
        }
    }
    public void RemoveSlot()
    {
        for(int i=0; i<slots.Length; i++)
        {
            slots[i].RemoveItem();
            slots[i].gameObject.SetActive(false);
        }
    }

    public void ShowItem(bool show)
    {
        RemoveSlot();
        if (show)
        {
            
            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].Show();
                slots[i].AddItem(inventory[i]);
                slots[i].gameObject.SetActive(true);
                
            }
        }
    }
    void Update()
    {
        vpad = FindObjectOfType<zFoxVirtualPad>();

        zFOXVPAD_BUTTON vpad_btnA = zFOXVPAD_BUTTON.NON;
        zFOXVPAD_BUTTON vpad_btnB = zFOXVPAD_BUTTON.NON;
        zFOXVPAD_BUTTON vpad_btnX = zFOXVPAD_BUTTON.NON;
        zFOXVPAD_BUTTON vpad_btnY = zFOXVPAD_BUTTON.NON;
        if (vpad != null)
        {
            vpad_btnA = vpad.buttonA;
            vpad_btnB = vpad.buttonB;
            vpad_btnX = vpad.buttonX;
            vpad_btnY = vpad.buttonY;
        }

        if (vpad_btnY == zFOXVPAD_BUTTON.DOWN)
        // 만약 Inventory(I)버튼이 눌리면 아래 내용을 실행합니다.
        {
            showInventory = !showInventory;
            // showInventory 앞에 느낌표는 낫(Not)연산자이며, 참>거짓, 거짓>참으로 바꿔주는 연산자입니다.
            // 누를때마다 참>거짓>참>거짓으로 바뀌겠죠
            
        }
        Time.timeScale = showInventory ? 0 : 1;
        
            inventoryUi.SetActive(showInventory);
            ShowItem(showInventory);       
        
    }
  
}
