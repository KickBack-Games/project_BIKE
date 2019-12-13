using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeMovement : MonoBehaviour
{
	private GameObject GM;
	private GameRules gr;
	private Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
    	pos = transform.position;
        GM = GameObject.FindGameObjectWithTag("GameManager");
        gr = GM.GetComponent<GameRules>();
    }

    // Update is called once per frame
    void Update()
    {
        pos += Vector2.down * gr.GAMESPEED * Time.deltaTime;
        transform.position = new Vector2(pos.x, pos.y);
    }
}
