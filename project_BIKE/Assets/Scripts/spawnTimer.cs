using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTimer : MonoBehaviour
{
	private spawner spawnScript;
	public GameObject spawnObj;
	public float spawnSeconds;
	public int distance;

    // Start is called before the first frame update
    void Start()
    {
    	spawnSeconds = 1.8f;
    	distance = 30;
        spawnScript = spawnObj.GetComponent<spawner>();
        spawnScript.spawn(distance);
        StartCoroutine(spawntimer(spawnSeconds));
    }

    IEnumerator spawntimer(float secs)
    {
    	yield return new WaitForSeconds(secs);
    	spawnScript.spawn(distance);
    	StartCoroutine(spawntimer(spawnSeconds));
    }
}
