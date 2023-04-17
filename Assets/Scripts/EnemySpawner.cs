using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] 
    private GameObject enemyPrefab;

    [SerializeField]
    private float goblinInterval = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy(goblinInterval, enemyPrefab));
    }

    IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f,5), Random.Range(-6f,6),0), Quaternion.identity);
            StartCoroutine(SpawnEnemy(interval, enemy));
        
    }
}
