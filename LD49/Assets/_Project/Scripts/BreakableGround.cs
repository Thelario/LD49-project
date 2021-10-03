using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGround : MonoBehaviour
{

    bool playerIn;
    bool isBreaking;
    float timeUntilStart = 0.5f;
    float timeToBreakValue;
    public float timeToBreak;
    public PlatformEffector2D plataform;
    public LayerMask playerLayer;
    void Start()
    {
        isBreaking = false;
        playerIn = false;
        timeToBreakValue = timeToBreak;
    }

    // Update is called once per frame
    void Update()
    {


        if (isBreaking)
        {
            if (timeToBreakValue >= 0)
            {
                timeToBreakValue -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + "is gone");
    }
}

