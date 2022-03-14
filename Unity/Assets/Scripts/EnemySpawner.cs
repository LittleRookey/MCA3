using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    
    public GameObject enemy;

    public static bool isGameOver = false;
    public bool nextRound = false;
    public int stage;

    public int enemySpawnNum=0;
    public int maxEnemyNum = 3;
    public int killedEnemyNum = 0;
    public float enemySpawnInterval = 2;
    public List<GameObject> enemies;

    public Text stageText;
    public Text enemyLeftText;
    public Text hpText;
    public Text playerDeadText;
    public Text levelBeatText;

    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Awake()
    {
       if (Instance ==  null)
        {
            Instance = this;
        }
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        stage = 1;
    }

    private void Start()
    {
        StartGame();
    }
    void StartGame()
    {
        enemySpawnNum = 0;
        killedEnemyNum = 0;
        
        StartCoroutine(SpawnEnemies());
    }

    public void TurnOffLevelBeatText()
    {
        playerDeadText.gameObject.SetActive(false);
    }

    IEnumerator Turn()
    {
        levelBeatText.gameObject.SetActive(true);
        //AudioSource aud =  levelBeatText.gameObject.GetComponent<AudioSource>();
        //aud.PlayOneShot(aud.clip);
        yield return new WaitForSeconds(1.5f);
        levelBeatText.gameObject.SetActive(false);
    }
    public IEnumerator SpawnEnemies()
    {
        
        enemies.Clear();
        WaitForSeconds two = new WaitForSeconds(enemySpawnInterval);
        // 6 -30, 0 50
        while(true)
        {
            if (enemySpawnNum >= maxEnemyNum)
            {
                break;
            }
            enemySpawnNum += 1;
            yield return two;
            
            Vector3 pos = new Vector3(Random.Range(-15f, 0f), 12f, Random.Range(-40, -30));
            var enem = Instantiate(enemy, pos, Quaternion.identity);
            enemies.Add(enem);
            UpdateUI();
        }
    }
    
    public void UpdateUI()
    {
        hpText.text = $"{playerHealth.currentHealth} / {playerHealth.maxHealth}";
        enemyLeftText.text = $"enemyLeft: {maxEnemyNum - killedEnemyNum}";
        stageText.text = $"Stage {stage}";
    }

    public void OnPlayerDead()
    {
        playerDeadText.gameObject.SetActive(true);
    }
    public void NextRound()
    {
        StartCoroutine(Turn());
        maxEnemyNum *= 2;
        stage += 1;
        StartGame();
    }
    public void enemyAllDead()
    {
        nextRound = maxEnemyNum - killedEnemyNum <= 0;
    }

    public void RestartRound()
    {
        StopAllCoroutines();
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i]);
        }
        enemies.Clear();
        playerDeadText.gameObject.SetActive(false);
        stage = 1;
        maxEnemyNum = 3;
        enemySpawnNum = 0;
        killedEnemyNum = 0;
        playerHealth.Reset();
        UpdateUI();
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        if (nextRound)
        {
            NextRound();
            nextRound = false;
        }
        if (isGameOver)
        {
            Invoke("RestartRound", 2f);
            isGameOver = false;
        }
    }
}
