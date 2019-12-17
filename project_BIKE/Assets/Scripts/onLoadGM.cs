using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onLoadGM : MonoBehaviour
{
	private Animator currController;

	public GameObject player;

	void Awake() {
		// TODO: When the controller for the animation (chosen player) is fixed/completed inside of changePlayer.cs, 
		// make sure to add it here so that the player loads up as the last player it played as.

		// Set the screen dimensions
		Screen.SetResolution(1080, 1920, true);
	}

}
