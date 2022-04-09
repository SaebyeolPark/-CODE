using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance =null;
    public PlayerMain player;
    public TalkManager talkManager;
    public bool isControl;
    public GameObject talkPanel;
    public Text UITalkText;
    public Text UINameText;
    public Image portraitImg;
    public GameObject scanObject;
    public string scanName;
    public int talkIndex;
    public bool isDialog;
    public bool isData;
    public int playerRepeat=0;
    public int objectRepeat = 0;
    public bool isNurseEvent;
    public int ArtEvent=0;
    public bool isEnding;
    public bool isNarration;
    public int nurseEvent = 0;
    public bool firstCall;
    public int click;
    public bool isNurseKey;
    public bool isArtKey;
    public bool isArtStorageKey;
    public bool isArtEvent;
    public bool isGetSyringe;
    public bool isGetNameplace;
    public bool firstEnterNurseRoom;
    public bool firstEnterArtEventFloor;
    public bool isGameOver;
    public bool end1Floor; //1층 끝
    //2층시작
    public bool secondFloorFirst;
    public bool isDollEvent; //인형스토리이벤트
    public bool isDollEventMain; //인형갇힘
    public bool isGetDoll; //인형주웟니?
    public bool isGetDollEnding; //인형엔딩이벤트

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        scanName = scanObject.name;
        
        isData = true;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);

        Debug.Log(scanName);

        talkPanel.SetActive(isDialog);
    }
    public void Pause()
    {
        talkPanel.SetActive(false);
    }
    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (scanName == "Player")
        {
            scanName = "고은비";

            /*
            if (isNarration == true)
            {
                scanName = "";
            }*/
        }

        if (talkData == null)
        {
            Debug.Log("talkdata null");
            isDialog = false;
            talkIndex = 0;
            talkPanel.SetActive(isDialog);
            isData = false;
            return;
        }

        
        if (isNpc)
        {
            UITalkText.text = talkData.Split(':')[0];
            if (talkData.Split(':')[1].Length!=0)
            {
                portraitImg.sprite = talkManager.GetPortrait(int.Parse(talkData.Split(':')[1]));
                portraitImg.color = new Color(1, 1, 1, 1);
                if (int.Parse(talkData.Split(':')[1]) == 3)
                {
                    scanName = "이지유";
                    //  portraitImg.color = new Color(1, 1, 1, 0);
                }
                else if (int.Parse(talkData.Split(':')[1]) == 8)
                {
                    scanName = "반중앙에서 떠들던 학생들";
                    portraitImg.color = new Color(1, 1, 1, 0);
                }

            }
            else
            {
                portraitImg.color = new Color(1, 1, 1, 0);
                scanName = "";
            }
            /*
            if (isNarration == true)
            {
                portraitImg.color = new Color(1, 1, 1, 0);
            }*/
            UINameText.text = scanName;
        }
        else
        {
            UITalkText.text = talkData.Split(':')[0];
            if (talkData.Split(':')[1].Length != 0)
            {
                portraitImg.sprite = talkManager.GetPortrait(int.Parse(talkData.Split(':')[1]));
                portraitImg.color = new Color(1, 1, 1, 1);
                if (int.Parse(talkData.Split(':')[1]) == 0|| int.Parse(talkData.Split(':')[1]) == 4|| int.Parse(talkData.Split(':')[1]) == 6)
                    scanName = "고은비";
                else if (int.Parse(talkData.Split(':')[1]) == 1)
                {
                    scanName = "교장";
                    portraitImg.color = new Color(1, 1, 1, 0);
                }
                else if (int.Parse(talkData.Split(':')[1]) == 2)
                {
                    scanName = "침대에 누운 학생";
                    portraitImg.color = new Color(1, 1, 1, 0);
                }
                else if (int.Parse(talkData.Split(':')[1]) == 3|| int.Parse(talkData.Split(':')[1]) == 5)
                {
                    scanName = "이지유";
                  //  portraitImg.color = new Color(1, 1, 1, 0);
                }
                else if (int.Parse(talkData.Split(':')[1]) == 7)
                {
                    //scanName = "무언가를 찾는 학생";
                    portraitImg.color = new Color(1, 1, 1, 0);
                }
                else if (int.Parse(talkData.Split(':')[1]) == 8)
                {
                    scanName = "반중앙에서 떠들던 학생들";
                    portraitImg.color = new Color(1, 1, 1, 0);
                }

            }
            else
            {
                portraitImg.color = new Color(1, 1, 1, 0);
                scanName = "";
            }
           
           UINameText.text = scanName;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            click++;
            talkIndex++;
            if (isNpc)
            {
                playerRepeat++;

            }
            else
            {
                objectRepeat++;

            }
        }
        if (talkIndex == 0 && talkData.Split(':')[0].Length == 0)
        {
            talkIndex++;
            if (isNpc)
            {
                playerRepeat++;

            }
            else
            {
                objectRepeat++;

            }
        }
        isDialog = true;
        isData = true;
        //   talkIndex++;
    }
    public void LoadStart()
    {
        StartCoroutine(LoadWaitCoroutine());
    }
    IEnumerator LoadWaitCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

    }
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
    // Start is called before the first frame update
    void Start()
    {
      //  DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
