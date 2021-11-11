using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public static MenuController instance = null;

    public GameObject panel;
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
        GameManager.instance.isControl = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void ExitButton()
    {
        Application.Quit();
        
    }
   public void NewButton()
    {
        SceneManager.LoadScene("5Floor");
        PlayerMain.instance.transform.position = new Vector3(5.25f, 0.7f, 0);
        PlayerMain.instance.isOpening = true;
        panel.SetActive(false);
    } 
    public void CountinueButton()
    {

    }
}
