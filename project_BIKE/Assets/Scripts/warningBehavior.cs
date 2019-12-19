using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warningBehavior : MonoBehaviour
{
	private float posX;
	public int lifeTime = 1000, spd;
	public Vector2 target;
	
	void Start() {
		target = new Vector2(-300, 0);
		transform.position = new Vector2(300, 0);
	}
    // Update is called once per frame
    void Update()
    {
    	lifeTime--;
    	if (lifeTime <= 0)
    		Destroy(gameObject);
        transform.position = Vector2.MoveTowards(transform.position, target, spd);
    }
}
