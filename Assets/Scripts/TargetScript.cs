using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour {
    public GameObject[] gizmos;
    GameObject spawnPoint;
    public bool loop = true;
    public bool firstLoop = true;
    public float speed = 1;

    public float[] distances;
    public int closest = 0;
    public float closestDist = 10;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 4; i++)
            gizmos[i] = GameObject.FindGameObjectWithTag("gizmo"+i);
        spawnPoint = GameObject.FindGameObjectWithTag("spawn");
	}

    // Update is called once per frame
    void Update() {
        if (!loop) { 
            transform.position = Vector2.MoveTowards(transform.position, spawnPoint.transform.position, Time.deltaTime);
        }
        else if (loop)
        {
            if (firstLoop)
            {
                SetLoop();
                firstLoop = false;
            }

            if (closest == 3)
            {
                //transform.LookAt(gizmos[3].transform.position);
                transform.position = Vector2.MoveTowards(transform.position, gizmos[0].transform.position, Time.deltaTime * speed);
                if (transform.position == gizmos[0].transform.position)
                    closest = 0;
            }
            else
            {
                //transform.LookAt(gizmos[closest - 1].transform.position);
                transform.position = Vector2.MoveTowards(transform.position, gizmos[closest+1].transform.position, Time.deltaTime * speed);
                if (transform.position == gizmos[closest+1].transform.position)
                    closest++;
            }
        }
	}

    public void SetLoop()
    {
        for (int i = 0; i < gizmos.Length; i++)
        {
            distances[i] = Vector2.Distance(transform.position, gizmos[i].transform.position);
            if (distances[i] < closestDist)
            {
                closest = i;
                closestDist = distances[i];
            }
        }
    }

    public void SpeedUp()
    {
        speed += 0.2f;
    }
}
