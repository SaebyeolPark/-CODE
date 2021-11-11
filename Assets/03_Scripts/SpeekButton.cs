using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeekButton : MonoBehaviour
{
    public GameObject chat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        chat.SetActive(true);
    }
}
