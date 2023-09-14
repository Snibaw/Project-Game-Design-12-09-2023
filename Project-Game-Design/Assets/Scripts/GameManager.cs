using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float[] yBounds;
    [SerializeField] private float[] timeBtwEnemiesBounds;
    [SerializeField] private GameObject loosingScreen;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int scoreEarnWhenKillingEnemy;
    private float spawnEnemyTimer;
    // Start is called before the first frame update
    private void Awake() {
        if(instance == null) instance = this;
        else if(instance != this) Destroy(gameObject);
    }
    private void Start() {
        Instantiate(playerPrefab, new Vector3(-6,0,0), Quaternion.identity);
        spawnEnemyTimer = 1f;
        loosingScreen.SetActive(false);
        scoreText.text = "0";

        DeathEvent.OnDeath += HandleDeath;
    }
    private void Update() {
        spawnEnemyTimer -= Time.deltaTime;
        if(spawnEnemyTimer <= 0)
        {
            spawnEnemyTimer = Random.Range(timeBtwEnemiesBounds[0], timeBtwEnemiesBounds[1]);
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, new Vector3(12, Random.Range(yBounds[0],yBounds[1]),0), Quaternion.identity);
    }

    private void OnDestroy()
    {
        DeathEvent.OnDeath -= HandleDeath;
    }
    private void HandleDeath(GameObject deadObject)
    {
        if(deadObject.tag == "Player")
        {
            loosingScreen.SetActive(true);
        }
        else if(deadObject.tag == "Enemy")
        {
            scoreText.text = (int.Parse(scoreText.text) + scoreEarnWhenKillingEnemy).ToString();
        }
    }
}
