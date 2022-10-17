using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameProgressScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    float levelLength = 60.0f;

    [SerializeField]
    public float bossSpawnTime = 40f;

    private float currentTime = 0.0f;

    private bool bossIsSpawned = false;

    private bool isFinished;
    void Start()
    {
        
    }

    public float GetLevelLength()
    {
        return levelLength;
    }
    
    public float GetCurrentTime()
    {
        return currentTime;
    }

    public bool IsFinished()
    {
        return isFinished;
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<PlayerScript>().GetLifes() > 0)
        {
            currentTime += Time.deltaTime;
        }
        if (!bossIsSpawned && currentTime >= bossSpawnTime)
        {
            SpawnBoss();
        }
        if(currentTime >= levelLength)
        {
            isFinished = true;
        }
    }
    void SpawnBoss()
    {
        bossIsSpawned = true;
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {            
            EnemySpawner spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
            EnemySpawner bossSpawner = GameObject.Find("BigGhostSpawner").GetComponent<EnemySpawner>();
            spawner._maxEnemies = 0;
            bossSpawner._maxEnemies = 3;
        }
    }
}
