using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
	public float GAMESPEED, SCORE, COINS, COINSFORREPLAY;
	public float spawnSeconds, jwalkerSpawnSeconds, motorSpawnSeconds, motorSpeedDifficulty;
	public int distance, jaywalkercounter, coneModeCounter, motorcyclistCounter;
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
        //PlayerPrefs.SetInt("PlayerCoins", 0);
        COINS = PlayerPrefs.GetInt("PlayerCoins", 0);
        COINSFORREPLAY = 75;
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

    // This function calculates the amount of coins given to player at the end of run.
    public void EndOfRunCoinAddition() {
    	print("CURRENTLY: " + COINS );
    	COINS = PlayerPrefs.GetInt("PlayerCoins", 0);
    	COINS += returnCoinsCalculation(SCORE);
    	PlayerPrefs.SetInt("PlayerCoins", (int)COINS);
    	print("TOTAL CURRENT COINS: " + (int)COINS);

    }

    public void setCoins(float coins) {
    	COINS += coins;
    	PlayerPrefs.SetInt("PlayerCoins", (int)COINS);
    }

    private int returnCoinsCalculation(float scoreTotal) {
    	print("TOTAL SCORE: " + scoreTotal);
    	// Every 10 cones will give the player 1 coin.
    	int everyTenIsOneCoin = (int)(scoreTotal / 10); 
    	print("Every 10: " + everyTenIsOneCoin);
    	// Upon completion of the run half of the distance will turn into coins. (will make it one quarter... 1/4)
    	int quarterOfFinalScore = (int)(scoreTotal / 4);
    	print("Final 4: " + quarterOfFinalScore);

    	// If you got past 100 cones you receive 25 coins for the distance + 10 coins from each set of 10 cones passed. 
    	if (scoreTotal >= 100) {
    		print("HERE");
    		// Add 25 coins to any of the variables that are already created... no big deal. Just going to the main total anyway
    		quarterOfFinalScore += 25;

    		// add 5 coins for every set of 10. So use prior variable that already has the amount of sets of 10's
    		// Subtracting because it's added for every other set of 10 after 100... the first 10, where the first 100... so 
    		// Take away 10 to make it adding 5 coins for every set of 10 AFTER the FIRST 100...
    		everyTenIsOneCoin += (everyTenIsOneCoin - 10) * 5;
    		print("Every 100 after 100: " + everyTenIsOneCoin);
    	}
    	int total = quarterOfFinalScore + everyTenIsOneCoin;
    	print("Total: " + total);
    	// ADD THE TOTALS AND RETURN THEM
    	return total;

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

