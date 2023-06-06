using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public int swordlevel;
    public int axeLevel;
    public int fireballLevel;
    public int frostboltLevel;
    public int health;
    public int speed;
    
    public GameObject player;
    private GameObject levelUpMenu;

    private GM GM;


    [SerializeField] private GameObject[] buttons;
    [SerializeField] private TextMeshProUGUI levelUpText;
    [SerializeField] private string[] buttonTexts;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GM>();
        levelUpMenu = GameObject.FindGameObjectWithTag("LevelUpMenu");
    }



    private void CloseMenu()
    {
        GM.Unpause(true);
        levelUpMenu.SetActive(false);
    }

    public void upgradeMenu()
    {
       
            GM.Pause(true);
            levelUpMenu.SetActive(true);


    
        
        // randomly select a button to be the upgrade
        for(int i = 0; i < 3; i++)
        {

            buttons[i].GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"{buttonTexts[Random.Range(0,buttonTexts.Length)]}";
            levelUpText.text = $"Lvl {player.GetComponent<PlayerStats>().GetLevel()} -> {player.GetComponent<PlayerStats>().GetLevel() + 1}";
            upgradeTree(i);
        }


    }

    private void upgradeTree(int i)
    {
        buttons[i].GetComponent<UnityEngine.UI.Button>().onClick.RemoveAllListeners();

        if (buttons[i].GetComponentInChildren<TextMeshProUGUI>().text.Contains("Fireball"))
        {
            buttons[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UpgradeFireball);
        }
        if (buttons[i].GetComponentInChildren<TextMeshProUGUI>().text.Contains("Frostbolt"))
        {
            buttons[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UpgradeFrostbolt);
        }
        if (buttons[i].GetComponentInChildren<TextMeshProUGUI>().text.Contains("Sword"))
        {
            buttons[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UpgradeSword);
        }
        if (buttons[i].GetComponentInChildren<TextMeshProUGUI>().text.Contains("Axe"))
        {
            buttons[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UpgradeAxe);
        }
        if (buttons[i].GetComponentInChildren<TextMeshProUGUI>().text.Contains("Health"))
        {
            buttons[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UpgradeHealth);
        }
        if (buttons[i].GetComponentInChildren<TextMeshProUGUI>().text.Contains("Speed"))
        {
            buttons[i].GetComponent<UnityEngine.UI.Button>().onClick.AddListener(UpgradeSpeed);
        }

    }

    public void UpgradeFireball()
    {
            fireballLevel++;
        
        CloseMenu();
    }

    public void UpgradeFrostbolt()
    {
       
            frostboltLevel++;
           
        
        CloseMenu();
    }

    public void UpgradeSword()
    {
        
            swordlevel++;
            
        
        CloseMenu();
    }

    public void UpgradeAxe()
    {
        
            axeLevel++;

        
        CloseMenu();
    }

    public void UpgradeHealth()
    {
        
            health++;
            gameObject.GetComponent<PlayerHealth>().healthScaling();
        
        CloseMenu();
    }

    public void UpgradeSpeed()
    {
        
            speed++;
       
        CloseMenu();
    }

    
    
    
}
