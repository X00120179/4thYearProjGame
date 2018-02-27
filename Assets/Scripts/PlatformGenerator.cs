using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {
    /* As the camera follows the character on the X-axis platforms 
     * appear on the screen, when the platforms are running out
     * this will generate more again, and again, and again.
     */

    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth; // So we dont spawn a platform larger than the previous so that the character doesnt have a continuous platform to walk on.

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    //public GameObject[] thePlatforms;
    private int platformSelector;
    private float[] platformWidths;

    // Holds several types of platforms for re-use, saving cpu usage.
    public ObjectPooler[] theObjectPools;

    private float minHeight;
    public Transform maxHeightPoint; // This helps us determine our actual max height!
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;  // This is to keep the game fair and not put a platform up too high that the character cannot jump to (uses maxHeightChange).


    private CoinGenerator theCoinGenerator;
    public float randomCoinThreshold;


    // Use this for initialization
    void Start () {
        //platformWidth = thePlatform.GetComponent<BoxCollider2D>().size.x; // Gets us the width of the platform.
        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            // theObjectPools has no BoxCollider2D so here we must call the specific pooledObject and get the BoxCollider2D of that object.
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        theCoinGenerator = FindObjectOfType<CoinGenerator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            // Selects a platform at random from the range of platforms created.
            platformSelector = Random.Range(0, theObjectPools.Length);

            // Change the height of platform.
            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            // Validation: Spawn platforms within the view of the camera and within certain heights.
            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);

            // Instantiates the platform randomly chosen from the platformSelector.
            //Instantiate(/*thePlatform*/ thePlatforms[platformSelector], transform.position, transform.rotation);

            // Creates a newPlatform and grabs the platform object from the object pool.
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            /*
             * So if you set randomCoinThreshold to 75 you should see coins 3/4 of the time, 25 would see coulds 1/4 of the time etc...
             */
            if (Random.Range(0f, 100f) < randomCoinThreshold) 
            {
                // Generate coins above the platform.
                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));
            }
            // Similar line as the one a few lines above, repeated here so that spaces inbetween platforms is consistant.
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);

        }
    }
}
