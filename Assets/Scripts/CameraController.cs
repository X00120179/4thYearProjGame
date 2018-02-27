using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // We want the camera to follow the character so first
    // we need to know what the player is, so we need the character script.
    public PlayerController thePlayer;

    private Vector3 lastPlayerPosition;
    private float distaneToMove;

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerController>();
        lastPlayerPosition = thePlayer.transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        // Get the camera to move with the character each frame, only concerned with the X-axis here.
        distaneToMove = thePlayer.transform.position.x - lastPlayerPosition.x;

        transform.position = new Vector3(transform.position.x + distaneToMove, transform.position.y, transform.position.z);

        lastPlayerPosition = thePlayer.transform.position;
	}
}
