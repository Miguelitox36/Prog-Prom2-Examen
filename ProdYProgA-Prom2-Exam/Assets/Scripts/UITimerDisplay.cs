using UnityEngine;
using TMPro;

public class UITimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnTimerUpdated += UpdateTimerDisplay;
        }
        else
        {
            Debug.LogWarning("UITimerDisplay: GameManager.Instance is null in OnEnable.");
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnTimerUpdated -= UpdateTimerDisplay;
        }
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            UpdateTimerDisplay(GameManager.Instance.GameTimer);
        }
        else
        {
            Debug.LogError("UITimerDisplay: GameManager.Instance is null in Start.");
        }
    }

    private void UpdateTimerDisplay(float currentTime)
    {
        if (timerText != null)
        {            
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);
            int centiseconds = Mathf.FloorToInt((currentTime * 100f) % 100f);
            timerText.text = $"Tiempo: {minutes:00}:{seconds:00}.{centiseconds:00}";
        }
    }
}