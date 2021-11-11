using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseWindow : MonoBehaviour
{
    public Sprite[] imageArr;
    float timer;
    bool timerStart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playerRepeat == 5)
        {
            GetComponent<SpriteRenderer>().sprite = imageArr[1];
        }
         if (GameManager.instance.playerRepeat >=6)
        {
            timerStart = true;
            GetComponent<SpriteRenderer>().sprite = imageArr[2];
           
        }
        if (GameManager.instance.firstEnterNurseRoom == true)
        {
            GetComponent<SpriteRenderer>().sprite = imageArr[2];

        }

        if (timerStart == true)
        {
            timer += Time.deltaTime;
            if (timer > 0.3f)
            {
                GameManager.instance.playerRepeat = 7;
                timerStart = false;

            }
        }
    }
}
