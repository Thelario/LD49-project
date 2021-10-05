using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideFireballSpawner : MonoBehaviour
{

    public GameObject fireBall;
    public float timeBetween;
    public float timeBeforeBeginning;
    private float timeValue;
    public Transform spawnPoint;
    public bool isLeft;
    public float fireForce;
    // Start is called before the first frame update
    void Start()
    {
        timeValue = timeBetween;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBeforeBeginning < 0)
        {
            if (timeValue <= 0)
            {
                int randomAngle = Random.Range(90, 60);
                if (isLeft) randomAngle *= -1;
                spawnPoint.rotation = Quaternion.Euler(0, 0, randomAngle);
                GameObject bullet = Instantiate(fireBall, spawnPoint.position, spawnPoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                float fireBallForce = GetForceFromDifficulty();
                rb.AddForce(spawnPoint.up * fireBallForce, ForceMode2D.Impulse);
                timeValue = timeBetween;
            }
            else
            {
                timeValue -= Time.deltaTime;
            }
        }
        else
        {
            timeBeforeBeginning-= Time.deltaTime;
        }
    }

    private float GetForceFromDifficulty()
    {
        return GameManager.Instance.GameDifficulty switch
        {
            Difficulty.Easy => fireForce * 1f,
            Difficulty.Normal => fireForce * 1.25f,
            Difficulty.Difficult => fireForce * 1.5f,
            _ => 1f,
        };
    }
}
