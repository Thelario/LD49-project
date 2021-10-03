using System.Collections;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    [Header("Parents")]
    [SerializeField] private Transform backgroundParent;
    [SerializeField] private Transform wallsParent;

    [Header("Prefabs")]
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private GameObject leftWallPrefab;
    [SerializeField] private GameObject rightWallPrefab;

    [Header("Timer")]
    [SerializeField] private float timeBetweenSpawn;

    private float yPosCounter = 0f;
    private bool difficulty = true;

    private void Start()
    {
        StartSpawnningEnvironment();
    }

    public void StartSpawnningEnvironment()
    {
        CreateWallsAndBackground();
        yPosCounter += 12f;

        StartCoroutine(SpawnWallsAndBackground());
    }

    public IEnumerator SpawnWallsAndBackground()
    {
        CreateWallsAndBackground();

        yPosCounter += 12f;

        float time;
        if (!difficulty)
            time = GetTimeFromDifficulty();
        else
        {
            time = 2f;
            difficulty = false;
        }

        yield return new WaitForSeconds(time);

        yield return SpawnWallsAndBackground();
    }

    private float GetTimeFromDifficulty()
    {
        return GameManager.Instance.GameDifficulty switch
        {
            Difficulty.Easy => timeBetweenSpawn * 6f,
            Difficulty.Normal => timeBetweenSpawn * 5f,
            Difficulty.Difficult => timeBetweenSpawn * 3f,
            _ => 1f,
        };
    }

    private void CreateAbstract(GameObject prefab, Transform parent)
    {
        Instantiate(prefab, new Vector3(0f, yPosCounter, 0f), Quaternion.identity, parent);
    }

    private void CreateWallsAndBackground()
    {
        CreateAbstract(backgroundPrefab, backgroundParent);
        CreateAbstract(leftWallPrefab, wallsParent);
        CreateAbstract(rightWallPrefab, wallsParent);
    }
}
