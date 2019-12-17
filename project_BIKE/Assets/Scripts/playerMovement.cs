using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

	// GAME MANAGER STUFF
	public GameObject gm;
	private UIController uic;

	private Vector3 pos;
	public float speed = 9.0f;
	//private Transform tr;

	public float counter, forwardSpeed;

	private bool move, onlyOnce, lost;

	// Use this for initialization
	void Start () 
	{
		uic = gm.GetComponent<UIController>();
		pos = transform.position;
		lost = false;
        onlyOnce = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		var mousePos = Input.mousePosition;

/*		if (pScript.inMenu)
		{
			move = false;
		}
		else
		{*/
			if (lost)
			{

			}
			else
			{
				if (uic.inPlay) 
				{
					
					if (Input.GetMouseButtonDown(0))
					{

						if ((mousePos.x >= Screen.width *.5f) && (pos.x < 40f) && (mousePos.y > Screen.height * .1f))
						{
		                	TapRight();
		                }
						else if ((mousePos.x < Screen.width *.5f) && (pos.x > -40f) && (mousePos.y > Screen.height * .1f)) 
						{
							TapLeft();
						}
					}
					//pos += Vector3.down * forwardSpeed;
					transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed); 
				}
			}
/*		}*/
	}

    public void TapLeft()
    {
        pos += Vector3.left * 20f;

        if (pos.x < -39f)
        {
        	pos = new Vector2(-38.5f, pos.y);
        }
    }
    public void TapRight()
    {   
        pos += Vector3.right * 20f;
        if (pos.x > 39f)
        {
        	pos = new Vector2(38.5f, pos.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Cone" || other.gameObject.tag == "ConeScore") // For the opponents hitbox
		{
			uic.LOST();
		}
	}
}
