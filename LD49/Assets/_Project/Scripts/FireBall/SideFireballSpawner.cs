using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFireballSpawner : MonoBehaviour
{

    public GameObject fireBall;
    public float timeBetween;
    private float timeValue;
    public Transform spawnPoint;
    public float fireForce;
    // Start is called before the first frame update
    void Start()
    {
        timeValue = timeBetween;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeValue <= 0)
        {
            GameObject bullet = Instantiate(fireBall, spawnPoint.position, spawnPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(spawnPoint.up * fireForce, ForceMode2D.Impulse);
            timeValue = timeBetween;
        }
        else
        {
            timeValue -= Time.deltaTime;
        }
    }
}
