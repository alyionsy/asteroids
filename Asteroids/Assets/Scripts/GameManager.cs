using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public AsteroidSpawner spawner;
    public ParticleSystem explosion;
    public Text scoreText;
    public Image[] lives;

    public float cooldown = 1.0f;
    public float godModeCooldown = 10.0f;

    private GameplaySoundManager soundManager;

    private int health = 3;
    private int numberOfLives;
    private int score = 0;

    private void Start()
    {
        soundManager = GetComponent<GameplaySoundManager>();
        numberOfLives = health;
    }

    private void Update()
    {
        scoreText.text = "SCORE: " + score;
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        explosion.transform.position = asteroid.transform.position;
        soundManager.PlayAsteroidDestroySound();
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
            soundManager.PlayDeathSound();
            explosion.Play();
            Invoke(nameof(GameOver), cooldown);
        }
        else
        {
            soundManager.PlayHurtSound();
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
