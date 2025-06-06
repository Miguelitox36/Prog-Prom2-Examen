using UnityEngine;
using TMPro;

public class UIHealthDisplay : MonoBehaviour
{
    public TextMeshProUGUI healthText;    

    private void OnEnable()
    {        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerHealthChanged += UpdateHealthDisplay;            
        }
        else
        {
            Debug.LogWarning("UIHealthDisplay: GameManager.Instance is null in OnEnable. Subscription skipped.");
        }
    }

    private void OnDisable()
    {
       
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerHealthChanged -= UpdateHealthDisplay;           
        }
        else
        {
            Debug.LogWarning("UIHealthDisplay: GameManager.Instance is null in OnDisable. Unsubscription skipped.");
        }
    }

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            UpdateHealthDisplay(GameManager.Instance.PlayerCurrentHealth);           
        }
        else
        {
            Debug.LogError("UIHealthDisplay: GameManager.Instance is null in Start. UI may not update correctly.");
        }
    }

    private void UpdateHealthDisplay(int newHealth)
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {newHealth}";
        }
    }        
}