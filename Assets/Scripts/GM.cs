using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    [ReadOnly] public int enemyLevelScale = 0;

    [SerializeField] private GameObject[] buttons;
    [SerializeField] private string[] buttonTexts;
    private InputHandler _input;
    private GameObject pauseMenu;
    private TextMeshProUGUI timerText;
    private int playerLevel;
    private int minutes = 0;
    private int seconds = 0;
    private float timer = 0;
    private bool started = true; // would change value upon starting/quitting the game, not needed currently as the game is always started
    private bool paused = false;
    private float difficultyScale = 1; // hardcoded for now, would need the player choosing Easy(1)/Medium(1.5)/Hard(2)
    private string time;

    void Awake()
    {
        pauseMenu = GameObject.FindWithTag("PauseMenu");
        pauseMenu.SetActive(false);
        _input = GameObject.FindWithTag("Player").GetComponentInChildren<InputHandler>();
        playerLevel = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerStats>().GetLevel();
        timerText = GameObject.FindWithTag("Timer").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (started && !paused)
        {
            timer += Time.deltaTime;
            minutes = Mathf.FloorToInt(timer / 60F);
            seconds = Mathf.FloorToInt(timer - minutes * 60);
            enemyLevelScale = Mathf.Max(Mathf.FloorToInt((Mathf.Pow(minutes, 0.7f) + (playerLevel / 4f)) * Mathf.Pow(difficultyScale, 1.5f)), 1);
            time = string.Format("{0:00}:{1:00}", minutes, seconds);
            timerText.text = time;
        }
        
        if (!pauseMenu.activeInHierarchy && _input.Pause)
        {
            Pause();
        }
        else if (pauseMenu.activeInHierarchy && _input.Pause)
        {
            Unpause();
        }
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        paused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        paused = true;
        UpdateLevelUpChoices();
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
}
