using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    private float _spawnCooldown = 1.0f;

    [SerializeField]
    public int _maxEnemies = 5;

    private float lastTimeSpawned = 0.0f;

    public GameObject Enemy;

    float yMin, yMax;
    float xMin, xMax;
    // Start is called before the first frame update
    void Start()
    {
        Transform bb = transform.GetChild(0);
        Vector3 max = bb.gameObject.GetComponent<SpriteRenderer>().bounds.max;
        Vector3 min = bb.gameObject.GetComponent<SpriteRenderer>().bounds.min;
        yMin = min.y;
        yMax = max.y;
        xMin = min.x;
        xMax = max.x;
    }

    // Update is called once per frame
    void Update()
    {
        
        lastTimeSpawned += Time.deltaTime;
        if(lastTimeSpawned > _spawnCooldown)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < _maxEnemies)
            {
                float y = Random.Range(yMin, yMax);
                float x = Random.Range(xMin, xMax);
                Vector3 spawnPos = new Vector3(transform.position.x, y, 0.0f);
                GameObject spawnedEnemy;
                spawnedEnemy = Instantiate(Enemy, spawnPos, Quaternion.identity);
                spawnedEnemy.transform.parent = transform;
                spawnedEnemy.layer = LayerMask.NameToLayer("Enemies");
                spawnedEnemy.tag = "Enemy";
                lastTimeSpawned = 0.0f;
            }
        }
    }
}
