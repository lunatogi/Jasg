using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScript : MonoBehaviour {
    public Transform spawnPoint;

    Vector3 firstPos;
    Vector3 lastPos;

    float gapX;
    float gapY;

    public bool buttonDown = false;

    GameObject ball;
    public GameObject prefabBall;

    public Transform[] gizmoPoints;

    public float timer;
	// Use this for initialization
	void Start () {
        ball = GameObject.FindGameObjectWithTag("ball");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1") && Input.mousePosition.y < Screen.height/2)
        {
            buttonDown = true;
            firstPos = Input.mousePosition;
        }else if (Input.GetButtonUp("Fire1") && buttonDown)
        {
            lastPos = Input.mousePosition;
            gapX = lastPos.x - firstPos.x;
            gapY = lastPos.y - firstPos.y;
            float hipo = Mathf.Sqrt(gapX * gapX + gapY * gapY);
            ball.GetComponent<CircleCollider2D>().isTrigger = true;
            ball.GetComponent<Rigidbody2D>().velocity = new Vector3(gapX / hipo * 15, gapY / hipo * 15);
            ball.gameObject.tag = "outball";
            GameObject newBall = Instantiate(prefabBall, spawnPoint.position, spawnPoint.rotation);
            newBall.gameObject.tag = "ball";
            ball = GameObject.FindGameObjectWithTag("ball");
            buttonDown = false;
        }

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("target");
            foreach(GameObject target in targets)
            {
                target.SendMessage("SpeedUp", SendMessageOptions.DontRequireReceiver);
            }
            timer = 5;
        }
        
	}
}
