using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Transform platformGenerator;
    private Vector3 platformStartPoint;

    public PlayerController thePlayer;
    private Vector3 playerStartPoint;

    private PlatformDestructor[] platformList; // List of platforms with the platformDestroyer script, So we know what platforms to destroy at game reset when character dies.

    private ScoreManager theScoreManager;

    public DeathMenu theDeathMenu;

	// Use this for initialization
	void Start () {
        platformStartPoint = platformGenerator.position;
        playerStartPoint = thePlayer.transform.position;

        theScoreManager = FindObjectOfType<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartGame()
    {
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false); // Disable the character
        theDeathMenu.gameObject.SetActive(true); // Turns death menu on
        //StartCoroutine("RestartGameCo");
    }

    public void Reset()
    {
        theDeathMenu.gameObject.SetActive(false); // Turns death menu off
        platformList = FindObjectsOfType<PlatformDestructor>();
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false); // Resets the active platforms to inactive and puts them back in the object pool for the next time the game runs.
        }

        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true); // Reset the character

        theScoreManager.scoreCount = 0; // Reset score after death
        theScoreManager.scoreIncreasing = true;
    }

    // This function below has been divided into two functions above for use with the death menu.
    /*public IEnumerator RestartGameCo()
    {
        theScoreManager.scoreIncreasing = false;
        thePlayer.gameObject.SetActive(false); // Disable the character
        yield return new WaitForSeconds(1.5f); // How long the game waits before restarting the game.
        platformList = FindObjectsOfType<PlatformDestructor>();
        for(int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false); // Resets the active platforms to inactive and puts them back in the object pool for the next time the game runs.
        }

        thePlayer.transform.position = playerStartPoint;
        platformGenerator.position = platformStartPoint;
        thePlayer.gameObject.SetActive(true); // Reset the character

        theScoreManager.scoreCount = 0; // Reset score after death
        theScoreManager.scoreIncreasing = true;
    }*/
}
