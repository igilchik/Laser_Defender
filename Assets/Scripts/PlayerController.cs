using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Vector2 inputData;
    public float moveSpeed = 5f;
    Vector2 minBounds, maxBounds;

    public float marginLeft = 0.4f, marginRight = 0.4f, marginTop = 0.4f, marginBottom = 1.5f;

    public GameObject projectilePrefab;
    public float projectileSpeed = 15f;
    private bool fire = false;
    public AudioSource audioShoot;

    [Header("Game Over UI")]
    public GameObject gameOverPanel; // <- Присвоим в инспекторе

    void OnMove(InputValue data)
    {
        inputData = data.Get<Vector2>();
    }

    void OnFire()
    {
        fire = true;
    }

    void Start()
    {
        initBounds();
    }

    void initBounds()
    {
        Camera mainCam = Camera.main;
        minBounds = mainCam.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCam.ViewportToWorldPoint(new Vector2(1, 1));
        maxBounds.x -= marginRight;
        minBounds.y += marginBottom;
        maxBounds.y -= marginTop;
    }

    void Update()
    {
        if (gameOverPanel.activeSelf) return; // Если игрок мертв — ничего не делать

        Vector2 delta = inputData * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y, maxBounds.y);
        transform.position = newPos;

        if (fire)
        {
            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.velocity = transform.up * projectileSpeed;

            if (audioShoot != null)
                audioShoot.Play();

            fire = false;
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);   // показать панель
        Destroy(gameObject);             // уничтожить игрока
        Time.timeScale = 0f;             // остановить игру
        Debug.Log("Игрок уничтожен. Game Over!");
    }
}
