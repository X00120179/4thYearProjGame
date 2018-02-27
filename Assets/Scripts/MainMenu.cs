using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

    public string playGameLevel;

    public void PlayGame()
    {
        // Open up Endless runner scene
        //Application.LoadLevel(playGameLevel);
        SceneManager.LoadScene(playGameLevel);
    }	

    public void QuitGame()
    {
        Application.Quit();
    }


}
