using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathMenu : MonoBehaviour {

    public string mainMenuLevel;

    // Function for button on death menu.
    public void RestartGame()
    {
        // All the code to restart the game is within the Reset() function in the GameManager.cs script.
        FindObjectOfType<GameManager>().Reset(); // Finding the object thats in the scene that has the GameManager script attached and call the reset function.
    }

    // Function for Quit button on death menu.
    public void QuitToMainMenu()
    {
        //Application.LoadLevel(mainMenuLevel);
        SceneManager.LoadScene(mainMenuLevel);
    }
}
