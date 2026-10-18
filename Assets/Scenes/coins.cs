
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int value = 1;
    [SerializeField] float spinSpeed = 90f;

    void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        Debug.Log("Player collected coin!");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(value);
        }

        Spawner spawner = GetComponentInParent<Spawner>();

        if (spawner != null)
        {
            spawner.CoinCollected();
        }

        Destroy(gameObject);
    }
}
