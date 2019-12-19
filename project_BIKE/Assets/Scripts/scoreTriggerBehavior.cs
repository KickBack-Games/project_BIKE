using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreTriggerBehavior : MonoBehaviour
{
	private GameRules gr;
	public GameObject gm;

	void Start() 
	{
		gr = gm.GetComponent<GameRules>();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
    	if (col.gameObject.tag == "ConeScore")
    	{
    		gr.incrementScore(1f);

    	}
    }
}
