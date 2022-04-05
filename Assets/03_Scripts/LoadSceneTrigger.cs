using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class LoadSceneTrigger : MonoBehaviour
{
    zFoxVirtualPad vpad;
    public bool isNurseRoom;
    public bool isArtClass;
    public bool isArtStorage;
    public string scene;
    public Vector3 playerVec;
    public Vector3 FirstEnemyVec;
    public bool onTrigger = false;
    float time = 0;
    public bool isHallway=false;
    public bool isTouch = false;
    bool isLocked = false;
    bool isButton = false;
    public int talkLength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //잠김검사
        if (isNurseRoom == true && GameManager.instance.isNurseKey == false)
            isLocked = true;
        else if (isArtClass == true && GameManager.instance.isArtKey == false)
            isLocked = true;
        else if (isArtStorage == true && GameManager.instance.isArtStorageKey == false)
            isLocked = true;
        else
            isLocked = false;

        if (onTrigger == true)
        {
            time += Time.deltaTime;
            Debug.Log(time);
        }
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

        if (isTouch == true)
        {
            if (vpad_btnA == zFOXVPAD_BUTTON.DOWN)
            {
                GameManager.instance.objectRepeat = 0;
                GameManager.instance.talkIndex = 0;
                isButton = true;
            }
            if (isButton == true)
            {
                if (isLocked == true)
                {                  
                    
                        GameManager.instance.isControl = false;
                        GameManager.instance.Action(gameObject);
                        // GameManager.instance.talkIndex = 0;
                        if (GameManager.instance.objectRepeat >= talkLength)
                        {
                            GameManager.instance.Pause();
                            //   GameManager.instance.objectRepeat = 0;
                            GameManager.instance.isControl = true;


                            // isTrigger = false;
                            isButton = false;                           
                        }

                    }
                else
                {
                    MoveObject();
                    isButton = false;
                  
                }
            }
                

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            if (isHallway == true)
            {
                MoveObject();
            }
            else
            {
                isTouch = true;

            }
            if (isNurseRoom == true)
            {
                GameManager.instance.playerRepeat = 0;
            }
            
            //CancelInvoke("MoveObject");
            //Invoke("MoveObject", 3.0f);
           // MoveObject();
           // StartCoroutine(WaitEnter());

            //   Invoke("MoveObject", 5f);
            //  yield WaitForSeconds(2);
            /*
            while (time > 2)
            {
                FirstEnemyMain.instance.transform.position = FirstEnemyVec;
                FirstEnemyMain.instance.gameObject.SetActive(true);
                if (time > 3)
                {
                    time = 0;
                    break;
                    
                }
                
            }

            onTrigger = false;
        */
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTouch = false;

    }

    void MoveObject()
    {
        if (PlayerMain.instance.currentSceneName == "NurseRoom" && GameManager.instance.isNurseEvent == false)
        {
            return;
        }
        onTrigger = true;

        SceneManager.LoadScene(scene);
        Debug.Log(scene);
        PlayerMain.instance.transform.position = playerVec;

        //1층만...
        if (GameManager.instance.end1Floor == false)
        {
            FirstEnemyMain.instance.gameObject.SetActive(false);
            FirstEnemyMain.instance.transform.position = FirstEnemyVec;
            if (isNurseRoom == false || GameManager.instance.firstEnterNurseRoom == true)
            {
                FirstEnemyMain.instance.isSceneLoad = true;
            }
            else
            {

                GameManager.instance.isNurseEvent = true;

                GameManager.instance.isControl = false;
                CameraMove.instance.transform.position = new Vector3(0, 0, CameraMove.instance.transform.position.z);
                GameManager.instance.talkIndex = 16;
            }
        }

       
      
        
    }
    IEnumerator EnemmyNext()
    {
        Debug.Log("코루틴 실행");
        yield return new WaitForSeconds(2f);
        FirstEnemyMain.instance.gameObject.SetActive(true);
        FirstEnemyMain.instance.transform.position = FirstEnemyVec;
    }
    
}
