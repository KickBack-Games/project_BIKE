using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
	public float GAMESPEED, SCORE;
	public float spawnSeconds, jwalkerSpawnSeconds;
	public int distance, jaywalkercounter, coneModeCounter;
	public bool lost, stopCones;

	public GameObject spawnObj, warningObj;

	private IEnumerator coroutine;


	private UIController uic;
	private spawner spawnScript;
	private bool beginningLock = false;

    // Start is called before the first frame update
    void Start()
    {
    	uic = GetComponent<UIController>();
    	lost = false;
    	jaywalkercounter = 0;

    	spawnSeconds = 1.6f;
    	jwalkerSpawnSeconds = .8f;
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
    			StartCoroutine(switchStopConesBool(10, true));
    		}

    		// Difficulty
    		if (SCORE <= 10) {
    			spawnSeconds = .7f;
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

    	// Depending on the boolean, spawn cones or jaywalkers
    	if (!stopCones)
    		StartCoroutine(spawntimer(spawnSeconds));
    	else
    		StartCoroutine(WaitForSecondsToSpawnJayWalker(3));
    }
    IEnumerator WaitForSecondsToSpawnJayWalker(float secs) 
    {
    	// Give a warning!
    	Instantiate(warningObj, new Vector2(90f, 0), Quaternion.identity);

    	yield return new WaitForSeconds(secs);

    	// The chance var decides whether one or 2 jaywalkers will be spawned. The higher the score, the bigger the chance of 2 jw's
    	float chance = Random.Range(0, SCORE);
    	spawnScript.spawnJaywalkers(distance, chance);
    	StartCoroutine(SpawnJaywalkers(jwalkerSpawnSeconds));
    }

    IEnumerator SpawnJaywalkers(float secs) 
    {
    	yield return new WaitForSeconds(secs);
    	float chance = Random.Range(0, SCORE);
    	spawnScript.spawnJaywalkers(distance, chance);

		// Depending on the boolean, gp back to spawning cones or jaywalkers
    	if (stopCones)
    		StartCoroutine(SpawnJaywalkers(jwalkerSpawnSeconds));
    	else{
    		StartCoroutine(spawntimer(3)); // go back to cones, but give some time for the warning to go by
    		Instantiate(warningObj, new Vector2(90f, 0), Quaternion.identity);
    	}

    }

    IEnumerator switchStopConesBool(int secs, bool stop)
   	{
   		yield return new WaitForSeconds(secs);
   		int length;
   		stopCones = stop;
   		if (stopCones) {
   			// Time to turn it back to cones
   			length = Random.Range(10, 20);
   			coneModeCounter++;
   			// Make it more difficult based on the amount of times it goes to jaywalkers
   			if (coneModeCounter == 1) {
   				jwalkerSpawnSeconds = .8f;
   			}
   			else if (coneModeCounter == 2) {
   				jwalkerSpawnSeconds =.7f;
   			} 
   			else if (coneModeCounter == 3) {
   				jwalkerSpawnSeconds = .6f;
   			}
   			else if (coneModeCounter == 4) {
   				jwalkerSpawnSeconds = .57f;
   			}
   				
   		} else {
   			// Time to turn it to jaywalker
   			length = Random.Range(20, 35);
   			jaywalkercounter++;
   			// Make it more difficult based on the amount of times it goes to jaywalkers
   			if (jaywalkercounter == 1) {
   				spawnSeconds = .6f;
   			}
   			else if (jaywalkercounter == 2) {
   				spawnSeconds =.55f;
   			} 
   			else if (jaywalkercounter == 3) {
   				spawnSeconds = .53f;
   			}
   			else if (jaywalkercounter == 4) {
   				spawnSeconds = .52f;
   			}
   		}
   		StartCoroutine(switchStopConesBool(length, !stop));
   	}
}

