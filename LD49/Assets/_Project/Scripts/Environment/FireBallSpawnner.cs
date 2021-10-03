using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpawnner : MonoBehaviour
{
    [Header("Rock Prefab")]
    [SerializeField] private GameObject fireBallPrefab;

    [Header("Fields")]
    [SerializeField] private Vector2[] possibleSpawnPositions;
    [SerializeField] private float timeBetweenFireBalls;
    [SerializeField] private float zOffset;

    [Header("References")]
    [SerializeField] private Transform lava;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);

        yield return SpawnFireBall();
    }

    private IEnumerator SpawnFireBall()
    {
        Instantiate(fireBallPrefab, new Vector3(lava.position.x, lava.position.y, 0f) + GetRandomSpawnPos(), Quaternion.identity, this.transform);

        float timeBetweenFireBallsCounter = SetTimeBetweenFireBalls();

        yield return new WaitForSeconds(timeBetweenFireBallsCounter);

        yield return SpawnFireBall();
    }

    private Vector3 GetRandomSpawnPos()
    {
        Vector2 pos = possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Length)];
        Vector3 finalPos = new Vector3(pos.x, pos.y, zOffset);
        return finalPos;
    }

    private float SetTimeBetweenFireBalls()
    {
        return GameManager.Instance.GameDifficulty switch
        {
            Difficulty.Easy => timeBetweenFireBalls * 1.5f,
            Difficulty.Normal => timeBetweenFireBalls,
            Difficulty.Difficult => timeBetweenFireBalls * 0.5f,
            _ => 1f,
        };
    }
}
