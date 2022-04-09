using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	//public float xMax, xMin, yMax, yMin;
    Transform player;
	Vector3 target;
	//cameraShake
	public float shakeAmount=0.1f;
	float shakeTime;
	Vector3 initialPosition;
	public static CameraMove instance=null;
	float targetX;
	float targetY;
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
	// Use this for initialization
	void Start()
	{
		
		initialPosition = new Vector3(0, 0, transform.position.z);
		shakeAmount = 0.5f;
		player = GameObject.Find("Player").GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
        if (GameManager.instance.isControl == true ||PlayerMain.instance.isEvent==true)
        {
			target = new Vector3(player.position.x, player.position.y, player.position.z - 10);
			transform.position = Vector3.Lerp(transform.position, target, 0.05f);
        }
        else
        {
            if (PlayerMain.instance.isOpeningCamera == true && PlayerMain.instance.currentSceneName == "1FloorCenter")
            {
               
	         if (transform.position.x > 0)
                {
					transform.Translate(2f * Vector3.left * Time.deltaTime);

                }
                else
                {
					PlayerMain.instance.isOpeningCamera = false;
                }

            }

            if (PlayerMain.instance.isEndingCamera == true)
            {
				targetX = (player.position.x + FirstEnemyMain.instance.gameObject.transform.position.x) / 2;
				targetY = (player.position.y + FirstEnemyMain.instance.gameObject.transform.position.y) / 2;
				target = new Vector3(targetX, targetY, player.position.z - 10);
				transform.position = Vector3.Lerp(transform.position, target, 0.05f);
			}

			if (shakeTime > 0)
			{
				transform.position = Random.insideUnitSphere * shakeAmount + initialPosition;
				shakeTime -= Time.deltaTime;
				Debug.Log(shakeTime);
            }
            else
            {
				shakeTime = 0.0f;
            }
		}
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), Mathf.Clamp(transform.position.y, yMin, yMax), target.z);
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, 3.65f), Mathf.Clamp(transform.position.y, 0, 3.7f), target.z);
        
	}
	public void VibrateForTime(float time)
    {
		shakeTime = time;
    }

}
