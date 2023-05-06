using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    
   public void PlayGame ()
    {
        if (StaticVar.LevelIndex == 0)
        {
            SceneManager.LoadScene("TheMagicShop");
        }
        else if (StaticVar.LevelIndex == 1)
        {
            SceneManager.LoadSceneAsync("TheMagicShop",LoadSceneMode.Additive);
        }
        
    }


   public void QuitGame()
    {
        Application.Quit();
    }
}