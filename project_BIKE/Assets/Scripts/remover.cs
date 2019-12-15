using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class remover : MonoBehaviour 
{
	private GameRules gr;
	public GameObject gm;

	void Start() 
	{
		gr = gm.GetComponent<GameRules>();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
    	if (col.gameObject.tag == "Cone")
    	{
        	Destroy(col.gameObject);
    	}
    	else if (col.gameObject.tag == "ConeScore")
    	{
    		// There are always 4 cones passing at once.
    		gr.incrementScore(1f);
        	Destroy(col.gameObject);

    	}
    }
}
