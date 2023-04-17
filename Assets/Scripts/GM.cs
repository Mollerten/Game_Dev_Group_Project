using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GM : MonoBehaviour
{
    //private bool paused = false;
    public InputHandler _input;
    public PlayerInput playerInput;

    [SerializeField]
    private GameObject pauseMenu;

    void Update()
    {
        if (!pauseMenu.activeInHierarchy && _input.Pause)
        {
            Time.timeScale = 0;
            //playerInput.SwitchCurrentActionMap("UI");
            pauseMenu.SetActive(true);
            //InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
            Debug.Log("Game Paused");
        }
        else if (pauseMenu.activeInHierarchy && _input.Pause)
        {
            Time.timeScale = 1;
            //playerInput.SwitchCurrentActionMap("Player");
            pauseMenu.SetActive(false);
            //InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInFixedUpdate;
            Debug.Log("Game Unpaused");
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
