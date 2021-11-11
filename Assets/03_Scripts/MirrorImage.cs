using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorImage : MonoBehaviour
{
    public Sprite[] imageArr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMain.instance.transform.position.x <= 0.6f)
        {

            GetComponent<SpriteRenderer>().sprite = imageArr[0];
        }

            if (GameManager.instance.playerRepeat >= 3 && GameManager.instance.playerRepeat <= 5)
            {
                GetComponent<SpriteRenderer>().sprite = imageArr[1];
            }
            else if (GameManager.instance.playerRepeat >= 6 && GameManager.instance.playerRepeat <= 7)
            {
                GetComponent<SpriteRenderer>().sprite = imageArr[2];

            }
            else if (GameManager.instance.playerRepeat >= 8)
            {
                GetComponent<SpriteRenderer>().sprite = imageArr[2];

            }
           
    }
}
