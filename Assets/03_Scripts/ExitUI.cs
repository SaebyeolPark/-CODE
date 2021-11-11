using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitUI : MonoBehaviour
{
    zFoxVirtualPad vpad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Exit()
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
        vpad_btnX = zFOXVPAD_BUTTON.NON;
        gameObject.SetActive(false);
        GameManager.instance.isControl = true;
        Time.timeScale = 1;
        PlayerMain.instance.isPressed = false;

    }
}
