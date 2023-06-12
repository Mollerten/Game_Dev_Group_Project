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

    IEnumerator Start()
    {
        while (true)
        {
            if (enemiesAlive < spawnMax)
            {
                SpawnEnemy(enemyPrefab);
                yield return new WaitForSeconds(golemInterval);
            }
            else
            {
                yield return new WaitUntil(delegate { return enemiesAlive == 0; });
            }
        }
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

    void SpawnEnemy(GameObject enemy)
    {
        GameObject mew = Instantiate(enemy, transform.position, Quaternion.identity, transform);
        mew.GetComponent<GolemController>().SetWaypointGroup(WaypointGroup);
    }
}
