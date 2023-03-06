using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    private bool paused = false;

    [SerializeField]
    private GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                //Cursor.lockState = CursorLockMode.None;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                //Cursor.lockState = CursorLockMode.Locked;
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }

            paused = !paused;
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
