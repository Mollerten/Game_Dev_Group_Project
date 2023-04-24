using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GM : MonoBehaviour
{
    //private bool paused = false;
    private InputHandler _input;
    private GameObject pauseMenu;

    void Awake()
    {
        pauseMenu = GameObject.FindWithTag("PauseMenu");
        pauseMenu.SetActive(false);
        _input = GameObject.FindWithTag("Player").GetComponentInChildren<InputHandler>();
    }

    void Update()
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

    public void Unpause()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Debug.Log("Game Unpaused");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
        Debug.Log("Game Paused");
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
