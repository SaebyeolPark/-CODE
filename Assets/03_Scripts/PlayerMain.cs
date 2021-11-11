using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMain : MonoBehaviour
{
    private SaveNLoad saveNLoad;
    zFoxVirtualPad vpad;
    MenuController menuController;

    //public float xMax, xMin, yMax, yMin;
    private Quaternion Right = Quaternion.identity;
    public GameObject ziyu;
    public GameObject saveNLoadPanel;
    public static PlayerMain instance = null;
    public bool isOpeningEnenmy = false;
    public bool isOpening=false;
    public bool isEvent = false;
    public float hor = 0;
    public float ver = 0;
    public float speed = 2.5f;
    public float timer =0;
    //  float detect_range = 1.5f;
    public string currentSceneName;
    public bool isEndingCamera;
    public bool isOpeningCamera;
    private bool isArtTrigger;
    public bool isPressed;
    public bool isButtonDown;
    public GameObject nameplate;
    public GameObject endingIllu;
    GameObject scanObject;

    NpcMove npcMove;

    Rigidbody2D rigid;
    Animator animator;
    SpriteRenderer spriteRenderer;
    LockedSound lockedSound;
    GlassSound glassSound;
    void Awake()
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
     //   DontDestroyOnLoad(gameObject);

    //    npcMove = GameObject.Find("NPC").GetComponent<NpcMove>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        saveNLoad = FindObjectOfType<SaveNLoad>();
        //   vpad = FindObjectOfType<zFoxVirtualPad>();
        currentSceneName = SceneManager.GetActiveScene().name;
        menuController = FindObjectOfType<MenuController>();

        if (currentSceneName == "5Floor")
        {
            isOpening = true;
        }
        else
        {
            isOpening = false;
        }
           
        Debug.Log("start");
    }
    /*
    private void FixedUpdate()
    {
        //조사액션
        Debug.DrawRay(rigid.position, new Vector3(direction * detect_range, 0, 0), new Color(0, 0, 1));

        //Layer가 object인 물체만 감지
        RaycastHit2D rayHit_detect = Physics2D.Raycast(rigid.position, new Vector3(direction, 0, 0), detect_range,
            LayerMask.GetMask("Object"));
        //감지되면 scanObject에 저장
        if(rayHit_detect.collider != null)
        {
            scanObject = rayHit_detect.collider.gameObject;
            Debug.Log(scanObject.name);
        }
        else
        {
            scanObject = null;
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        npcMove = FindObjectOfType<NpcMove>();

        vpad = FindObjectOfType<zFoxVirtualPad>();
        //Debug.Log(vpad);

        currentSceneName = SceneManager.GetActiveScene().name;

        if(currentSceneName=="NurseRoom")
            Inventory.instance.RemoveItem(1005);
        else if(currentSceneName == "ArtClassRoom")
            Inventory.instance.RemoveItem(1006);


        //가상패드 
        float vpad_vertical = 0.0f;
        float vpad_horizontal = 0.0f;
        zFOXVPAD_BUTTON vpad_btnA = zFOXVPAD_BUTTON.NON;
        zFOXVPAD_BUTTON vpad_btnB = zFOXVPAD_BUTTON.NON;
        zFOXVPAD_BUTTON vpad_btnX = zFOXVPAD_BUTTON.NON;
        zFOXVPAD_BUTTON vpad_btnY = zFOXVPAD_BUTTON.NON;
        if (vpad != null)
        {
            vpad_vertical = vpad.vertical;
            vpad_horizontal = vpad.horizontal;
            vpad_btnA = vpad.buttonA;
            vpad_btnB = vpad.buttonB;
            vpad_btnX = vpad.buttonX;
            vpad_btnY = vpad.buttonY;
        }

        //패드처리
        float vpadHorMv = vpad_horizontal;
        float vpadverMv = vpad_vertical;

        vpadHorMv = Mathf.Pow(Mathf.Abs(vpadHorMv), 1.5f) * Mathf.Sign(vpadHorMv);
        vpadverMv = Mathf.Pow(Mathf.Abs(vpadverMv), 1.5f) * Mathf.Sign(vpadverMv);


        if (vpad_btnA == zFOXVPAD_BUTTON.DOWN && isPressed == false)
        {
            //saveNLoad.CallSave();
          //  saveNLoadPanel.SetActive(true);
            vpad_btnA = zFOXVPAD_BUTTON.NON;
           // GameManager.instance.isControl = false;

        }
        if (vpad_btnB == zFOXVPAD_BUTTON.DOWN)
        {
            //     saveNLoad.CallLoad();

        }
        //이벤트
        if (GameManager.instance.isControl == false)
        {
            animator.speed = 0;

            if (isOpening == true)
            {
                OpeningMove();
            }
            else if (GameManager.instance.isNurseEvent == true&&GameManager.instance.firstEnterNurseRoom==false)
            {
                NurseEventMove();
            } else if (GameManager.instance.isEnding == true)
            {
                EndingMove();
            } else if (isArtTrigger==true)
            {
                ArtEventMove();
            }
        }
 //move
        else if (GameManager.instance.isControl == true)
        {
            // Move(); 
            
            //키보드
            hor = Input.GetAxisRaw("Horizontal");
            ver = Input.GetAxisRaw("Vertical");

          
            //   playerCtrl.ActionMove(joyMv + vpadMv);

            //겹치는거 방지
            if (Mathf.Abs(hor + vpadHorMv) > Mathf.Abs(ver + vpadverMv))
            {
                ver = 0;
                vpadverMv = 0;
            }
            else
            {
                hor = 0;
                vpadHorMv = 0;
            }

            //임시방편
            transform.Translate((hor + vpadHorMv) * speed * Time.deltaTime, (ver + vpadverMv) * speed * Time.deltaTime, transform.position.z);

            if (hor == -1 || vpadHorMv == -1)
            {
                animator.speed = 1;
               
                animator.SetBool("isLeft", true);
                animator.SetBool("isRight", false);
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
            }
            else if (hor == 1 || vpadHorMv == 1)
            {
                animator.speed = 1;

                animator.SetBool("isRight", true);
                animator.SetBool("isLeft", false);
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
            }
            else if (ver == -1 || vpadverMv == -1)
            {
                animator.speed = 1;

                animator.SetBool("isFront", true);
                animator.SetBool("isRight", false);
                animator.SetBool("isBack", false);
                animator.SetBool("isLeft", false);
            }
            else if (ver == 1 || vpadverMv == 1)
            {
                animator.speed = 1;

                animator.SetBool("isBack", true);
                animator.SetBool("isRight", false);
                animator.SetBool("isLeft", false);
                animator.SetBool("isFront", false);
            }
            else
            {
                animator.speed = 0;
            }
         
        }
        // transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y, yMin, yMax), transform.position.z);

        //지유 setactive
    //    if (currentSceneName == "2FloorCenter" && GameManager.instance.isEnding == true)
        //{
//            ziyu.SetActive(true);
        //}

    }
    void OpeningMove()
    {
        //5층 오프닝
        if (currentSceneName == "5Floor")
        {
            if (transform.position.x <= 0.6f)
            {
                if (GameManager.instance.playerRepeat == 3)
                {
                   
                    GameManager.instance.Pause();
                    npcMove.FiveFloor();
                    //  GameManager.instance.repeat = 4;
                }
                else if (GameManager.instance.playerRepeat == 6)
                {
                    GameManager.instance.Pause();
                    npcMove.FiveFloor();
                    //  GameManager.instance.repeat = 4;
                }
                else if (GameManager.instance.playerRepeat == 8)
                {
                    GameManager.instance.Pause();
                    animator.speed = 1;
                    animator.SetBool("isLeft", true);
                    animator.SetBool("isRight", false);
                    animator.SetBool("isBack", false);
                    animator.SetBool("isFront", false);
                    transform.Translate(3 * Time.deltaTime * Vector2.left);

                    if (transform.position.x < -4)
                    {
                        SceneManager.LoadScene("1FloorCenter");
                        transform.position = new Vector3(9f, 0, 0);
                        CameraMove.instance.gameObject.transform.position = new Vector3(7.0f, 0, -10);
                        
                        isOpeningCamera = true;

                    }
                    // Inventory.instance.GetItem(1002);
                    //  GameManager.instance.isControl = true;
                    // SceneManager.LoadScene("1Floor");
                }
                else
                {
                    animator.speed = 0;
                    animator.SetBool("isFront", true);
                    animator.SetBool("isRight", false);
                    animator.SetBool("isBack", false);
                    animator.SetBool("isLeft", false);
                    GameManager.instance.isDialog = true;
                    GameManager.instance.Action(gameObject);
                }
                
            }
            else
            {
                animator.speed = 1;
                animator.SetBool("isLeft", true);
                animator.SetBool("isRight", false);
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
                transform.Translate(speed * Time.deltaTime * Vector2.left);
            }
        }else if (currentSceneName == "1FloorCenter") //1층오프닝
        {
           
                animator.speed = 0;
                GameManager.instance.Action(gameObject);
                //                
                if (GameManager.instance.playerRepeat == 8)
                {
                GameManager.instance.Pause();
                isOpeningCamera = false;
                animator.speed = 1;
                    animator.SetBool("isLeft", true);
                    animator.SetBool("isRight", false);
                    animator.SetBool("isBack", false);
                    animator.SetBool("isFront", false);
                    transform.Translate(3 * Time.deltaTime * Vector2.left);
                    FirstEnemyMain.instance.transform.position = new Vector3(-0.5f, 0, 0);
                    nameplate.transform.position = new Vector3(8f, -1f, 0);
                    nameplate.SetActive(true);

                    if(transform.position.x<8)
                        GameManager.instance.playerRepeat = 9;
                }
                else if(GameManager.instance.playerRepeat == 10)
                {
                    GameManager.instance.Pause();

                    timer += Time.deltaTime;
                    if (timer > 1.0f)
                    {
                        GameManager.instance.playerRepeat = 11;
                        nameplate.SetActive(false);
                        GameManager.instance.isNarration = true;
                        Inventory.instance.GetItem(1004);
                        timer = 0;
                    }

                }else if (GameManager.instance.playerRepeat == 12)
                 {
                    GameManager.instance.Pause();
                isOpeningCamera = true;
                       GameManager.instance.isNarration = false;
                    animator.speed = 1;
                    animator.SetBool("isLeft", true);
                    animator.SetBool("isRight", false);
                    animator.SetBool("isBack", false);
                    animator.SetBool("isFront", false);
                    transform.Translate(3 * Time.deltaTime * Vector2.left);
                    if (transform.position.x < 2f)
                    {
                        GameManager.instance.playerRepeat = 13;
                    }
                }
                else if (GameManager.instance.playerRepeat == 14)
                {
                     GameManager.instance.isNarration = true;
                }/* else if (GameManager.instance.playerRepeat == 15)
                {
                    GameManager.instance.isNarration = false;
                }*/
                else if (GameManager.instance.playerRepeat == 16)
                {

                    GameManager.instance.Pause();
                    isOpeningEnenmy = true;
                    timer += Time.deltaTime;
                    if (timer > 2.0f)
                    {
                        isOpeningEnenmy = false;
                        GameManager.instance.playerRepeat = 17;
                        timer = 0;
                    }
                } else if (GameManager.instance.playerRepeat == 20)
                {
                   GameManager.instance.isNarration = true;
                }
                else if (GameManager.instance.playerRepeat == 22)
                {
                    GameManager.instance.Pause();
                    GameManager.instance.isControl = true;
                    isOpening = false;
                    GameManager.instance.talkIndex = 0;
                   GameManager.instance.isNarration = false;
                }                   
            }       
               
    }

    void NurseEventMove()
    {
        lockedSound = GameObject.Find("soundLocked").GetComponent<LockedSound>();
        glassSound = GameObject.Find("soundGlass").GetComponent<GlassSound>();

        Debug.Log("nurseevent");
        GameManager.instance.Action(gameObject);
        if (GameManager.instance.playerRepeat == 2)
        {
            lockedSound.Play();
            isButtonDown = true;
        }
        else if (GameManager.instance.playerRepeat == 4)
        {
            GameManager.instance.Pause();
            GameManager.instance.isControl = true;
            GameManager.instance.isNurseEvent = false;
            
           
          //  GameManager.instance.playerRepeat
        }else if(GameManager.instance.playerRepeat == 5) {
            GameManager.instance.Pause();
           
            if (transform.position.y > -1.5f)
            {

                animator.speed = 1;
                animator.SetBool("isLeft", false);
                animator.SetBool("isRight", false);
                animator.SetBool("isBack", true);
                animator.SetBool("isFront", false);
                transform.Translate(2 * Time.deltaTime * Vector2.down);
            }
            else
            {
                animator.speed = 0;
                animator.SetBool("isLeft", false);
                animator.SetBool("isRight", false);
                animator.SetBool("isBack", true);
                animator.SetBool("isFront", false);
            }
            timer += Time.deltaTime;
            if (timer > 1.5f)
            {
                    GameManager.instance.playerRepeat = 6;
                    timer = 0;
                
                
            }
        }else if (GameManager.instance.playerRepeat == 6)
        {
            GameManager.instance.Pause();
            //오디오
            glassSound.Play();
            CameraMove.instance.VibrateForTime(0.2f);
            GameManager.instance.Pause();
           // FirstEnemyMain.instance.gameObject.SetActive(true);
            SceneEnemyActive.instance.firstEnemy.SetActive(true);
         //   GameManager.instance.playerRepeat = 7;
        }
        else if (GameManager.instance.playerRepeat == 8)
        {
            GameManager.instance.firstEnterNurseRoom = true;
            GameManager.instance.Pause();
            GameManager.instance.isControl = true;
           // GameManager.instance.isNurseEvent = false;
        }

    }

    void ArtEventMove()
    {
        Debug.Log("ENTER ART TRIGGER");
        GameManager.instance.Action(gameObject);
        if (GameManager.instance.playerRepeat == 3)
        {
            GameManager.instance.Pause();
            GameManager.instance.firstEnterArtEventFloor = true;
           // GameManager.instance.playerRepeat = 0;
            GameManager.instance.isControl = true;
            isArtTrigger = false;
        }
            
    }

    void EndingMove()
    {

        if (currentSceneName == "ArtStorage")
        {
            if (GameManager.instance.playerRepeat == 9)
            {
                FirstEnemyMain.instance.gameObject.transform.position = new Vector3(3f, -1.5f, 0);
                SceneEnemyActive.instance.firstEnemy.SetActive(true);
                GameManager.instance.Pause();
                timer += Time.deltaTime;
                isOpeningEnenmy = true;
                if (timer > 1.5f)
                {
                    isOpeningEnenmy = false;
                    GameManager.instance.playerRepeat = 10;
                    timer = 0;
                }
            }
            else if (GameManager.instance.playerRepeat == 14)
            {
                GameManager.instance.Pause();
                if (transform.position.y < 0.7f)
                {
                    animator.speed = 1;
                    animator.SetBool("isLeft", false);
                    animator.SetBool("isRight", false);
                    animator.SetBool("isBack", true);
                    animator.SetBool("isFront", false);
                    transform.Translate(3 * Time.deltaTime * Vector2.up);
                }
                else if (transform.position.x < 2.5f)
                {
                    animator.speed = 1;
                    animator.SetBool("isLeft", false);
                    animator.SetBool("isRight", true);
                    animator.SetBool("isBack", false);
                    animator.SetBool("isFront", false);
                    transform.Translate(3 * Time.deltaTime * Vector2.right);
                }
                else if (transform.position.y < 2)
                {
                    animator.speed = 1;
                    animator.SetBool("isLeft", false);
                    animator.SetBool("isRight", false);
                    animator.SetBool("isBack", true);
                    animator.SetBool("isFront", false);
                    transform.Translate(3 * Time.deltaTime * Vector2.up);
                }
                else
                {
                    SceneManager.LoadScene("1FloorCenter");
                    // GameManager.instance.playerRepeat = 9;
                    transform.position = new Vector3(-14, -3, 0);
                    FirstEnemyMain.instance.gameObject.transform.position = new Vector3(-25f, -3, 0);
                    FirstEnemyMain.instance.gameObject.SetActive(false);
                }
            }
            else
            {
                GameManager.instance.Action(gameObject);

            }
        }
        else if (currentSceneName == "1FloorCenter")
        {
            GameManager.instance.Action(gameObject);


            if (GameManager.instance.playerRepeat == 14)
            {
                isOpeningEnenmy = true; 
                SceneEnemyActive.instance.firstEnemy.SetActive(true);

                GameManager.instance.Pause();
                isEvent = true;
                if (transform.position.x < -10)
                {
                    animator.speed = 1;
                    animator.SetBool("isLeft", false);
                    animator.SetBool("isRight", true);
                    animator.SetBool("isBack", false);
                    animator.SetBool("isFront", false);
                    transform.Translate(2.5f * Time.deltaTime * Vector2.right);
                }else if (transform.position.y < 0.2f)
                {
                    animator.speed = 1;
                    animator.SetBool("isLeft", false);
                    animator.SetBool("isRight", false);
                    animator.SetBool("isBack", true);
                    animator.SetBool("isFront", false);
                    transform.Translate(2.5f * Time.deltaTime * Vector2.up);
                }
                else if (transform.position.x < -0.3f)
                {
                    animator.speed = 1;
                    animator.SetBool("isLeft", false);
                    animator.SetBool("isRight", true);
                    animator.SetBool("isBack", false);
                    animator.SetBool("isFront", false);
                    transform.Translate(2.5f * Time.deltaTime * Vector2.right);
                }
                else
                {
                    Debug.Log("돌아라");
                    // Right.eulerAngles = new Vector3(0, 0, 90);
                    transform.rotation = Quaternion.Euler(new Vector3(00, 0, 90));
                    timer += Time.deltaTime;
                    if (timer > 1.2f)
                    {
                        isEndingCamera = true;

                        GameManager.instance.playerRepeat = 15;
                        isEvent = false;
                        isOpeningEnenmy = false;

                        timer = 0;
                    }

                    //transform.rotation = Quaternion.Slerp(transform.rotation, Right, Time.deltaTime * 5.0f);
                    //nameplate.SetActive(true);

                    //  nameplate.transform.position = new Vector3(-6f, -1f, 0);
                                   
                }
            }else if (GameManager.instance.playerRepeat == 18)
            {
                GameManager.instance.Pause();
                timer += Time.deltaTime;
                isOpeningEnenmy = true;
                if (timer > 1.5f)
                {
                    isOpeningEnenmy = false;
                    GameManager.instance.playerRepeat = 19;
                    timer = 0;
                }
            }
            else if (GameManager.instance.playerRepeat == 21)
            {
                GameManager.instance.Pause();
                timer += Time.deltaTime;
                isOpeningEnenmy = true;
                if (timer > 1.5f)
                {
                    isOpeningEnenmy = false;
                    GameManager.instance.playerRepeat = 22;
                    timer = 0;
                }
            }
            else if (GameManager.instance.playerRepeat == 28)
            {
                GameManager.instance.Pause();
                timer += Time.deltaTime;
                isOpeningEnenmy = true;
                if (timer > 1.5f)
                {
                    isOpeningEnenmy = false;
                    GameManager.instance.playerRepeat = 29;
                    timer = 0;
                }
            }
            else if (GameManager.instance.playerRepeat == 33)
            {
                GameManager.instance.Pause();
                //  nameplate.SetActive(true);
                //  nameplate.transform.position = new Vector3(FirstEnemyMain.instance.transform.position.x+3.5f, -1f, 0);
                transform.rotation = Quaternion.Euler(new Vector3(00, 0, 0));
                endingIllu.transform.position = CameraMove.instance.gameObject.transform.position;
                endingIllu.transform.position = new Vector3(endingIllu.transform.position.x, endingIllu.transform.position.y, 0);
                endingIllu.SetActive(true);
                if (Input.GetButtonDown("Fire1"))
                {
                    GameManager.instance.playerRepeat = 34;

                }
            }
            else if (GameManager.instance.playerRepeat == 35)
            {
                GameManager.instance.Pause();
                

                    if (Input.GetButtonDown("Fire1"))
                    {
                        SceneManager.LoadScene("2FloorCenter");
                        endingIllu.SetActive(false);
                       // menuController.panel.SetActive(true);
                        GameManager.instance.isControl = false;
                        isEndingCamera = false;
                       GameManager.instance.isEnding = false;
                      //  transform.position = new Vector3(8, 3, 0);
                        transform.rotation = Quaternion.Euler(new Vector3(00, 0, 0));
                    //  FirstEnemyMain.instance.transform.position = new Vector3(10, -3, 0);
                    FirstEnemyMain.instance.gameObject.SetActive(false);
                    }
                    // GameManager.instance.playerRepeat = 18;
                }

            }
           
        
        }

        
    
    void Move()
    {

        //키보드
         hor = Input.GetAxisRaw("Horizontal");
         ver = Input.GetAxisRaw("Vertical");

        //가상패드 
        float vpad_vertical = 0.0f;
        float vpad_horizontal = 0.0f;
        zFOXVPAD_BUTTON vpad_btnA = zFOXVPAD_BUTTON.NON;
        zFOXVPAD_BUTTON vpad_btnB = zFOXVPAD_BUTTON.NON;
        zFOXVPAD_BUTTON vpad_btnX = zFOXVPAD_BUTTON.NON;
        zFOXVPAD_BUTTON vpad_btnY = zFOXVPAD_BUTTON.NON;
        if (vpad != null)
        {
            vpad_vertical = vpad.vertical;
            vpad_horizontal = vpad.horizontal;
            vpad_btnA = vpad.buttonA;
            vpad_btnB = vpad.buttonB;
            vpad_btnX = vpad.buttonX;
            vpad_btnY = vpad.buttonY;
        }

        //패드처리
        float vpadHorMv = vpad_horizontal;
        float vpadverMv = vpad_vertical;

        vpadHorMv = Mathf.Pow(Mathf.Abs(vpadHorMv), 1.5f) * Mathf.Sign(vpadHorMv);
        vpadverMv = Mathf.Pow(Mathf.Abs(vpadverMv), 1.5f) * Mathf.Sign(vpadverMv);
       

        if (vpad_btnX == zFOXVPAD_BUTTON.DOWN && isPressed==false)
        {         
            //saveNLoad.CallSave();
            saveNLoadPanel.SetActive(true);
           vpad_btnX = zFOXVPAD_BUTTON.NON;
            GameManager.instance.isControl = false;
           
        }
        if (vpad_btnB == zFOXVPAD_BUTTON.DOWN)
        {
       //     saveNLoad.CallLoad();

        }
        //   playerCtrl.ActionMove(joyMv + vpadMv);

        //겹치는거 방지
        if (Mathf.Abs(hor+ vpadHorMv )> Mathf.Abs(ver + vpadverMv))
        {
            ver = 0;
            vpadverMv = 0;
        }
        else
        {
            hor = 0;
            vpadHorMv = 0;
        }

        //임시방편
        transform.Translate((hor + vpadHorMv) * speed * Time.deltaTime, (ver+ vpadverMv) * speed * Time.deltaTime, transform.position.z);

           if (hor == -1 || vpadHorMv == -1)
            {
            animator.speed = 1;

            animator.SetBool("isLeft", true);
                animator.SetBool("isRight", false);
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
            }
            else if (hor == 1 || vpadHorMv == 1)
            {
            animator.speed = 1;

            animator.SetBool("isRight", true);
                animator.SetBool("isLeft", false);
                animator.SetBool("isBack", false);
                animator.SetBool("isFront", false);
            }
            else if (ver == -1 || vpadverMv == -1)
        {
            animator.speed = 1;

            animator.SetBool("isFront", true);
                animator.SetBool("isRight", false);
                animator.SetBool("isBack", false);
                animator.SetBool("isLeft", false);
            }
            else if (ver == 1 || vpadverMv == 1)
        {
            animator.speed = 1;

            animator.SetBool("isBack", true);
                animator.SetBool("isRight", false);
                animator.SetBool("isLeft", false);
                animator.SetBool("isFront", false);
            } else
             {
            animator.speed = 0;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EventTrigger")
        {
            if (GameManager.instance.isArtEvent == true && GameManager.instance.firstEnterArtEventFloor == false)
            {
                isArtTrigger = true;
                GameManager.instance.playerRepeat = 0;
                GameManager.instance.isControl = false;
                GameManager.instance.talkIndex = 21;
            }
        }
    }
}
