
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject redCoinPrefab;
    [SerializeField] GameObject blueCoinPrefab;
    [SerializeField] Transform player;
    [SerializeField] float area = 7f;

    GameObject currentCoin;
    int redCoinTarget;
    bool redCoinSpawned;
    bool bonusStarted;

    void Start()
    {
        redCoinTarget = Random.Range(1, 10);
        SpawnCoin();
    }

    public void SpawnCoin()
    {
        if (currentCoin != null)
            return;

        if (!redCoinSpawned &&
            GameManager.Instance != null &&
            GameManager.Instance.GetScore() >= redCoinTarget)
        {
            SpawnRedCoin();
            return;
        }

        SpawnObject(coinPrefab);
    }

    void SpawnRedCoin()
    {
        if (redCoinPrefab == null)
        {
            Debug.LogError("Assign Red Coin Prefab!");
            return;
        }

        redCoinSpawned = true;
        SpawnObject(redCoinPrefab);
    }

    void SpawnObject(GameObject prefab)
    {
        if (prefab == null || player == null)
        {
            Debug.LogError("Assign Prefab and Player!");
            return;
        }

        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-area, area),
                1f,
                Random.Range(-area, area)
            );

            if (Vector3.Distance(pos, player.position) < 2f)
                continue;

            currentCoin = Instantiate(
                prefab,
                pos,
                Quaternion.Euler(90f, 0f, 0f),
                transform
            );
            return;
        }

        Debug.LogWarning("Could not find a suitable coin position.");
    }

    public void CoinCollected()
    {
        currentCoin = null;
        SpawnCoin();
    }

    public void RedCoinCollected()
    {
        currentCoin = null;

        if (bonusStarted)
            return;

        bonusStarted = true;
        SpawnBlueCoins();
        SpawnObject(coinPrefab);
    }

    void SpawnBlueCoins()
    {
        if (blueCoinPrefab == null || player == null)
        {
            Debug.LogError("Assign Blue Coin Prefab and Player!");
            return;
        }

        for (int i = 0; i < 50; i++)
        {
            float angle = i * (360f / 50f) * Mathf.Deg2Rad;
            float radius = 6f;

            Vector3 pos = player.position + new Vector3(
                Mathf.Cos(angle) * radius,
                1f,
                Mathf.Sin(angle) * radius
            );

            Instantiate(
                blueCoinPrefab,
                pos,
                Quaternion.Euler(90f, 0f, 0f),
                transform
            );
        }
    }
}
