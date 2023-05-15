using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GM : MonoBehaviour
{
    public int enemyLevelScale = 0;

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
