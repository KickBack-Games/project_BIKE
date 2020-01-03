using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAway : MonoBehaviour
{
	private SpriteRenderer sr;
	// Use this for initialization
	private float i;
	public float freq;
	void Start () 
	{
		i = 1;
		sr = GetComponent<SpriteRenderer>();
	}
	void Update()
	{
		if (i > 0f)
			i -= freq;
		else
			Destroy(this.gameObject);
		sr.color = new Color(1f,.9f,0f,i);
	}
}
