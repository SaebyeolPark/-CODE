using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeName : MonoBehaviour
{

    Text sceneName;
    // Start is called before the first frame update
    void Start()
    {
        sceneName = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeSceneName()
    {
        sceneName.text = PlayerMain.instance.currentSceneName;
       
    }
}
