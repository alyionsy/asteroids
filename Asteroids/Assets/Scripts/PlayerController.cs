﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public Laser laserPrefab;
    public GameManager gameManager;
    public Camera cam;

    private Rigidbody2D rigidBody;
    private PlayerSoundManager soundManager;
    private SpriteRenderer spriteRenderer;

    private Vector2 movement;
    private Vector2 mousePosition;
    private Vector2 screenMax;
    private Vector2 screenMin;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        soundManager = GetComponent<PlayerSoundManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        screenMin = Camera.main.ViewportToWorldPoint(Vector2.zero);
        screenMax = Camera.main.ViewportToWorldPoint(Vector2.one);
    }

    private void Update()
    {
        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if ((mousePosition - rigidBody.position).magnitude > 1.5f)
        {
            movement = mousePosition - rigidBody.position;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rigidBody.MovePosition(rigidBody.position + movement.normalized * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        }

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

    private void FixedUpdate()
    {
        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90.0f;
        rigidBody.rotation = angle;

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        soundManager.PlayFireSound();
        Laser laser = Instantiate(laserPrefab, transform.position + transform.up, transform.rotation);
        laser.Project(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            rigidBody.velocity = Vector2.zero;

            gameObject.SetActive(false);

            gameManager.PlayerHurt();
        }
    }
}
