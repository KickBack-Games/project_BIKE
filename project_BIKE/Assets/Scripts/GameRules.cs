using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
	public float GAMESPEED, SCORE;
	public float spawnSeconds;
	public int distance;
	public bool lost;

	public GameObject spawnObj;

	private IEnumerator coroutine;

	private UIController uic;
	private spawner spawnScript;
	private bool beginningLock = false;

    // Start is called before the first frame update
    void Start()
    {
    	uic = GetComponent<UIController>();
    	lost = false;

    	spawnSeconds = 1.6f;
        spawnScript = spawnObj.GetComponent<spawner>();
    }

    // Update is called once per frame
    void Update()
    {
    	if (uic.inMenu) {

    	}  
    	else if (uic.inPlay) {
    		if (!beginningLock) {
    			// Do this once at the very beginning of the game. make the lock true so that this does not get triggered again.
    			beginningLock = true;

    			coroutine = spawntimer(spawnSeconds);
    			StartCoroutine(coroutine);
    		}
    		if (SCORE <= 3) {
    			spawnSeconds = 1.5f;
    		}
    		else if (SCORE <= 6) {
    			spawnSeconds = .9f;
    		}
    		else if (SCORE <= 9) {
    			spawnSeconds = .6f;
    		}
    	}
        
    }

    public void incrementScore(float score) 
    {
    	SCORE += score;

    	// Make a function to update the score text.
    	uic.UPDATESCORE(SCORE);
    }

    IEnumerator spawntimer(float secs)
    {
    	yield return new WaitForSeconds(secs);
    	spawnScript.spawn(distance);
    	StartCoroutine(spawntimer(spawnSeconds));
    }
}

