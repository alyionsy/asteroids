using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public GameManager gameManager;

    public float speed = 5.0f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private Vector2 screenMax;
    private Vector2 screenMin;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        boxCollider.size = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
        rigidBody.rotation = Random.value * Mathf.Rad2Deg;

        screenMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        screenMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Update()
    {
        if (!spriteRenderer.isVisible)
        {
            if (rigidBody.position.x > screenMax.x)
            {
                rigidBody.position = new Vector2(screenMin.x, rigidBody.position.y);
            }

            if (rigidBody.position.x < screenMin.x)
            {
                rigidBody.position = new Vector2(screenMax.x, rigidBody.position.y);
            }

            if (rigidBody.position.y > screenMax.y)
            {
                rigidBody.position = new Vector2(rigidBody.position.x, screenMin.y);
            }

            if (rigidBody.position.y < screenMin.y)
            {
                rigidBody.position = new Vector2(rigidBody.position.x, screenMax.y);
            }
        }
    }

    public void SetTrajectory(Vector2 direction)
    {
        rigidBody.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            gameManager.AsteroidDestroyed(this);
            Destroy(gameObject);
        }
    }
}
