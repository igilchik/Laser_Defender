using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Здоровье игрока")]
    public int health = 50;

    [Header("Эффекты при попадании")]
    public ParticleSystem hitEffect;
    public AudioSource hitSound;

    [Header("Эффекты при смерти")]
    public GameObject explosionPrefab; // Префаб анимации взрыва

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Получение урона от снаряда
        if (other.CompareTag("EnemyProjectile"))
        {
            health -= 10;

            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);

            if (hitSound != null)
                hitSound.Play();

            Destroy(other.gameObject);

            Debug.Log("Игрок получил урон от снаряда. Осталось HP: " + health);
        }

        // Мгновенная смерть при столкновении с врагом
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Игрок столкнулся с врагом!");
            health = 0;
        }

        // Проверка на смерть
        if (health <= 0)
        {
            Debug.Log("Игрок уничтожен!");

            if (explosionPrefab != null)
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            if (GameManager.Instance != null)
                GameManager.Instance.GameOver();

            Destroy(gameObject);
        }
    }
}

