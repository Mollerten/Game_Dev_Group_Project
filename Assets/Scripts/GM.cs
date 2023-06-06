using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{

    [Header("Menu")]
    [SerializeField] TMP_Dropdown difficultyDropdown;
    [Header("InGame")]
    [ReadOnly] public int enemyLevelScale = 0;

    [SerializeField] public GameObject[] buttons;
    [SerializeField] public string[] buttonTexts;
    private InputHandler _input;
    private GameObject pauseMenu;
    private TextMeshProUGUI timerText;
    private int playerLevel;
    private int minutes = 0;
    private int seconds = 0;
    private float timer = 0;
    public bool paused = false;
    private static float difficultyScale;
    private string time;
    private GameObject levelUpMenu;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Menu") difficultyScale = 1;
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            pauseMenu = GameObject.FindWithTag("PauseMenu");
            pauseMenu.SetActive(false);
            _input = GameObject.FindWithTag("Player").GetComponentInChildren<InputHandler>();
            playerLevel = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerStats>().GetLevel();
            timerText = GameObject.FindWithTag("Timer").GetComponent<TextMeshProUGUI>();
            levelUpMenu = GameObject.FindGameObjectWithTag("LevelUpMenu");
            levelUpMenu.SetActive(false);
        }
        paused = false;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            if (!paused)
            {
                timer += Time.deltaTime;
                minutes = Mathf.FloorToInt(timer / 60F);
                seconds = Mathf.FloorToInt(timer - minutes * 60);
                enemyLevelScale = Mathf.Max(Mathf.FloorToInt((Mathf.Pow(timer / 60F, 0.7f) + (playerLevel / 4f)) * Mathf.Pow(difficultyScale, 1.5f)), 1);
                time = string.Format("{0:00}:{1:00}", minutes, seconds);
                timerText.text = time;
            }

            if (!levelUpMenu.activeInHierarchy)
            {
                if (!pauseMenu.activeInHierarchy && _input.Pause)
                {
                    Pause();
                }
                else if (pauseMenu.activeInHierarchy && _input.Pause)
                {
                    Unpause();
                }
            } 
        }
    }

    public void SetDifficulty()
    {
        // Easy(1)/Medium(1.5)/Hard(2)
        switch (difficultyDropdown.value)
        {
            case 0: 
                difficultyScale = 1; break;
            case 1:
                difficultyScale = 1.5f; break;
            case 2:
                difficultyScale = 2; break;
            default:
                break;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
    }

    public void Unpause(bool levelUp = false)
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (!levelUp)
        {
            pauseMenu.SetActive(false);
        }
        paused = false;
    }

    public void Pause(bool levelUp = false)
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (!levelUp)
        {
            pauseMenu.SetActive(true);
        }
        paused = true;
        // UpdateLevelUpChoices();
    }

    public void UpdateLevelUpChoices()
    {
        foreach (var button in buttons)
        {
            //button.GetComponent<Button>().onClick.RemoveAllListeners();
            //get onClick from arr
            button.GetComponentInChildren<TextMeshProUGUI>().text = $"{buttonTexts[Random.Range(0,buttonTexts.Length)]}";
            //button.GetComponent<Button>().onClick.AddListener(ExitGame);
            //button.GetComponent<Button>().onClick.AddListener(delegate { ExitGame();});
        }
    }

    public void ExitGame()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		    Application.Quit();
#endif 
        }
        else
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
