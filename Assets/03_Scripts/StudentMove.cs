using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudentMove : MonoBehaviour
{
    public float hor = 0;
    public float ver = 0;
    public float speed = 1.5f;
    public float timer = 0;
    public bool isPressed;
    public float waitingTime;
    float xDir;
    float yDir;
    Transform playerTr;
    zFoxVirtualPad vpad;
    Rigidbody2D rigid;
    //  Animator animator;
    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
        // animator = GetComponent<Animator>();
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        timer = 0;
        // waitingTime = 2;

        //  animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
        //  saveNLoad = FindObjectOfType<SaveNLoad>();
        //   vpad = FindObjectOfType<zFoxVirtualPad>();
        //    currentSceneName = SceneManager.GetActiveScene().name;
        // menuController = FindObjectOfType<MenuController>();
    }
    /*
    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(playerTr.position.x + 1.0f, playerTr.position.y + 1.0f, 0);
        // npcMove = FindObjectOfType<NpcMove>();

        vpad = FindObjectOfType<zFoxVirtualPad>();
        //Debug.Log(vpad);   

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




        if (GameManager.instance.isControl == false)
        {
            animator.speed = 0;
        }
        else if (GameManager.instance.isControl == true)
        {

            //  Move();

            hor = Input.GetAxisRaw("Horizontal");
            ver = Input.GetAxisRaw("Vertical");
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
         //   transform.Translate((hor + vpadHorMv) * speed * Time.deltaTime, (ver + vpadverMv) * speed * Time.deltaTime, transform.position.z);

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
    }*/
    public void LeftMove()
    {
        Animator animator;
        animator = GetComponent<Animator>();

        animator.speed = 1;

        animator.SetBool("isLeft", true);
        animator.SetBool("isRight", false);
        animator.SetBool("isBack", false);
        animator.SetBool("isFront", false);
    }
    public void RightMove()
    {
        Animator animator;
        animator = GetComponent<Animator>();
        animator.speed = 1;

        animator.SetBool("isLeft", false);
        animator.SetBool("isRight", true);
        animator.SetBool("isBack", false);
        animator.SetBool("isFront", false);
    }
    public void FrontMove()
    {
        Animator animator;
        animator = GetComponent<Animator>();
        animator.speed = 1;

        animator.SetBool("isLeft", false);
        animator.SetBool("isRight", false);
        animator.SetBool("isBack", false);
        animator.SetBool("isFront", true);
    }
    public void BackMove()
    {
        Animator animator;
        animator = GetComponent<Animator>();
        animator.speed = 1;

        animator.SetBool("isLeft", false);
        animator.SetBool("isRight", false);
        animator.SetBool("isBack", true);
        animator.SetBool("isFront", false);
    }
    public void StopAnimation()
    {
        Animator animator;
        animator = GetComponent<Animator>();
        animator.speed = 0;
    }
}

