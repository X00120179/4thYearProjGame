using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string mainMenuLevel;

    public GameObject pauseMenu;

    public void PauseGame()
    {
        Time.timeScale = 0f; // 0f is no time, 1f is normal time, anything inbetween is slow motion andything over 1f is fast motion.
        pauseMenu.SetActive(true); // Pause menu turns on when we click the pause game button.
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Reset time back to normal on resume.
        pauseMenu.SetActive(false); // Pause menu turns off when we click the resume game button.
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset time back to normal on restart.
        pauseMenu.SetActive(false); // Pause menu turns off when we click the restart game button.
        // All the code to restart the game is within the Reset() function in the GameManager.cs script.
        FindObjectOfType<GameManager>().Reset(); // Finding the object thats in the scene that has the GameManager script attached and call the reset function.
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Reset time back to normal on next level load.
        //Application.LoadLevel(mainMenuLevel);
        SceneManager.LoadScene(mainMenuLevel);
        //pauseMenu.SetActive(false); // Pause menu turns off when we click the quit game button.
    }
}
