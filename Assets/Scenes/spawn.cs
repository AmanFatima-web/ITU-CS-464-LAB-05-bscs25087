
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Transform player;
    [SerializeField] float area = 7f;

    GameObject currentCoin;

    void Start()
    {
        SpawnCoin();
    }

    public void SpawnCoin()
    {
        if (currentCoin != null)
        {
            return;
        }

        for (int i = 0; i < 10; i++)
        {
            float x = Random.Range(-area, area);
            float z = Random.Range(-area, area);

            Vector3 pos = new Vector3(x, 1f, z);

            if (Vector3.Distance(pos, player.position) < 2f)
            {
                continue;
            }

            currentCoin = Instantiate(
               coinPrefab, pos, Quaternion.Euler(90f, 0f, 0f), transform
            );

            return;
        }
    }

    public void CoinCollected()
    {
        currentCoin = null;
        SpawnCoin();
    }
}
