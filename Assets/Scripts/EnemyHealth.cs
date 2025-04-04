using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Здоровье врага")]
    public int health = 30;

    [Header("Эффекты")]
    public ParticleSystem hitEffect;            // эффект при попадании
    public AudioSource hitSound;                // звук при попадании
    public GameObject explosionPrefab;          // префаб взрыва

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerProjectile"))
        {
            // 1. Уменьшаем здоровье
            health -= 10;

            // 2. Воспроизводим эффект попадания
            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);

            // 3. Звук попадания
            if (hitSound != null)
                hitSound.Play();

            // 4. Вспышка красного
            if (spriteRenderer != null)
                StartCoroutine(FlashRed());

            // 5. Удаляем пулю
            Destroy(other.gameObject);

            // 6. Проверка смерти
            if (health <= 0)
            {
                Debug.Log("Враг уничтожен!");

                // 6.1 Воспроизводим взрыв
                if (explosionPrefab != null)
                    Instantiate(explosionPrefab, transform.position, Quaternion.identity);

                // 6.2 Увеличиваем очки игрока
                if (GameManager.Instance != null)
                    GameManager.Instance.AddScore(10);

                // 6.3 Удаляем врага
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Враг получил урон. Осталось HP: " + health);
            }
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }
}
