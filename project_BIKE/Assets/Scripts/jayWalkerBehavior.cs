using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jayWalkerBehavior : MonoBehaviour
{
	private GameObject GM;
	private GameRules gr;
	private Vector2 pos;
	public float directionSpd, direction;

	void Start() {
		GM = GameObject.FindGameObjectWithTag("GameManager");
		gr = GM.GetComponent<GameRules>();

		// Decide which side to come from
		direction = Random.Range(1,3);
		if (direction == 1)
			direction = 70f;
		else
			direction = -70f;
		transform.position = new Vector2(direction, -150f);
		pos = transform.position;

		// decide beginning spd
		directionSpd = Random.Range(15f, 55f);
		if (pos.x < 0) {
			// must go right.
			directionSpd *= -1f;
			transform.localScale = new Vector2(-1,1); // flip sprite around
		}
	}
	
    // Update is called once per frame
    void Update()
    {
        pos += Vector2.down * gr.GAMESPEED * Time.deltaTime;
        pos += Vector2.left * directionSpd * Time.deltaTime;
        transform.position = new Vector2(pos.x, pos.y);
    }
}
