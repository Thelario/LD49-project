using UnityEngine;

public class DifficultyButton : MonoBehaviour
{
    public Difficulty difficulty;

    public void ChangeDifficulty()
    {
        GameManager.Instance.GameDifficulty = difficulty;
    }
}
