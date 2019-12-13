using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class onLoadGM : MonoBehaviour
{
	private Animator currController;

	public GameObject player;

	void Awake() {
		// TODO: When the controller for the animation (chosen player) is fixed/completed inside of changePlayer.cs, 
		// make sure to add it here so that the player loads up as the last player it played as.
		currController = player.GetComponent<Animator>();
		currController.runtimeAnimatorController = Resources.Load("Animations/player" + PlayerPrefs.GetInt("currPlayer", 0) + "_0") as RuntimeAnimatorController;
  
	}

}
