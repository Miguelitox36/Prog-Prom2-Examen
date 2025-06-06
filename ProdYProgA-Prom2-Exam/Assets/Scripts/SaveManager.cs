using UnityEngine;
using System.IO; 

public class SaveManager : MonoBehaviour
{
    private string saveFilePath; 

    void Awake()
    {        
        saveFilePath = Path.Combine(Application.persistentDataPath, "game_save.json");
        Debug.Log($"Save path: {saveFilePath}");
    }

    public void LoadGame()
    {
        try
        {
            if (!File.Exists(saveFilePath))
            {               
                Debug.LogWarning($"Save file not found at: {saveFilePath}. Starting new game.");               
                return;
            }

            string content = File.ReadAllText(saveFilePath);
            Debug.Log($"Game loaded successfully from: {saveFilePath}. Content (simulated): {content}");           
        }
        catch (FileNotFoundException ex)
        {            
            Debug.LogError($"Error loading game (File not found): {ex.Message}");
            
        }
        catch (IOException ex)
        {
           
            Debug.LogError($"I/O error loading game: {ex.Message}");
        }
        catch (System.Exception ex)
        {           
            Debug.LogError($"An unexpected error occurred while loading the game: {ex.Message}");
        }
    }

    public void SaveGame(string gameData) 
    {
        try
        {
            File.WriteAllText(saveFilePath, gameData);
            Debug.Log($"Game saved successfully to: {saveFilePath}");
        }
        catch (IOException ex)
        {
            Debug.LogError($"I/O error saving game: {ex.Message}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"An unexpected error occurred while saving the game: {ex.Message}");
        }
    }
}