using UnityEngine;
using GameJolt.UI;
using UnityEngine.SceneManagement;
using GameJolt.API;

public class MainSceneManager : MonoBehaviour
{
    void Start()
    {
        GameJoltUI.Instance.ShowSignIn((success) =>
        {
            if (success)
            {
                SceneManager.LoadScene("MenuScene");
                Trophies.TryUnlock(269984);
            }
            else
            {
                Debug.Log("No se pudo logear");
            }
        }); 
    }

}