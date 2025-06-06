using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using GameJolt.API;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    
    public event Action<string> OnEnemyDefeated;
    public UnityEvent<int> OnPlayerExperienceChanged;
    public event Action<int> OnPlayerHealthChanged;
    public event Action<string> OnGameStateChanged;
    public event Action<float> OnTimerUpdated;            
        
    private List<string> gameEvents = new List<string>();
    private Stack<string> gameStates = new Stack<string>();
    private Queue<string> pendingActions = new Queue<string>();
        
    public int PlayerCurrentHealth = 150;
        
    private float gameTimer = 0f;
    private bool isTimerRunning = false;
    private bool levelCompleted = false;
        
    public float GameTimer => gameTimer;
    public bool IsTimerRunning => isTimerRunning;

    void Awake()
    {
        if (OnPlayerExperienceChanged == null)
            OnPlayerExperienceChanged = new UnityEvent<int>();

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        gameStates.Push("MainMenu");
        gameStates.Push("Playing");

        Debug.Log("GameManager initialized");
    }

    void Start()
    {
        // Iniciar el timer si estamos en la escena de gameplay
        if (SceneManager.GetActiveScene().name == "GamePlayScene")
        {
            StartTimer();
        }
    }

    void Update()
    {
        // Actualizar timer
        if (isTimerRunning)
        {
            gameTimer += Time.deltaTime;
            OnTimerUpdated?.Invoke(gameTimer);
        }

        ProcessPendingActions();
    }

    public void StartTimer()
    {
        gameTimer = 0f;
        isTimerRunning = true;
        levelCompleted = false;
        Debug.Log("Game timer started!");
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        Debug.Log($"Game timer stopped at: {gameTimer:F2} seconds");
    }

    public void CompleteLevel()
    {
        if (!levelCompleted)
        {
            levelCompleted = true;
            StopTimer();
                       
            GameWinData.Save(PlayerCurrentHealth, gameTimer);

            Debug.Log($"Level completed! Time: {gameTimer:F2}s, Final Health: {PlayerCurrentHealth}");

            // Cambiar a escena de victoria
            SceneManager.LoadScene("GameWin");
        }

        int time = Mathf.RoundToInt( gameTimer );
        Scores.Add(time, $"{time} segundos", 1010542, "", success =>
        {
            Debug.Log("¿Score enviado? " + success);
        });

        int fhealth = Mathf.FloorToInt(PlayerCurrentHealth);
        Scores.Add(fhealth, $"{fhealth} de vida", 1010658, "", success =>
        {
            Debug.Log("¿Score enviado? " + success);
        });
    }

    public void NotifyPlayerExperienceChanged(int newExperience)
    {
        OnPlayerExperienceChanged?.Invoke(newExperience);
        Debug.Log($"GameManager notified: Player experience changed to {newExperience}");
    }

    public void DefeatEnemy(string enemyName, int enemyHealth)
    {
        try
        {
            if (enemyName == "Goblin" && enemyHealth <= 0)
            {
                CompleteLevel();
            }
        }
        catch (Exception ex)
        {            
            Console.WriteLine($"Error al derrotar enemigo: {ex.Message}");
        }
    }
    public void UpdatePlayerHealth(int newHealth)
    {
        PlayerCurrentHealth = newHealth;
        OnPlayerHealthChanged?.Invoke(newHealth);
    }
    public void PushState(string newState)
    {
        gameStates.Push(newState);
        Debug.Log($"Game state changed to: {newState}");
        OnGameStateChanged?.Invoke(newState);
    }
    public string PopState()
    {
        if (gameStates.Count > 0)
        {
            string state = gameStates.Pop();
            if (gameStates.Count > 0)
            {
                OnGameStateChanged?.Invoke(gameStates.Peek());
            }
            return state;
        }
        return "Unknown";
    }
    public string GetCurrentState()
    {
        if (gameStates.Count > 0)
        {
            return gameStates.Peek();
        }
        return "Unknown";
    }

    public void ProcessPendingActions()
    {
        while (pendingActions.Count > 0)
        {
            string action = pendingActions.Dequeue();
            Debug.Log($"Processing: {action}");
        }
    }

    public void ResetGame()
    {
        PlayerCurrentHealth = 150;
        gameTimer = 0f;
        isTimerRunning = false;
        levelCompleted = false;        
        gameEvents.Clear();
        
        gameStates.Clear();
        gameStates.Push("MainMenu");
        gameStates.Push("Playing");
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GamePlayScene")
        {
            StartTimer();
        }
    }
}