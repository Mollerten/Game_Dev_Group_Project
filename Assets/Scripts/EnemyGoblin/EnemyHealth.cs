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
    [SerializeField] private TextMeshProUGUI levelText;
    private float actualValue;
    private float startValue;
    private float displayValue = 1f;
    private float timer = 0f;
    private float attackTimer = 10f;
    private float hitCooldown;
    private int level;
    private int xp;
    

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        isEnemyDead = false;
        currentHealth = maxHealth;
        startValue = actualValue = currentHealth / (float)maxHealth;
        SetKinematic(true);

        level = GameObject.FindWithTag("GameController").GetComponent<GM>().enemyLevelScale;
        if (level < 1) level = 1;
        xp = level * 20;

        levelText.text = $"{level}";
    }

    void Update()
    {
        // check for player weapon and get attack cooldown
        hitCooldown = player.GetComponentInChildren<WeponController>().swordAttackCooldown;
        timer += Time.deltaTime * 2;
        displayValue = Mathf.Lerp(startValue, actualValue, timer);
        healthBarLoss.fillAmount = displayValue;
        attackTimer += Time.deltaTime;

        healthBar.transform.LookAt(player.transform.position);


        // man I wish i knew a better way to do this
        if (player.GetComponent<PlayerStats>().GetLevel() - level <= -3)
        {
            levelText.color = new Color(23, 135, 0);
        }
        else if (player.GetComponent<PlayerStats>().GetLevel() - level == 0)
        {
            levelText.color = new Color(255, 255, 255);
        }
        else if (player.GetComponent<PlayerStats>().GetLevel() - level >= 3)
        {
            levelText.color = new Color(255, 115, 0);
        }
        else if (player.GetComponent<PlayerStats>().GetLevel() - level >= 6)
        {
            levelText.color = new Color(255, 10, 0);
        }
    }

    public void TakeDamage(int damage)
    {
        if (attackTimer > 0.2f)
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
                GetComponent<Collider>().enabled = false;
                GetComponent<Rigidbody>().AddExplosionForce(1000, transform.position, 1);
                // Death animation goes here OR activate ragdoll and disable animator
                isEnemyDead = true;
                player.GetComponent<PlayerStats>().AddXP(xp);
                Destroy(gameObject, 10);
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
