using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public int weapon1Level;
    public int weapon2Level;
    public int spellELevel;
    public int spellQLevel;
    public int skillPoints;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        skillPoints = player.GetComponent<PlayerStats>().GetSkillPoints();

    }

   void upgradeItem(int item)
   {
        if (skillPoints > 0)
        {
            switch (item)
            {
                case 1:
                    weapon1Level++;
                    skillPoints--;
                    break;
                case 2:
                    weapon2Level++;
                    skillPoints--;
                    break;
                case 3:
                    spellELevel++;
                    skillPoints--;
                    break;
                case 4:
                    spellQLevel++;
                    skillPoints--;
                    break;
            }
        }
   }
}
