using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Laser laserPrefab;
    public GameManager gm;

    public float speed;
    private float fixedSpeed;

    private Rigidbody2D rb;
    private PlayerSoundManager psm;
    private SpriteRenderer sr;
    public Camera cam;

    private Vector2 movement;
    private Vector2 mousePosition;
    private Vector2 screenMax;
    private Vector2 screenMin;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        psm = GetComponent<PlayerSoundManager>();
        sr = GetComponent<SpriteRenderer>();
        screenMin = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        screenMax = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        fixedSpeed = speed;
    }

    private void Update()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if ((mousePosition - rb.position).magnitude > 1.5f)
        {
            movement = mousePosition - rb.position;
        }


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

    private void FixedUpdate()
    {
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90.0f;
        rb.rotation = angle;

        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.MovePosition(rb.position + new Vector2(-movement.y, movement.x).normalized * speed * Time.fixedDeltaTime);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(rb.position + new Vector2(movement.y, -movement.x).normalized * speed * Time.fixedDeltaTime);
        }


        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

    }

    private void Fire()
    {
        psm.PlayFireSound();
        Laser laser = Instantiate(laserPrefab, transform.position + transform.up, transform.rotation);
        laser.Project(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0.0f;

            gameObject.SetActive(false);

            gm.PlayerHurt();
        }
    }
}
