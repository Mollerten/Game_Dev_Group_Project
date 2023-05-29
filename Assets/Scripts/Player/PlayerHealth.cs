using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

    [SerializeField] private Image healthBarFill;
    [SerializeField] private Image healthBarLoss;
    [SerializeField] private TextMeshProUGUI healthBarText;

    private float actualValue;
    private float startValue;
    private float displayValue = 1f;
    private float timer = 0f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth = 100;
        startValue = actualValue = currentHealth / (float)maxHealth;
    }

    void Update()
    {
        timer += Time.deltaTime * 2;
        displayValue = Mathf.Lerp(startValue, actualValue, timer);
        healthBarLoss.fillAmount = displayValue; 
    }
    
    
    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        actualValue = currentHealth / (float)maxHealth;
        startValue = healthBarLoss.fillAmount;
        healthBarFill.fillAmount = actualValue;
        healthBarText.text = $"{currentHealth}/{maxHealth}";
        timer = 0f;
    }

    public void healthScaling()
    {
        maxHealth = maxHealth + (player.GetComponent<PlayerUpgrades>().health * 25);
        currentHealth = maxHealth;
        actualValue = currentHealth / (float)maxHealth;
        startValue = healthBarLoss.fillAmount;
        healthBarFill.fillAmount = actualValue; 
       
        healthBarText.text = $"{currentHealth}/{maxHealth}";
    }

    
}
