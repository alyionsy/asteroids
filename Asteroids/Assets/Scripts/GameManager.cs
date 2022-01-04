using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public ParticleSystem explosion;
    public Text scoreText;
    public Image[] lives;
    private GameplaySoundManager gsm;

    public AsteroidSpawner spawner;

    public float cooldown = 1.0f;
    public float godModeCooldown = 10.0f;

    private int health = 3;
    private int numberOfLives;
    public int score = 0;

    private void Start()
    {
        gsm = GetComponent<GameplaySoundManager>();
        numberOfLives = health;
    }

    private void Update()
    {
        scoreText.text = "SCORE: " + score;
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        gsm.PlayAsteroidDestroySound();
        explosion.Play();
        score += 100;

        spawner.Spawn(1);
    }

    public void PlayerHurt()
    {
        health--;
        lives[health].enabled = false;

        if (health <= 0)
        {
            explosion.transform.position = player.transform.position;
            gsm.PlayDeathSound();
            explosion.Play();
            Invoke(nameof(GameOver), cooldown);
        }
        else
        {
            gsm.PlayHurtSound();
            Invoke(nameof(Cooldown), cooldown);
        }
    }

    private void Cooldown()
    {
        player.gameObject.layer = LayerMask.NameToLayer("GodMode");
        player.gameObject.SetActive(true);
        
        Invoke(nameof(GodModeOff), godModeCooldown);
    }

    private void GodModeOff()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
