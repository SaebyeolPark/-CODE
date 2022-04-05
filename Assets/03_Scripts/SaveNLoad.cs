using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveNLoad : MonoBehaviour
{
    [System.Serializable]
    public class Data
    {
        public float playerX;
        public float playerY;
        public float playerZ;

        public float firstEnemyX;
        public float firstEnemyY;
        public float firstEnemyZ;
        
        public List<int> playerItemInventory;

        public string sceneName;

        public int nurseEvent;
        public int artEvent;

        public bool isNurseKey;
        public bool isArtKey;
        public bool isArtStorageKey;
        public bool firstEnterNurseRoom;
        public bool isArtEvent;
        public bool isGetSyringe;
        public bool isGetNameplace;
        public bool firstEnterArtEventFloor;
        public bool isNurseEvent;
        public bool end1Floor;
        public bool isDollEvent;

    }

    private PlayerMain playerMain;
    private Inventory inventory;
    private ItemDatabase itemDatabase;

    public Data data;
    private ChangeName changeName;

    private Vector3 playerVector;
    private Vector3 firstEnemyVector;

    private void Start()
    {
        changeName = FindObjectOfType<ChangeName>();

    }

    public void CallSave(string _name)
    {
       itemDatabase = FindObjectOfType<ItemDatabase>();
        playerMain = FindObjectOfType<PlayerMain>();
        inventory = FindObjectOfType<Inventory>();

        data.playerX = playerMain.transform.position.x;
        data.playerY = playerMain.transform.position.y;
        data.playerZ = playerMain.transform.position.z;

        data.firstEnemyX = FirstEnemyMain.instance.transform.position.x;
        data.firstEnemyY = FirstEnemyMain.instance.transform.position.y;
        data.firstEnemyZ = FirstEnemyMain.instance.transform.position.z;

        data.sceneName = playerMain.currentSceneName;

        data.nurseEvent = GameManager.instance.nurseEvent;
        data.artEvent = GameManager.instance.ArtEvent;
        data.isNurseEvent = GameManager.instance.isNurseEvent;

        data.isArtKey = GameManager.instance.isArtKey;
        data.isNurseKey = GameManager.instance.isNurseKey;
        data.isArtStorageKey = GameManager.instance.isArtStorageKey;
        data.firstEnterNurseRoom = GameManager.instance.firstEnterNurseRoom;
        data.isArtEvent = GameManager.instance.isArtEvent;
        data.isGetNameplace = GameManager.instance.isGetNameplace;
        data.isGetSyringe = GameManager.instance.isGetSyringe;
        data.firstEnterArtEventFloor = GameManager.instance.firstEnterArtEventFloor;
        data.end1Floor = GameManager.instance.end1Floor;
        data.isDollEvent = GameManager.instance.isDollEvent;

        Debug.Log("기초 데이터 성공");

        data.playerItemInventory.Clear();

        List<ItemManager> inventoryList = inventory.SaveItem();
        for(int i=0; i<inventoryList.Count; i++)
        {
            Debug.Log("인벤토리 저장: " + inventoryList[i].itemID);
            data.playerItemInventory.Add(inventoryList[i].itemID);
        }

        BinaryFormatter bf = new BinaryFormatter();
        //  FileStream file = File.Create(Application.dataPath + "/"+_name+".dat");
        FileStream file = File.Create(Application.persistentDataPath + "/" + _name + ".dat"); 
        bf.Serialize(file, data);
        file.Close();

        Debug.Log(Application.dataPath + "의 위치에 저장완료");

        //changeName.ChangeSceneName(data.sceneName);

    }
    public void CallLoad(string _name)
    {
        BinaryFormatter bf = new BinaryFormatter();
      //  FileStream file = File.Open(Application.dataPath + "/" + _name + ".dat", FileMode.Open);
        FileStream file = File.Open(Application.persistentDataPath + "/" + _name + ".dat", FileMode.Open);

        if (file != null && file.Length > 0)
        {
            data = (Data)bf.Deserialize(file);

            itemDatabase = FindObjectOfType<ItemDatabase>();
            playerMain = FindObjectOfType<PlayerMain>();
            inventory = FindObjectOfType<Inventory>();
           
            //씬로드
            playerMain.currentSceneName = data.sceneName;
            SceneManager.LoadScene(data.sceneName);

            playerVector.Set(data.playerX, data.playerY, data.playerZ);
            playerMain.transform.position = playerVector;

            firstEnemyVector.Set(data.firstEnemyX, data.firstEnemyY, data.firstEnemyZ);
            FirstEnemyMain.instance.transform.position = firstEnemyVector;

            GameManager.instance.nurseEvent = data.nurseEvent;
            GameManager.instance.ArtEvent = data.artEvent;
            GameManager.instance.isNurseEvent = data.isNurseEvent;

            GameManager.instance.isNurseKey = data.isNurseKey;
            GameManager.instance.isArtKey = data.isArtKey;
            GameManager.instance.isArtStorageKey = data.isArtStorageKey;
            GameManager.instance.firstEnterNurseRoom = data.firstEnterNurseRoom;
           GameManager.instance.isArtEvent =data.isArtEvent;
            GameManager.instance.isGetNameplace= data.isGetNameplace;
            GameManager.instance.isGetSyringe= data.isGetSyringe;
            GameManager.instance.firstEnterArtEventFloor = data.firstEnterArtEventFloor;
            GameManager.instance.end1Floor = data.end1Floor;
            GameManager.instance.isDollEvent = data.isDollEvent;

            //인벤토리로드
            List<ItemManager> inventoryList = new List<ItemManager>();
            for(int i=0; i<data.playerItemInventory.Count; i++)
            {
                for(int x=0; x<itemDatabase.itemList.Count; x++)
                {
                    if (data.playerItemInventory[i] == itemDatabase.itemList[x].itemID)
                    {
                        inventoryList.Add(itemDatabase.itemList[x]);
                        Debug.Log("인벤토리 로드: " + itemDatabase.itemList[x].itemID);
                        break;
                    }
                }
            }
            inventory.LoadItem(inventoryList);

            Debug.Log("로드완료");

            
        }
        else
        {
            Debug.Log("로드할 수 있는 파일이 없습니다.");
        }

        file.Close();
    }

}
