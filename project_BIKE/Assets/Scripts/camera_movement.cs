﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement : MonoBehaviour 
{
	public GameObject player;
	public float offset;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3(0, player.transform.position.y - offset, -600);
	}
}
