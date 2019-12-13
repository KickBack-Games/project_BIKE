using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour 
{
	public GameObject tree, noTree, player, pup1;
	public List<int> gridPos = new List<int>();

	public int powerUpSpawnTimer, powerUpFrequency;


	private playerMovement pm;
	private int left = -53, 
				leftCenter = -18, 
				rightCenter = 18,
				right = 53,
				choice;

	// Use this for initialization
	void Start () 
	{
		powerUpSpawnTimer = 5;

		powerUpFrequency = 3;
		pm = player.GetComponent<playerMovement>();
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
		gridPos.Add(rightCenter);
		gridPos.Add(right);

		// spawn 3 trees
		int iteration = 3;
		while(iteration > 0)
		{
			choice = Random.Range(0,gridPos.Count);
			Instantiate(tree, new Vector2(gridPos[choice], transform.position.y + off), Quaternion.identity);
			gridPos.RemoveAt(choice);
			iteration--;
		}
		// finally spawn the empty space object
		Instantiate(noTree, new Vector2(gridPos[0], transform.position.y + off), Quaternion.identity);

		if (pm.getPowerUpCounterOneUp() == powerUpSpawnTimer) {
			Instantiate(pup1, new Vector2(gridPos[0], transform.position.y + off), Quaternion.identity);
			powerUpSpawnTimer += powerUpFrequency;
			powerUpFrequency += powerUpSpawnTimer;

		}

		// remove so that when this function is called again, there are no duplicates
		gridPos.RemoveAt(0);	
	}
}
