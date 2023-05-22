using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public InputHandler _input;
    public float minDamage = 5f;
    public float maxDamage = 10f;
    public float range = 0.7f;
    public Transform direction;
    //public float critMult = 1.5f;
    //public float critChance = 10f;

    void Update()
    {
        if (_input.Fire)
        {
            HitCheck(direction.eulerAngles);
        }
    }

    void HitCheck(Vector3 euler)
    {
        float elevation = euler.x * Mathf.Deg2Rad;
        float heading = euler.y * Mathf.Deg2Rad;
        Vector3 orientation = new(Mathf.Cos(elevation) * Mathf.Sin(heading), Mathf.Sin(elevation), Mathf.Cos(elevation) * Mathf.Cos(heading));

        Ray rayFrom = new(transform.position, orientation);
        if (Physics.Raycast(rayFrom, out RaycastHit hit, range, 1 << 3))
        {
            Debug.Log(hit.collider.gameObject.name);
            if ((bool)!hit.collider.GetComponent<EnemyHealth>()?.IsEnemyDead())
            {
                int damage = Mathf.RoundToInt(Random.Range(minDamage, maxDamage));
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
    }
}
