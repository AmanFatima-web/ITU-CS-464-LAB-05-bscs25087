
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] float spinSpeed = 90f;

    public enum CoinType
    {
        Normal,
        Red,
        Blue
    }

    [SerializeField] CoinType coinType = CoinType.Normal;

    void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        Spawner spawner = GetComponentInParent<Spawner>();

        if (coinType == CoinType.Red)
        {
            if (spawner != null)
                spawner.RedCoinCollected();
        }
        else if (coinType == CoinType.Blue)
        {
            if (GameManager.Instance != null)
                GameManager.Instance.AddBonusCoin();
        }
        else
        {
            if (GameManager.Instance != null)
                GameManager.Instance.AddScore(value);

            if (spawner != null)
                spawner.CoinCollected();
        }

        Destroy(gameObject);
    }
}
