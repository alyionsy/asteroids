using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public float speed = 5.0f;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private BoxCollider2D bc;

    private Vector2 screenMax;
    private Vector2 screenMin;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        sr.sprite = sprites[Random.Range(0, sprites.Length)];
        bc.size = new Vector2(sr.sprite.bounds.size.x, sr.sprite.bounds.size.y);
        rb.rotation = Random.value * Mathf.Rad2Deg;

        screenMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        screenMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Update()
    {
        if (!sr.isVisible)
        {
            if (rb.position.x > screenMax.x)
            {
                rb.position = new Vector2(screenMin.x, rb.position.y);
            }

            if (rb.position.x < screenMin.x)
            {
                rb.position = new Vector2(screenMax.x, rb.position.y);
            }

            if (rb.position.y > screenMax.y)
            {
                rb.position = new Vector2(rb.position.x, screenMin.y);
            }

            if (rb.position.y < screenMin.y)
            {
                rb.position = new Vector2(rb.position.x, screenMax.y);
            }
        }
    }

    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            Destroy(gameObject);
        }
    }
}
