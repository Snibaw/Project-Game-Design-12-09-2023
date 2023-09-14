using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private TMP_Text numberOfWeaponUpgradeText;
    [SerializeField] private GameObject[] locks;
    [SerializeField] private bool isInfiniteMode = false;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Sprite winSprite;
    [SerializeField] private GameObject[] collectablePrefab;
    [SerializeField] private float probabilityOfZigZag;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float[] yBounds;
    [SerializeField] private float[] timeBtwEnemiesWaveBound;
    [SerializeField] private float[] timeBtwEnemiesBound;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int scoreEarnWhenKillingEnemy;

    [SerializeField] private Image[] weaponTypeImage;
    [SerializeField] private Sprite[] weaponTypeSprites;
    private float spawnWaveEnemyTimer;
    private GameObject player;
    private BulletGun bulletGun;
    private BaseAvatar playerAvatar;
    private int level;
    [SerializeField] private int numberOfWaves = 5;
    // Start is called before the first frame update
    private void Awake() {
        if(instance == null) instance = this;
        else if(instance != this) Destroy(gameObject);
    }
    private void Start() {
        player = Instantiate(playerPrefab, new Vector3(-6,0,0), Quaternion.identity);
        bulletGun = player.GetComponent<BulletGun>();
        playerAvatar = player.GetComponent<BaseAvatar>();

        spawnWaveEnemyTimer = 1f;
        endScreen.SetActive(false);
        scoreText.text = "0";
        ChangeWeaponTypeUI(0);

        DeathEvent.OnDeath += HandleDeath;

        level = PlayerPrefs.GetInt("level",1);
        if(!isInfiniteMode) levelText.text = "Level : " + level.ToString();
        numberOfWaves = 3 + Mathf.FloorToInt(level/3);
        probabilityOfZigZag = Mathf.Clamp(0.1f + level/10f,0.1f,0.8f);
        Debug.Log("Probability of zigzag : " + probabilityOfZigZag);

        locks[0].SetActive(true);
        locks[1].SetActive(true);
        SetWeaponUpgradeText();
    }
    private void Update() {
        // if(Input.GetKeyDown(KeyCode.Escape))
        // {
        //     PlayerPrefs.SetInt("weapon",PlayerPrefs.GetInt("weapon",0) + 1);
        //     SetWeaponUpgradeText();
        // }
        if(isInfiniteMode)
        {
            Debug.Log("Infinite mode");
        }
        else
        {
            if(numberOfWaves <= 0) 
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                if(enemies.Length == 0)
                {
                    if(int.Parse(scoreText.text) >= 0)
                    {
                        Invoke("ShowWinScreen", 1.5f);
                    }
                    else
                    {
                        endScreen.SetActive(true);
                        Time.timeScale = 0;
                    }
                    
                }
                return;
            }

            spawnWaveEnemyTimer -= Time.deltaTime;
            if(spawnWaveEnemyTimer <= 0)
            {
                spawnWaveEnemyTimer = Mathf.Infinity;
                StartCoroutine(SpawnEnemyWave());
                numberOfWaves--;
            }
        }
            
    }
    void SetWeaponUpgradeText()
    {
        int weaponUpgrade = PlayerPrefs.GetInt("weapon",0);
        numberOfWeaponUpgradeText.text = Mathf.Min(6,weaponUpgrade).ToString() + "/6";
        if(weaponUpgrade >= 3)
        {
            locks[0].SetActive(false);
            if(weaponUpgrade >= 6)
                locks[1].SetActive(false);
        }

    }
    private void ShowWinScreen()
    {
        endScreen.SetActive(true);
        endScreen.GetComponent<Image>().sprite = winSprite;
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level",1) + 1);
        Time.timeScale = 0;
    }
    public void ChangeWeaponTypeUI(int i)
    {
        weaponTypeImage[0].sprite = weaponTypeSprites[i];
        weaponTypeImage[1].sprite = weaponTypeSprites[(i+1)%weaponTypeSprites.Length];
        weaponTypeImage[2].sprite = weaponTypeSprites[(i+2)%weaponTypeSprites.Length];
    }
    private IEnumerator SpawnEnemyWave()
    {
        int numberOfEnemies = level*2;
        for(int i = 0; i < numberOfEnemies; i++)
        {
            CheckIfSpawnCollectable();
            SpawnEnemy();
            yield return new WaitForSeconds(Random.Range(timeBtwEnemiesBound[0],timeBtwEnemiesBound[1]));
        }
        spawnWaveEnemyTimer = Random.Range(timeBtwEnemiesWaveBound[0],timeBtwEnemiesWaveBound[1]);
    }
    private void CheckIfSpawnCollectable()
    {
        int startIndex = PlayerPrefs.GetInt("weapon",0) >= 6 ? 1 : 0;
        if(Random.Range(0f,1f) <= 0.2f)
        {
            Instantiate(collectablePrefab[Random.Range(startIndex,collectablePrefab.Length)], new Vector3(12, Random.Range(yBounds[0],yBounds[1]),0), Quaternion.identity);
        }
    }
    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, new Vector3(12, Random.Range(yBounds[0],yBounds[1]),0), Quaternion.identity);
        enemy.GetComponent<AIEnemyBasicEngine>().isZigZaging = Random.Range(0f,1f) <= probabilityOfZigZag;
    }

    private void OnDestroy()
    {
        DeathEvent.OnDeath -= HandleDeath;
    }
    private void HandleDeath(GameObject deadObject)
    {
        if(deadObject.tag == "Player")
        {
            endScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else if(deadObject.tag == "Enemy")
        {
            ChangeScoreText(scoreEarnWhenKillingEnemy);
        }
    }
    public void ChangeScoreText(int scoreEarned)
    {
        scoreText.text = (int.Parse(scoreText.text) + scoreEarned).ToString();
    }
    public void CollectItem(string itemType, float value)
    {
        switch(itemType)
        {
            case "energie":
                bulletGun.GainEnergie(value);
                break;
            case "energieMax":
                bulletGun.GainMaxEnergie(value);
                break;
            case "health":
                playerAvatar.GainHealth(value);
                break;
            case "combo":
                Debug.Log("Combo");
                break;
            case "weapon":
                PlayerPrefs.SetInt("weapon", PlayerPrefs.GetInt("weapon") + 1);
                SetWeaponUpgradeText();
                break;
        }
    }

}
