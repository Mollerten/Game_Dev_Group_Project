using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DashCooldownUI : MonoBehaviour
{
    [SerializeField] private Image cooldownBackground;
    [SerializeField] private TMP_Text textCooldown;
    [SerializeField] private PlayerMovement playerMovement;

    void Start()
{
    cooldownBackground.fillAmount = 0.0f;
    textCooldown.gameObject.SetActive(false);
}
void Update()
{
    float cooldownTime = playerMovement.dashCooldown;
    float cooldownTimer = playerMovement.dashCooldownTimer;

    if (cooldownTimer > 0)
    {
        textCooldown.gameObject.SetActive(true);
        textCooldown.text = Mathf.CeilToInt(cooldownTimer).ToString();
        cooldownBackground.fillAmount = cooldownTimer / cooldownTime;
    }
    else
    {
        textCooldown.gameObject.SetActive(false);
        cooldownBackground.fillAmount = 0.0f;
    }
}

}
