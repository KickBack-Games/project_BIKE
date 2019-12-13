using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour 
{
	public GameObject cone, player;
	public List<int> gridPos = new List<int>();

	private PlayerMovement pm;
	private int left = -40, 
				leftCenter = -20,
				middle = 0,
				rightCenter = 20,
				right = 40,
				choice;

	// Use this for initialization
	void Start () 
	{
		pm = player.GetComponent<PlayerMovement>();
		spawn(-30);
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.R))
        {
            print("R key was pressed");
			Application.LoadLevel (Application.loadedLevelName);
		}
	}

	public void spawn(int off)
	{
		// fill with needed values
		gridPos.Add(left);
		gridPos.Add(leftCenter);
		gridPos.Add(middle);
		gridPos.Add(rightCenter);
		gridPos.Add(right);

		// spawn 4 cones
		int iteration = 4;
		while(iteration > 0)
		{
			choice = Random.Range(0,gridPos.Count);
			Instantiate(cone, new Vector2(gridPos[choice], transform.position.y + off), Quaternion.identity);
			gridPos.RemoveAt(choice);
			iteration--;
		}
		// finally spawn the empty space object
		//Instantiate(noTree, new Vector2(gridPos[0], transform.position.y + off), Quaternion.identity);



		// remove so that when this function is called again, there are no duplicates
		gridPos.RemoveAt(0);	
	}
}
