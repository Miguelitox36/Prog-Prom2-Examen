using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameWinUI : MonoBehaviour
{
    public TextMeshProUGUI completionTimeText;
    public TextMeshProUGUI finalHealthText;       

    public GameObject playAgainButton;
    public GameObject menuButton;

    void Start()
    {
        (int finalHealth, float completionTime) = GameWinData.Load();

        // Mostrar tiempo de finalización
        completionTimeText.text = "Tiempo de Finalización: " + completionTime.ToString("F2") + "s";

        // Mostrar vida final
        finalHealthText.text = "Vida Final: " + finalHealth;
                            
        // Configurar botones
        if (playAgainButton != null)
        {
            playAgainButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(PlayAgain);
        }
        if (menuButton != null)
        {
            menuButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(GoToMenu);
        }
                
    }

    public void PlayAgain()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGame();
        }               
        GameWinData.Clear();

        SceneManager.LoadScene("GamePlayScene");
    }

    public void GoToMenu()
    {        
        GameWinData.Clear();

        SceneManager.LoadScene("MenuScene");
    }
}