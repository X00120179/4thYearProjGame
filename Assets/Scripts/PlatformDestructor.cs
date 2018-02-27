using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestructor : MonoBehaviour {


    public GameObject platformDestructionPoint;

	// Use this for initialization
	void Start () {

        platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");

	}
	
	// Update is called once per frame
	void Update () {
		

        // Once a platform hits the imaginary "destruction point" deconstruct it.
        if(transform.position.x < platformDestructionPoint.transform.position.x)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
	}
}
