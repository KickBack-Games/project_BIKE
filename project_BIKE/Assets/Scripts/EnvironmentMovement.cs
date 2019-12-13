using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMovement : MonoBehaviour {

   	public GameObject gameManager;
   	public float spdOffset;
   	private GameRules gr;
   	private Vector2 offset;

 	// Use this for initialization
 	void Start () {
 		gr = gameManager.GetComponent<GameRules>();
 
 	}
 
 	// Update is called once per frame
 	void Update () {
 		// This script works with a background that is used in a 3d object called Quad.
 		// https://www.youtube.com/watch?v=HrDxnMI7pCc&t=136s for details...
        offset = new Vector2(0, Time.time * gr.GAMESPEED * spdOffset);
        GetComponent<Renderer>().material.mainTextureOffset = offset;    
	}﻿
}
