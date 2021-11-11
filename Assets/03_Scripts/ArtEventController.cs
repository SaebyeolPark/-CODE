﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtEventController : MonoBehaviour
{
    public GameObject[] onTriggers;
    private EventTrigger[] offTriggers;    // Start is called before the first frame update
    public GameObject nurseRoomWait;
    void Start()
    {
        offTriggers = gameObject.GetComponentsInChildren<EventTrigger>();

    }

    // Update is called once per frame
    void Update()
    {
      
        //여긴 다른거
        if (GameManager.instance.isArtEvent)
        {
            for (int i = 0; i < offTriggers.Length; i++)
            {
                if (offTriggers[i].isArtEventTriggerOff)
                {
                    offTriggers[i].gameObject.SetActive(false);
                }
            }
            for(int i=0; i<onTriggers.Length; i++)
            {
                if (onTriggers[i].name == "가위이벤트" && GameManager.instance.isGetNameplace == false){

                }else if(onTriggers[i].name == "핏자국이벤트" && GameManager.instance.isGetSyringe == false)
                {

                }
                else
                {
                    onTriggers[i].SetActive(true);
                }

                if (onTriggers[i].name == "가위" && GameManager.instance.isGetNameplace == true)
                    onTriggers[i].SetActive(false);
                else if (onTriggers[i].name == "핏자국" && GameManager.instance.isGetSyringe == true)
                    onTriggers[i].SetActive(false);
            }
        }
        


    }
}
