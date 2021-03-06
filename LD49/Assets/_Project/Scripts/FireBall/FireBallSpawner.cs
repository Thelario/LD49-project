using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpawner : MonoBehaviour
{
    public GameObject fireBall;
    public float timeBetween;
    float timeValue;
    public float timeBeforeSpawning;
    void Start()
    {
        timeValue = timeBetween;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBeforeSpawning < 0)
        {
            if (timeValue <= 0)
            {
                Instantiate(fireBall, transform.position, transform.rotation);
                timeValue = timeBetween;
            }
            else
            {
                timeValue -= Time.deltaTime;
            }
        }
        else
        {
            timeBeforeSpawning -= Time.deltaTime;
        }
    }
}
