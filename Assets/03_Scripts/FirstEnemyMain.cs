using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FirstEnemyMain : MonoBehaviour
{
    public static FirstEnemyMain instance = null;

    Animator animator;

    Transform playerTr;
    public float speed = 1.5f;
   public float timer;
    public float waitingTime;
    float xDir;
    float yDir;
    public bool isSceneLoad=false;
    public Sprite npcSprite;
    public GameObject deadEnding;

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
    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
       // AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        timer = 0;
        waitingTime = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameOver == false)
        {

            if (GameManager.instance.isControl == true || PlayerMain.instance.isOpeningEnenmy == true)
            {
                Move();

            }
            else
            {
                if (GameManager.instance.isEnding == true && GameManager.instance.playerRepeat == 33)
                {
                    GetComponent<SpriteRenderer>().sprite = npcSprite;
                }
                animator.speed = 0;
                /*
                if (PlayerMain.instance.isOpeningEnenmy == true)
                {
                    PlayerMain.instance.isOpeningEnenmy = false;
                    GameManager.instance.repeat = 12;
                    transform.Translate(-1, 0, 0);
                }*/
            }
        }

        if (deadEnding.gameObject.activeSelf==true)
        {
            Debug.Log("ENTERED ENEMY deadending true");

            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("ENTERED ENEMY click");

                deadEnding.gameObject.SetActive(false);
                SceneManager.LoadScene("Menu");
                CameraMove.instance.gameObject.transform.position = new Vector3(0, 0, -10);
                // menuController.panel.SetActive(true);
                //   GameManager.instance.isControl = false;
                // GameManager.instance.isEnding = false;
                PlayerMain.instance.gameObject.transform.position = new Vector3(8, 3, 0);
                //   transform.rotation = Quaternion.Euler(new Vector3(00, 0, 0));
                transform.position = new Vector3(10, -4, 0);
                GameManager.instance.isGameOver = false;

                //  FirstEnemyMain.instance.gameObject.SetActive(false);
            }
        }
    }
     void Move()
    {
        xDir = playerTr.position.x - transform.position.x;
        yDir = playerTr.position.y - transform.position.y;
        //쫓기
        if (playerTr != null)
        {
            timer += Time.deltaTime;
            animator.speed = 0;
            if(PlayerMain.instance.isOpeningEnenmy == true)
            {
                waitingTime = 1.3f;
            }
            if (timer > waitingTime)
            {
                GetComponent<AudioSource>().Play();

                if (Mathf.Abs(xDir) < 1 && Mathf.Abs(yDir) < 1f)
                {
                    transform.position = playerTr.position;
                }
                else
                {
                    animator.speed = 1;
                    transform.position = Vector3.Lerp(transform.position, playerTr.position, Time.deltaTime * speed);
                    if (Mathf.Abs(xDir) > Mathf.Abs(yDir))
                    {
                        if (xDir > 0)
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 90);
                        }
                        else
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 270);
                        }
                    }
                    else
                    {
                        if (yDir > 0)
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 180);
                        }
                        else
                        {
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                        }
                    }
                }
                if (timer > waitingTime + 0.3)
                {
                   // GetComponent<AudioSource>().Stop();
                    timer = 0;
                }
                   
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerTrigger")
        {
          //  Time.timeScale = 0;
            gameObject.transform.position = collision.gameObject.transform.position;
            GameManager.instance.isControl = false;
            GameManager.instance.isGameOver = true;
//move menu 
            deadEnding.gameObject.SetActive(true);
           /* if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log("ENTERED ENEMY click");

                deadEnding.gameObject.SetActive(false);
                SceneManager.LoadScene("Menu");
                // menuController.panel.SetActive(true);
             //   GameManager.instance.isControl = false;
               // GameManager.instance.isEnding = false;
                  PlayerMain.instance.gameObject.transform.position = new Vector3(8, 3, 0);
             //   transform.rotation = Quaternion.Euler(new Vector3(00, 0, 0));
                  transform.position = new Vector3(10, -3, 0);
              //  FirstEnemyMain.instance.gameObject.SetActive(false);
            }*/
        }
    }

    IEnumerator EnemmyNext()
    {
        Debug.Log("코루틴 실행");
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(true);
        Debug.Log("코루틴 실행완료");
    }
}
