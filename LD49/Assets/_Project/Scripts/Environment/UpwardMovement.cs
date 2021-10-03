using UnityEngine;

public class UpwardMovement : Singleton<UpwardMovement>
{
    [SerializeField] private float ySpeed;

    void Update()
    {
        float speed = CheckDifficulty();

        transform.position += new Vector3(0f, speed * Time.deltaTime, 0f);
    }

    private float CheckDifficulty()
    {
        if (GameManager.Instance.GameDifficulty == Difficulty.Easy)
        {
            return ySpeed;
        }
        else if (GameManager.Instance.GameDifficulty == Difficulty.Normal)
        {
            return ySpeed * 1.25f;
        }
        else
        {
            return ySpeed * 1.6f;
        }
    }
}
