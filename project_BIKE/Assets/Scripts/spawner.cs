using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour 
{
	public GameObject cone, coneScorer, jaywalker, jaywalkerScorer, player, warningObj;
	public List<int> gridPos = new List<int>();

	private PlayerMovement pm;
/*	private int left = -40, 
				leftCenter = -20,
				middle = 0,
				rightCenter = 20,
				right = 40,
				choice;
*/

	private int left = -30, 
				leftCenter = -10,
				rightCenter = 10,
				right = 30,
				choice;

	// Use this for initialization
	void Start () 
	{
		pm = player.GetComponent<PlayerMovement>();
	}
	

	public void spawn(int off)
	{
		// fill with needed values
		gridPos.Add(left);
		gridPos.Add(leftCenter);
		//gridPos.Add(middle);
		gridPos.Add(rightCenter);
		gridPos.Add(right);

		// spawn 4 cones
		int iteration = gridPos.Count - 1;
		while(iteration > 0)
		{
			
			choice = Random.Range(0, gridPos.Count);
			if (iteration == 3) {
				Instantiate(coneScorer, new Vector2(gridPos[choice], transform.position.y + off), Quaternion.identity);
			}
			else { 
				Instantiate(cone, new Vector2(gridPos[choice], transform.position.y + off), Quaternion.identity);
			}
			gridPos.RemoveAt(choice);
			iteration--;
		}

		// remove so that when this function is called again, there are no duplicates
		gridPos.RemoveAt(0);
	}
	public void spawnJaywalkers(int off, float chance)
	{
		// Spawn jaywalker
		Instantiate(jaywalkerScorer, new Vector2(-70f, -150f), Quaternion.identity);
		if (chance >= 10)
			Instantiate(jaywalker, new Vector2(-70f, -150f), Quaternion.identity);
	}
}
