using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public ParticleSystem explosion;
    public Text scoreText;
    public Image[] lives;

    public AsteroidSpawner spawner;

    public float cooldown = 1.0f;
    public float godModeCooldown = 2.0f;

    public int health = 3;
    public int score = 0;
    public int numberOfLives;

    private void Start()
    {
        numberOfLives = health;
    }

    private void Update()
    {
        scoreText.text = "SCORE: " + score;
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
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
            explosion.Play();
            GameOver();
        }
        else
        {
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
        //TODO
    }
}
