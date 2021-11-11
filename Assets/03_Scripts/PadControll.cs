using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadControll : MonoBehaviour
{
    public GameObject pad;
    public static PadControll instance = null;

    private void Awake()
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
        
    }

    // Update is called once per frame
    void Update()
    {
        //보이기 안보이기
        if (GameManager.instance.isControl == false)
        {
            pad.SetActive(false);
        }
        else
        {
            pad.SetActive(true);
        }
    }
}
