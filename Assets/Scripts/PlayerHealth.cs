using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    [SerializeField] private Image healthBarFill;
    [SerializeField] private Image healthBarLoss;
    [SerializeField] private TextMeshProUGUI healthBarText;
    private float actualValue;
    private float startValue;
    private float displayValue = 1f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        startValue = actualValue = currentHealth / (float)maxHealth;
    }

    void Update()
    {
        timer += Time.deltaTime * 2;
        displayValue = Mathf.Lerp(startValue, actualValue, timer);
        healthBarLoss.fillAmount = displayValue;
    }

    public void Test()
    {
        currentHealth -= 10;
        actualValue = currentHealth / (float)maxHealth;
        startValue = healthBarFill.fillAmount;
        healthBarFill.fillAmount = actualValue;
        healthBarText.text = $"{currentHealth}/{maxHealth}";
        timer = 0f;
    }

    public void PlayerTakeDamage(int damage)
    {
        currentHealth -= damage;
        actualValue = currentHealth / (float)maxHealth;
        startValue = healthBarLoss.fillAmount;
        healthBarFill.fillAmount = actualValue;
        healthBarText.text = $"{currentHealth}/{maxHealth}";
        timer = 0f;
    }
}
