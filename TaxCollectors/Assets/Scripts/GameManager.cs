using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject enemyPrefab; // Assign your enemy prefab in the Unity Inspector
    public GameObject ammoPrefab;
    public GameObject player;
    public GunController gun;
    public Transform playerSpawn;

    public float initialSpawnDelay = 2.0f; // Time before the first enemy spawns
    public float spawnIntervalDecrease = 0.1f; // Decrease in time between spawns
    public float minimumSpawnInterval = 0.5f; // Minimum time between spawns

    public TextMeshProUGUI ScoreTxt;
    public TextMeshProUGUI HighscoreTxt;
    public TextMeshProUGUI EndScoreTxt;

    private int score = 0;
    private int highScore = 0;
    private float spawnInterval; // Current time between spawns
    private float nextSpawnTime; // When to spawn the next enemy

    private Camera mainCamera; // Reference to the main camera
    public bool playerDead;

    public GameObject GameUI;
    public GameObject IntroUI;
    public GameObject EndGameUI;

    void Start()
    {
        Time.timeScale = 0.0f;
        playerDead = true;
        mainCamera = Camera.main; // Cache the main camera
        GameUI.SetActive(false);
        IntroUI.SetActive(true);
        EndGameUI.SetActive(false);
        gun.ammo = 20;

        
    }

    public void newGame()
    {
        //all enemies deleted
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        GameObject[] ammos = GameObject.FindGameObjectsWithTag("Ammo");
        foreach (GameObject ammo in ammos)
        {
            Destroy(ammo);
        }
        player.SetActive(true);
        IntroUI.SetActive(false);
        EndGameUI.SetActive(false);
        GameUI.SetActive(true);
        spawnInterval = initialSpawnDelay;
        nextSpawnTime = Time.time + spawnInterval; // Schedule the first spawn
        score = 0;
        playerDead = false;
        Time.timeScale = 1.0f;
        //Reset player location
        player.transform.position = playerSpawn.position;
        //Gun Controller reset
        gun.ammo = 20;
        //ammo reset
        
        
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && playerDead == false)
        {
            SpawnEnemyOffScreen();
            UpdateSpawnTiming();
        }
        if (playerDead)
        {
            if (score > highScore)
            {
                highScore = score;
            }
        }
        ScoreTxt.text = "Taxes Recovered: $" + score.ToString();
        HighscoreTxt.text = "Highscore : $" + highScore.ToString();
        EndScoreTxt.text = "You saved $" + score.ToString() + " taxes";
    }

    public void AddScore(int amt)
    {
        score += amt;
    }
    void SpawnEnemyOffScreen()
    {
        Vector2 spawnPosition = GetRandomOffScreenPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void SpawnAmmoBox(int chance, Vector2 spawnPosition)
    {
        if (Random.Range(0, 100) < chance)
        {
            Instantiate(ammoPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector2 GetRandomOffScreenPosition()
    {
        float randomValue = Random.value;
        Vector2 viewportPosition = new Vector2();

        if (randomValue < 0.25f) // Top
        {
            viewportPosition.x = Random.Range(0f, 1f);
            viewportPosition.y = 1.1f; // Slightly above the top edge
        }
        else if (randomValue < 0.5f) // Bottom
        {
            viewportPosition.x = Random.Range(0f, 1f);
            viewportPosition.y = -0.1f; // Slightly below the bottom edge
        }
        else if (randomValue < 0.75f) // Right
        {
            viewportPosition.x = 1.1f; // Slightly to the right of the right edge
            viewportPosition.y = Random.Range(0f, 1f);
        }
        else // Left
        {
            viewportPosition.x = -0.1f; // Slightly to the left of the left edge
            viewportPosition.y = Random.Range(0f, 1f);
        }

        Vector2 worldPosition = mainCamera.ViewportToWorldPoint(viewportPosition);
        return worldPosition;
    }

    void UpdateSpawnTiming()
    {
        spawnInterval = Mathf.Max(spawnInterval - spawnIntervalDecrease, minimumSpawnInterval);
        nextSpawnTime = Time.time + spawnInterval;
    }
}
