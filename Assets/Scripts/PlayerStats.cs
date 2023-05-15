using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private int playerLevel = 0;
    private float playerLevelXp = 0;
    private float playerXP = 0;
    private float xpReq;

    [SerializeField] private Image xpBarFill;
    [SerializeField] private TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        xpReq = GetXPReq(playerLevel + 1);
    }

    // Update is called once per frame
    void Update()
    {
        xpBarFill.fillAmount = playerLevelXp / xpReq;
        levelText.text = $"{playerLevel}";
    }

    private float GetXPReq(int level)
    {
        return Mathf.Pow(level / 0.07f, 2);
    }

    public void AddXP(int xp)
    {
        playerXP += xp;
        playerLevelXp += xp;
        if (playerLevelXp / xpReq >= 1)
        {
            playerLevel++;
            playerLevelXp = playerXP - xpReq;
            xpReq = GetXPReq(playerLevel + 1);
        }
    }

    public int GetLevel()
    {
        return playerLevel;
    }
}
