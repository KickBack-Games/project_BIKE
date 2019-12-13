using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class changePlayer : MonoBehaviour
{
	private int maxNumPlayers;
	private Animator currController;

	public GameObject player;
	
    // Start is called before the first frame update
    void Start()
    {
    	maxNumPlayers = 1;
    	currController = player.GetComponent<Animator>();
    }



    public void rightPlayer() 
    {
    	// Might be a good idea to make a spritesheet containing all of the players ( just an icon - can be just the first image )
    	// So that I can Resource.LoadAll, and use the total length (.length) of the array of sprites, to make it more dynamic
    	// because IF the player presses the right, and we add 1 to the counter, then if there are 10 players currently, if the
    	// counter goes to 11, then it should restart at 0. Rather than changing the number, doing it dynamically would be safer
    	int nextPlayerCounter = PlayerPrefs.GetInt("currPlayer", 0) + 1; // Counter must be incremented
    	if (nextPlayerCounter > maxNumPlayers) {
    		nextPlayerCounter = 0;
    		PlayerPrefs.SetInt("currPlayer", 0);
    	} else {
    		PlayerPrefs.SetInt("currPlayer", nextPlayerCounter);
    	}

    	// This line allows you to switch the controller via script by loading it using Resources and passing the directory of the controller
        currController.runtimeAnimatorController = Resources.Load("Animations/player" + nextPlayerCounter + "_0") as RuntimeAnimatorController;
    }

    public void leftPlayer() 
    {
    	
    	// Might be a good idea to make a spritesheet containing all of the players ( just an icon - can be just the first image )
    	// So that I can Resource.LoadAll, and use the total length (.length) of the array of sprites, to make it more dynamic
    	// because IF the player presses the right, and we add 1 to the counter, then if there are 10 players currently, if the
    	// counter goes to 11, then it should restart at 0. Rather than changing the number, doing it dynamically would be safer
    	int nextPlayerCounter = PlayerPrefs.GetInt("currPlayer", 0) - 1; // Counter must be incremented
    	if (nextPlayerCounter < 0) {
    		nextPlayerCounter = maxNumPlayers;
    		PlayerPrefs.SetInt("currPlayer", maxNumPlayers);
    	} else {
    		PlayerPrefs.SetInt("currPlayer", nextPlayerCounter);
    	}

    	// This line allows you to switch the controller via script by loading it using Resources and passing the directory of the controller
        currController.runtimeAnimatorController = Resources.Load("Animations/player" + nextPlayerCounter + "_0") as RuntimeAnimatorController;

    }
}
