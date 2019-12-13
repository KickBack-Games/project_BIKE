using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundReset : MonoBehaviour
{
	public GameObject GM;
	public float offset;
	private GameRules gr;
	private Vector2 pos;


    // Start is called before the first frame update
    void Start()
    {
    	pos = transform.position;
        gr = GM.GetComponent<GameRules>();
    	
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        pos += Vector2.down * gr.GAMESPEED * Time.deltaTime;
        if (pos.y >= 500f) {
        	transform.position = new Vector2(0, offset);
        }
        else {
        	transform.position = new Vector2(0, pos.y);
    	}
    	pos = transform.position;
    }
}
