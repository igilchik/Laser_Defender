using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float minInterval = 1f;
    [SerializeField] float maxInterval = 3f;
    [SerializeField] float projectileSpeed = 5f;

    void Start()
    {
        StartCoroutine(FireContinuously());
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            float delay = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(delay);

            GameObject projectile = Instantiate(
                projectilePrefab,
                transform.position,
                Quaternion.identity
            );

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.down * projectileSpeed;
        }
    }
}
