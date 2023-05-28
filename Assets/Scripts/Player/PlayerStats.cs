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
    [SerializeField] private TextMeshProUGUI statText;

    //tester 
    public int skillPoints; 

    // Start is called before the first frame update
    void Start()
    {
        xpReq = GetXPReq(playerLevel + 1);
    }

    // Update is called once per frame
    void Update()
    {
        xpBarFill.fillAmount = playerLevelXp / (GetXPReq(playerLevel + 1) - GetXPReq(playerLevel));
        levelText.text = $"{playerLevel + 1}";
        statText.text = $"Total xp: {playerXP}xp\n" +
            $"Current xp: {playerLevelXp}xp\n" +
            $"Total xp requirement for next level: {string.Format("{0:0.00}", xpReq)}xp\n" +
            $"Xp until next level: {string.Format("{0:0.00}", xpReq - playerXP)}xp";
    }

    private float GetXPReq(int level)
    {
        return Mathf.Pow(level / 0.07f, 2);
    }

    public void AddXP(int xp)
    {
        //float xp = _xp;
        playerXP += xp;
        playerLevelXp += xp;
        if (xpReq - playerXP <= 0) // level up
        {
            playerLevel++;
            playerLevelXp = playerXP - xpReq;
            xpReq = GetXPReq(playerLevel + 1);
            //test 
            skillPoints++;
            gameObject.GetComponent<PlayerUpgrades>().upgradeMenu();
            Debug.Log("skill point: " + skillPoints);

        }       
    }

    public int GetLevel()
    {
        return playerLevel;
    }

    public int GetSkillPoints()
    {
        return skillPoints;
    }

    public void SetSkillPoints(int skillPoints)
    {
        this.skillPoints = skillPoints;
    }
}
