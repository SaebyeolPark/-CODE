using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMove : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public float speed=2;
    public GameObject player;
    public bool isDone=false;
   // Vector3 target;
    float xReach;
    float yReach;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player= GameObject.Find("Player");

        // isDone = GameManager.instance.isData;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    public void FiveFloor()
    {
        if (GameManager.instance.playerRepeat == 3)
        {
            if (transform.position.y >= -0.42f)
            {
                GameManager.instance.playerRepeat = 4;

            }
            else
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);

            }
        }else if (GameManager.instance.playerRepeat == 6)
        {
            spriteRenderer.sortingLayerName = "Default";
            if (transform.position.y <= -1.5f)
            {
                GameManager.instance.playerRepeat = 7;
                gameObject.SetActive(false);
            }
            else
            {
                transform.Translate(Vector2.down * 3 * Time.deltaTime);

            }
        }
    }
        void Move()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
