using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int spawnMax = 10;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float goblinInterval = 5f;

    [ReadOnly] public int enemiesAlive = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy(goblinInterval, enemyPrefab));
    }

    void Update()
    {
        int tmp = 0;
        foreach (Transform child in transform)
        {
            if (!child.gameObject.GetComponent<EnemyHealth>().IsEnemyDead()) tmp++;
        }
        enemiesAlive = tmp;
    }

    IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        if (!(enemiesAlive >= spawnMax))
        {
            GameObject mew = Instantiate(enemy, transform.position, Quaternion.identity, transform);
        }
        yield return new WaitForSeconds(interval);
        StartCoroutine(SpawnEnemy(interval, enemy));
    }
}
