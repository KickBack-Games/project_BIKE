using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class motorcyclistBehavior : MonoBehaviour
{
	private GameObject GM;
	private GameRules gr;
	private Vector2 pos;

	void Start() {
		GM = GameObject.FindGameObjectWithTag("GameManager");
		gr = GM.GetComponent<GameRules>();
		pos = transform.position;
	}
	
    // Update is called once per frame
    void Update()
    {
        pos += Vector2.down * (gr.GAMESPEED * gr.motorSpeedDifficulty) * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, pos.y, pos.y);
    }
}
