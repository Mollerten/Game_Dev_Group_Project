using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth;
    
    private bool isEnemyDead;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Image healthBarLoss;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject player;
    private float actualValue;
    private float startValue;
    private float displayValue = 1f;
    private float timer = 0f;
    private float attackTimer = 10f;
    private float hitCooldown;
    

    void Start()
    {
        isEnemyDead = false;
        currentHealth = maxHealth;
        startValue = actualValue = currentHealth / (float)maxHealth;
        SetKinematic(true);
    }

    void Update()
    {
        // check for player weapon and get attack cooldown
        hitCooldown = player.GetComponentInChildren<WeponController>().AttackCooldown;
        timer += Time.deltaTime;
        displayValue = Mathf.Lerp(startValue, actualValue, timer);
        healthBarLoss.fillAmount = displayValue;
        attackTimer += Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (attackTimer > hitCooldown)
        {
            currentHealth -= damage;
            actualValue = currentHealth / (float)maxHealth;
            startValue = healthBarFill.fillAmount;
            healthBarFill.fillAmount = actualValue;
            timer = 0f;
            attackTimer = 0f;
    
            if (currentHealth <= 0 && !isEnemyDead)
            {         
                SetKinematic(false);
                GetComponent<Animator>().enabled = false;
                healthBar.SetActive(false);
                // Death animation goes here OR activate ragdoll and disable animator
                isEnemyDead = true;
                Destroy(gameObject, 10);
                Debug.Log($"EnemyHealth is dead? {isEnemyDead}");
            }
        }
    }

    public bool IsEnemyDead()
    {
        return isEnemyDead;
    }

    void SetKinematic(bool newValue)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
             rb.isKinematic = newValue;
        }
    }
    
}
