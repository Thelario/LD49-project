using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawnner : MonoBehaviour
{
    [Header("Rock Prefab")]
    [SerializeField] private GameObject rockPrefab;

    [Header("Fields")]
    [SerializeField] private Vector2[] possibleSpawnPositions;
    [SerializeField] private float timeBetweenRocks;
    [SerializeField] private float zOffset;

    private UpwardMovement um;

    private void Awake()
    {
        um = UpwardMovement.Instance;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(6f);
        StartCoroutine(SpawnRock());
    }

    private IEnumerator SpawnRock()
    {
        Instantiate(rockPrefab, um.transform.position + GetRandomSpawnPos(), Quaternion.identity, this.transform);

        float timeBetweenRocksCounter = SetTimeBetweenRocks();

        yield return new WaitForSeconds(timeBetweenRocksCounter);

        yield return SpawnRock();
    }

    private Vector3 GetRandomSpawnPos()
    {
        Vector2 pos = possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Length)];
        Vector3 finalPos = new Vector3(pos.x, pos.y, zOffset);
        return finalPos;
    }

    private float SetTimeBetweenRocks()
    {
        return GameManager.Instance.GameDifficulty switch
        {
            Difficulty.Easy => timeBetweenRocks * 1.5f,
            Difficulty.Normal => timeBetweenRocks,
            Difficulty.Difficult => timeBetweenRocks * 0.5f,
            _ => 1f,
        };
    }
}
