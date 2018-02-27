using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {

    public ObjectPooler coinPool;

    public float distanceBetweenCoins;

	public void SpawnCoins(Vector3 startPosition)
    {
        // 1. Create a coin.
        // 2. Set the position of it.
        // 3. Make sure it's active.
        // 4. Repeat for more coins (3 coins)

        // 1.
        GameObject coin = coinPool.GetPooledObject();

        // 2.
        coin.transform.position = startPosition;

        // 3.
        coin.SetActive(true);

        
        GameObject coin2 = coinPool.GetPooledObject();
        coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y, startPosition.z);
        coin2.SetActive(true);

        GameObject coin3 = coinPool.GetPooledObject();
        coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);
        coin3.SetActive(true);
    }
}
