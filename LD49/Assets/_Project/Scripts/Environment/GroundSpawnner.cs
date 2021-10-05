using System.Collections;
using UnityEngine;

public class GroundSpawnner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject[] groundPrefabs;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject umbrPrefab;

    [Header("Fields")]
    [SerializeField] private Vector2[] possibleSpawnPositions;
    [SerializeField] private float timeToSpawnGroundLines;
    [SerializeField] private float zOffset;
    [SerializeField] private Vector3 coinOffset;

    private UpwardMovement um;

    private Vector2 previousGroundPos = Vector2.zero;

    private void Awake()
    {
        um = UpwardMovement.Instance;
    }

    void Start()
    {
        StartCoroutine(SpawnGroundLine());
    }

    private IEnumerator SpawnGroundLine()
    {
        GameObject ground = Instantiate(GetRandomGroundPrefab(), um.transform.position + GetRandomSpawnPos(), Quaternion.identity, this.transform);

        // Chance to spawn a coin as well
        int rnd = Random.Range(0, 100);
        if (rnd > 25) 
            Instantiate(coinPrefab, ground.transform.position + coinOffset, Quaternion.identity, this.transform);
        else if(rnd > 22)
        { 
           Instantiate(umbrPrefab, ground.transform.position + coinOffset, Quaternion.identity, this.transform);
        }


        float time = GetTimeFromDifficulty();

        yield return new WaitForSeconds(time);

        yield return SpawnGroundLine();
    }

    private float GetTimeFromDifficulty()
    {
        return GameManager.Instance.GameDifficulty switch
        {
            Difficulty.Easy => timeToSpawnGroundLines * 1.05f,
            Difficulty.Normal => timeToSpawnGroundLines * 0.85f,
            Difficulty.Difficult => timeToSpawnGroundLines * 0.65f,
            _ => timeToSpawnGroundLines,
        };
    }

    private Vector3 GetRandomSpawnPos()
    {
        Vector2 pos = Vector2.zero;
        bool param = true;
        while(param)
        {
            pos = possibleSpawnPositions[Random.Range(0, possibleSpawnPositions.Length)];

            if (previousGroundPos == Vector2.zero)
                break;

            switch (previousGroundPos.x)
            {
                case 3f:
                    if (pos.x == -3f)
                        continue;
                    else
                        param = false;
                    break;
                case -3f:
                    if (pos.x == 3f)
                        continue;
                    else
                        param = false;
                    break;
                case 0f:
                    param = false;
                    break;
            }
        }

        previousGroundPos = pos;
        Vector3 finalPos = new Vector3(pos.x, pos.y, zOffset);
        return finalPos;
    }

    private GameObject GetRandomGroundPrefab()
    {
        return GameManager.Instance.GameDifficulty switch
        {
            Difficulty.Easy => groundPrefabs[Random.Range(0, 3)],
            Difficulty.Normal => groundPrefabs[Random.Range(0, 2)],
            Difficulty.Difficult => groundPrefabs[Random.Range(0, 1)],
            _ => groundPrefabs[0],
        };
    }
}
