using System.Collections;
using UnityEngine;

public class DestroyYourself : MonoBehaviour
{
    [SerializeField] private float distanceToBeDestroyed;

    private UpwardMovement um;

    private void Awake()
    {
        um = UpwardMovement.Instance;
    }

    private void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        Vector3 centerPos = um.transform.position;
        float yDst = centerPos.y - transform.position.y;
        if (yDst > 0f)
        {
            if (yDst > distanceToBeDestroyed)
            {
                Destroy(gameObject);
            }
        }
    }
}
