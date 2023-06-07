using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSpawner : MonoBehaviour
{
    [SerializeField] private int spawnMax = 1;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float golemInterval = 60f;
    [ReadOnly] public int enemiesAlive = 0;
    public string WaypointGroup;

    void Start()
    {
        StartCoroutine(SpawnEnemy(golemInterval, enemyPrefab));
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
        if (!(enemiesAlive >= spawnMax) &&
            Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) > 30 &&
            Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) < 60)
        {
            GameObject mew = Instantiate(enemy, transform.position, Quaternion.identity, transform);
            mew.GetComponent<GolemController>().SetWaypointGroup(WaypointGroup);
        }
        yield return new WaitForSeconds(interval);
        StartCoroutine(SpawnEnemy(interval, enemy));
    }
}
