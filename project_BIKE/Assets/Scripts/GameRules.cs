using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
	public float GAMESPEED, SCORE;
	public float spawnSeconds, jwalkerSpawnSeconds, motorSpawnSeconds, motorSpeedDifficulty;
	public int distance, jaywalkercounter, coneModeCounter, motorcyclistCounter, replayLimit;
	public bool lost, stopCones, motorcyclistsComing;

	public GameObject spawnObj, warningObj, clearScreenObj;

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
    	motorcyclistsComing = false;

    	spawnSeconds = .7f;
    	jwalkerSpawnSeconds = .8f;
    	motorSpeedDifficulty = 2.0f;
        spawnScript = spawnObj.GetComponent<spawner>();
        replayLimit = 1;
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
    	}
        
    }

    public void incrementScore(float score) 
    {
    	SCORE += score;

    	// Make a function to update the score text.
    	uic.UPDATESCORE(SCORE);
    }

    public void clearScreen() {
    	Instantiate(clearScreenObj, new Vector2(0f, 0f), Quaternion.identity);
    }

    IEnumerator spawntimer(float secs)
    {
    	yield return new WaitForSeconds(secs);
    	spawnScript.spawn(distance);

    	// Depending on the boolean, spawn cones or jaywalkers
    	if (!stopCones)
    		StartCoroutine(spawntimer(spawnSeconds));
    	else {
    		// Decide motocyclists vs jaywalkers
    		motorcyclistCounter = Random.Range(0, motorcyclistCounter);
    		if (motorcyclistCounter == 1) {
    			StartCoroutine(WaitForSecondsToSpawnMotorcyclists(3));
    		} else {
    			// After this goes for the first time, then motorcyclists have a chance to be spawned
    			motorcyclistCounter = 2;
    			StartCoroutine(WaitForSecondsToSpawnJayWalker(3));
    		}
    	}
    }

    IEnumerator SpawnJaywalkers(float secs) 
    {
    	yield return new WaitForSeconds(secs);
    	float chance = Random.Range(0, SCORE);
    	spawnScript.spawnJaywalkers(distance, chance);

		// Depending on the boolean, gp back to spawning cones or jaywalkers
    	if (stopCones) {
    		StartCoroutine(SpawnJaywalkers(jwalkerSpawnSeconds));
    	}
    	else{
    		StartCoroutine(spawntimer(3)); // go back to cones, but give some time for the warning to go by
    		Instantiate(warningObj, new Vector2(90f, 0), Quaternion.identity);
    	}

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

    IEnumerator SpawnMotorcyclists(float secs) 
    {
    	yield return new WaitForSeconds(secs);
    	spawnScript.spawnMotorcyclists(distance);

		// Depending on the boolean, gp back to spawning cones or jaywalkers
    	if (stopCones)
    		StartCoroutine(SpawnMotorcyclists(motorSpawnSeconds));
    	else{
    		StartCoroutine(spawntimer(3)); // go back to cones, but give some time for the warning to go by
    		Instantiate(warningObj, new Vector2(90f, 0), Quaternion.identity);
    	}

    }

    IEnumerator WaitForSecondsToSpawnMotorcyclists(float secs) 
    {
    	// Give a warning!
    	Instantiate(warningObj, new Vector2(90f, 0), Quaternion.identity);
    	yield return new WaitForSeconds(secs);

    	spawnScript.spawnMotorcyclists(distance);
    	StartCoroutine(SpawnMotorcyclists(motorSpawnSeconds));
    }

    IEnumerator switchStopConesBool(int secs, bool stop)
   	{
   		yield return new WaitForSeconds(secs);
   		int length;
   		stopCones = stop;
   		if (stopCones) {

   			// Time to turn it back to cones
   			coneModeCounter++;
   			length = Random.Range(12, 22);

   			// Make it more difficult based on the amount of times it goes to jaywalkers
   			if (coneModeCounter == 1) {
   				jwalkerSpawnSeconds = .8f;
   				motorSpawnSeconds = .3f;
   				motorSpeedDifficulty = 1.5f;
   			}
   			else if (coneModeCounter == 2) {
   				jwalkerSpawnSeconds =.7f;
   				motorSpawnSeconds = .28f;
   				motorSpeedDifficulty = 1.6f;
   			} 
   			else if (coneModeCounter == 3) {
   				jwalkerSpawnSeconds = .6f;
   				motorSpawnSeconds = .26f;
   				motorSpeedDifficulty = 1.6f;
   			}
   			else if (coneModeCounter == 4) {
   				jwalkerSpawnSeconds = .57f;
   				motorSpawnSeconds = .23f;
   				motorSpeedDifficulty = 1.85f;
   			}

   		} else {
   			// Time to turn it to jaywalker or motorcyclist
   			length = Random.Range(15, 28);

   			jaywalkercounter++;
   			// Make it more difficult based on the amount of times it goes to jaywalkers
   			if (jaywalkercounter == 1) {
   				spawnSeconds = .65f;
   			}
   			else if (jaywalkercounter == 2) {
   				spawnSeconds =.6f;
   			} 
   			else if (jaywalkercounter == 3) {
   				spawnSeconds = .58f;
   			}
   			else if (jaywalkercounter == 4) {
   				spawnSeconds = .57f;
   			}
   			else if (jaywalkercounter == 5) {
   				spawnSeconds = .55f;
   			}
   		}
   		StartCoroutine(switchStopConesBool(length, !stop));
   	}
}

