using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GM : MonoBehaviour
{

    [Header("Menu")]
    [SerializeField] TMP_Dropdown difficultyDropdown;
    [Header("InGame")]
    [ReadOnly] public int enemyLevelScale = 0;
    [ReadOnly] public int enemiesAlive;
    public int maxEnemies;


    public AudioClip[] audioClips = new AudioClip[2];

    private AudioSource audioSource;
    private Dictionary<float, int> difficultyDict = new();
    private AudioClip gameOver;
    private InputHandler _input;
    private GameObject pauseMenu;
    private GameObject levelUpMenu;
    private GameObject deathScreen;
    private TextMeshProUGUI timerText;
    private int playerLevel;
    private int minutes = 0;
    private int seconds = 0;
    private float timer = 0;
    public bool paused = false;
    private static float difficultyScale;
    private string time;
    private bool playerIsDead = false;
    

    void Awake()
    {
        difficultyDict.Add(1, 0);    // Easy
        difficultyDict.Add(1.5f, 1); // Medium
        difficultyDict.Add(2, 2);    // Hard
        if (SceneManager.GetActiveScene().name == "Menu") difficultyScale = 1;
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            gameOver = Resources.Load("GameOver") as AudioClip;
            gameOver.LoadAudioData();
            deathScreen = GameObject.FindWithTag("DeathScreen");
            
            pauseMenu = GameObject.FindWithTag("PauseMenu");
            
            _input = GameObject.FindWithTag("Player").GetComponentInChildren<InputHandler>();
            playerLevel = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerStats>().GetLevel();
            timerText = GameObject.FindWithTag("Timer").GetComponent<TextMeshProUGUI>();
            levelUpMenu = GameObject.FindGameObjectWithTag("LevelUpMenu");
            
            audioSource = GetComponent<AudioSource>();
            StartCoroutine(ChangeAudioClip());
        }
        paused = false;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            levelUpMenu.SetActive(false);
            pauseMenu.SetActive(false);
            deathScreen.SetActive(false);  
        }
        

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            UpdateEnemiesAlive();
            if (!paused && !playerIsDead)
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
                if (!pauseMenu.activeInHierarchy && _input.Pause && !playerIsDead)
                {
                    Pause();
                }
                else if (pauseMenu.activeInHierarchy && _input.Pause && !playerIsDead)
                {
                    Unpause();
                }
            } 
        }
    }

    public bool CanEnemiesSpawn()
    {
        return enemiesAlive < maxEnemies;
    }

    private void UpdateEnemiesAlive()
    {
        int enemyCount = 0;
        foreach(GameObject spawnerObj in GameObject.FindGameObjectsWithTag("ESpawner")) 
        {
            enemyCount += spawnerObj.GetComponent<EnemySpawner>().enemiesAlive;
        }
        enemiesAlive = enemyCount;
    }

    public void GameOver()
    {
        if (!playerIsDead)
        {
            playerIsDead = true;
            audioSource.clip = gameOver;
            audioSource.loop = false;
            audioSource.volume = 1.0f;
            audioSource.Play();
            deathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            difficultyDropdown.SetValueWithoutNotify(difficultyDict[difficultyScale]); 
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
        SceneManager.LoadScene("Level1Scene", LoadSceneMode.Single);
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
    }


    IEnumerator ChangeAudioClip()
    {
        audioSource.volume = 0.2f;
        audioSource.clip = audioClips[0];
        audioSource.Play();
        yield return new WaitForSecondsRealtime(audioSource.clip.length);

        audioSource.clip = audioClips[1];
        audioSource.Play();
        audioSource.loop = true;
    }

    public void ExitGame(bool hardQuit = false)
    {
        if (SceneManager.GetActiveScene().name == "Menu" || hardQuit)
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
