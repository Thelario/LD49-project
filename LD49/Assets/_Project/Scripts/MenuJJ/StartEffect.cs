using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEffect : MonoBehaviour
{

    [SerializeField] private Transform upwardMovementParent;
   
    public float valueOfShake;

    /*
    private void Start()
    {
        PlayerMovement.OnPlayerExplote += explosionShake;
    }

    private void OnDestroy()
    {
        PlayerMovement.OnPlayerExplote -= explosionShake;
    }
    */

    private void Update()
    {
        float rndX = Random.Range(-valueOfShake, valueOfShake);
        float rndY = Random.Range(-valueOfShake, valueOfShake);

        if (upwardMovementParent.position.x - (transform.position.x + rndX) > 0.1f ||
            upwardMovementParent.position.y - (transform.position.y + rndY) > 0.1f)
            return;

        transform.position += new Vector3(rndX, rndY, 0);
    }

    public void explosionShake()
    {
        StartCoroutine(explosionShakeC());
    }
    IEnumerator explosionShakeC()
    {
        valueOfShake = 0.04f;
        yield return new WaitForSeconds(0.6f);
        valueOfShake = 0.02f;
    }
}

