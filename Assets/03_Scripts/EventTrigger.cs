using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventTrigger : MonoBehaviour
{
    private ObjData objData;

    zFoxVirtualPad vpad;
    public bool isButton = false;
    public bool isTrigger = false;
    public int talkLength;
    public bool isused = false;
    public int button;
    float time;
    bool timerStart;
    public bool isNecessary;
    public bool isEventTriggerOff;
    WindowSound windowSound;

    // public bool isArtEventTriggerOn;

    // Start is called before the first frame update
    void Start()
    {
        objData = FindObjectOfType<ObjData>();

    }

    // Update is called once per frame
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
        //1층 지도
        if (gameObject.name == "1층게시판")
        {
            Inventory.instance.GetItem(1012);

        }
        if (gameObject.name == "waitTrigger")
        {
            if (GameManager.instance.playerRepeat == 4)
            {
                time += Time.deltaTime;
                if (time > 1f)
                {
                    PlayerMain.instance.isButtonDown = false;
                    time = 0;
                }
            }
            if (GameManager.instance.isNurseEvent == true || PlayerMain.instance.isButtonDown == true )
                isused = true;
            else
                isused = false;
        }
        //양호실 시간재기
        if (timerStart == true)
        {
            time += Time.deltaTime;
            if (time > 1f)
            {
                GameManager.instance.isNurseEvent = true;
                GameManager.instance.playerRepeat = 5;
                GameManager.instance.talkIndex = 20;

                timerStart = false;
            //    GameManager.instance.isNurseEvent = true;

            }
        }
       
        if (isused == false)
        {
            if (isTrigger == true)
            {
                if (vpad_btnA == zFOXVPAD_BUTTON.DOWN)
                {
                    
                    GameManager.instance.objectRepeat = 0;
                    GameManager.instance.talkIndex = 0;
                    GameManager.instance.firstCall = true;
                    isButton = true;
                }
                if (isButton == true)
                {                                  
                    GameManager.instance.isControl = false;
                    GameManager.instance.Action(gameObject);
                    // GameManager.instance.talkIndex = 0;
                    if (GameManager.instance.objectRepeat >= talkLength)
                    {
                        GameManager.instance.Pause();
                        //   GameManager.instance.objectRepeat = 0;
                        GameManager.instance.isControl = true;

                        //양호실이벤트 
                        if (PlayerMain.instance.currentSceneName == "NurseRoom" )
                        {
                            NurseRoomEvent();
                        }

                        //미술실이벤트
                        ArtClassEvent();

                        //미술창고이벤트
                        ArtStorageEvent();
                        
                        //2층 인형 스토리이벤트
                        DollEvent();
                        //인형 갇힘
                        DollEventMain();

                        // isTrigger = false;
                        isButton = false;
                        
                        if(PlayerMain.instance.currentSceneName=="1FloorLeft"|| PlayerMain.instance.currentSceneName == "1FloorRight"||gameObject.name=="바구니이벤트"||gameObject.name== "waitTrigger")
                        {
                            //복도 조사는 여러번 할 수 있음
                        }
                        else
                        {
                            isused = true;
                        }
                        

                        //아이템
                        if (gameObject.name == "우측서류")
                        {
                            Inventory.instance.GetItem(1005);
                            GameManager.instance.isNurseKey = true;
                        }else if (gameObject.name == "양호실책상")
                        {
                            Inventory.instance.GetItem(1006);
                            GameManager.instance.isArtKey = true;
                        }

                      //  GameManager.instance.objectRepeat = 0;
                      //  GameManager.instance.talkIndex = 0;

                    }
                   
                }

            }
        }
    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            isTrigger = true;
            if (gameObject.name == "FirstEnterEvent"&&isused==false)
            {
                GameManager.instance.objectRepeat = 0;
                GameManager.instance.talkIndex = 0;

                GameManager.instance.isControl = false;
                GameManager.instance.Action(gameObject);
                // GameManager.instance.talkIndex = 0;
                if (GameManager.instance.objectRepeat >= talkLength)
                {
                    GameManager.instance.Pause();
                    //   GameManager.instance.objectRepeat = 0;
                    GameManager.instance.isControl = true;
                    isused = true;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            isTrigger = false;
        }
    }
    void DollEvent()
    {
        if (gameObject.name == "무언가를 찾는 학생")
        {
            GameManager.instance.isDollEvent = true;

        }
    }
    void DollEventMain()
    {
        if (gameObject.name == "중앙에 몰려있는 학생들")
        {
            GameManager.instance.isDollEventMain = true;
            GameManager.instance.talkIndex = 66;
            GameManager.instance.isControl = false;
            GameManager.instance.isDollEvent = false;
        }
        if (gameObject.name == "곰인형조사")
        {
            GameManager.instance.isGetDoll = true;
            Inventory.instance.GetItem(1013);

        }
        if (gameObject.name == "문조사")
        {
            GameManager.instance.talkIndex = 77;
            GameManager.instance.isControl = false;
            GameManager.instance.playerRepeat = 0;
            Debug.Log("문조사 확인");
            GameManager.instance.isGetDollEnding = true;
        }
        if(gameObject.name=="무언가 찾는 학생")
        {
            GameManager.instance.isGetDollEnding = false;
            SceneManager.LoadScene("2FloorLeftF-2");

        }
    }
   
    void ArtClassEvent()
    {

        if (gameObject.name == "가위이벤트")//가위
        {
            //Inventory.instance.GetItem(1001);
            GameManager.instance.ArtEvent++;
        }
        else if (gameObject.name == "풀이벤트")//풀
        {
            Inventory.instance.GetItem(1002);
            GameManager.instance.ArtEvent++;

        }
        else if (gameObject.name == "가운데신발장이벤트")//이름표
        {
            Inventory.instance.GetItem(1008);
            GameManager.instance.isGetNameplace = true;
            GameManager.instance.ArtEvent++;

        }
        else if (gameObject.name == "왼쪽서랍장이벤트")//주사기
        {
            Inventory.instance.GetItem(1009);

            GameManager.instance.isGetSyringe = true;
            GameManager.instance.ArtEvent++;

        }
        else if (gameObject.name == "핏자국이벤트")//주사기피
        {
            Inventory.instance.RemoveItem(1009);
            Inventory.instance.GetItem(1010);
            GameManager.instance.isGetSyringe = true;
            GameManager.instance.ArtEvent++;

        }
        else if (gameObject.name == "냉장고이벤트")//물
        {
            GameManager.instance.ArtEvent++;
            Inventory.instance.GetItem(1011);

        }
        else if (gameObject.name == "종이")//물
        {
            GameManager.instance.isArtEvent = true;
            Inventory.instance.GetItem(1003);

        }
        else if (gameObject.name == "미술실칠판")//미술실창고열쇠
        {
            Inventory.instance.GetItem(1007);
            GameManager.instance.isArtStorageKey = true;

        }

    }

    void ArtStorageEvent()
    {
        if (GameManager.instance.ArtEvent >= 6)
        {
            if (gameObject.name == "바구니이벤트")
            {
                Inventory.instance.RemoveItem(1001);
                Inventory.instance.RemoveItem(1002);
                Inventory.instance.RemoveItem(1003);
                Inventory.instance.RemoveItem(1004);
                Inventory.instance.RemoveItem(1005);
                Inventory.instance.RemoveItem(1006);
                Inventory.instance.RemoveItem(1007);
                Inventory.instance.RemoveItem(1008);
                Inventory.instance.RemoveItem(1009);
                Inventory.instance.RemoveItem(1010);
                Inventory.instance.RemoveItem(1011);

                GameManager.instance.isEnding = true;
                GameManager.instance.isControl = false;
                GameManager.instance.talkIndex = 24;
                GameManager.instance.playerRepeat = 0;
                GameManager.instance.ArtEvent = 0;
             //   GameManager.instance.Action(PlayerMain.instance.gameObject);
                FirstEnemyMain.instance.gameObject.SetActive(false);
            }
        }
    }

    void NurseRoomEvent()
    {
        
            if (isNecessary == true)
            {
                GameManager.instance.nurseEvent++;
            }
            if (GameManager.instance.nurseEvent >= 7)
            {
            windowSound = GameObject.Find("soundWindow").GetComponent<WindowSound>();
            //오디오
            windowSound.Play();
            GameManager.instance.isControl = false;
                //GameManager.instance.playerRepeat = 0;

                CameraMove.instance.VibrateForTime(0.7f);
                GameManager.instance.nurseEvent = 0;
                timerStart = true;

            }


        
    }
}
