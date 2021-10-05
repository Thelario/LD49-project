using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField] private float ySpeed;
    [SerializeField] private MoveDownType type;

    void Update()
    {
        float speed;
        if (type == MoveDownType.Rock)
            speed = GetSpeedFromDifficulty();
        else
            speed = ySpeed;

        transform.position += new Vector3(0f, -speed * Time.deltaTime, 0f);
    }

    private float GetSpeedFromDifficulty()
    {
        return GameManager.Instance.GameDifficulty switch
        {
            Difficulty.Easy => ySpeed * 1f,
            Difficulty.Normal => ySpeed * 1.75f,
            Difficulty.Difficult => ySpeed * 3f,
            _ => 1f,
        };
    }
}

public enum MoveDownType
{
    Ground,
    Rock
}
