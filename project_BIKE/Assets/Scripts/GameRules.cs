using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
	public float GAMESPEED;
	public bool inMenu, lost;
    // Start is called before the first frame update
    void Start()
    {
    	inMenu = true;
    	lost = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
