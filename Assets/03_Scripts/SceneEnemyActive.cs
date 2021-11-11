using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEnemyActive : MonoBehaviour
{
    public static SceneEnemyActive instance = null;

    public float sceneTimer = 0;
    public GameObject firstEnemy;
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
        
    }

    // Update is called once per frame
    void Update()
    {

        if(FirstEnemyMain.instance.isSceneLoad==true)
        {
            firstEnemy.SetActive(false);
            sceneTimer += Time.deltaTime;
            if (sceneTimer > 2)
            {
                firstEnemy.SetActive(true);
                sceneTimer = 0;
                FirstEnemyMain.instance.isSceneLoad = false;
            }

            //  StartCoroutine(EnemmyNext());

        }
    }
}
