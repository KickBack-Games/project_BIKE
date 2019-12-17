using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePlayer : MonoBehaviour
{
	private int maxNumPlayers, index;

	private GameObject[] playerList;
	
    // Start is called before the first frame update
    void Start()
    {
    	maxNumPlayers = transform.childCount;

        playerList = new GameObject[maxNumPlayers];

        // Fill array with our players
        for (int i = 0; i < maxNumPlayers; i++) {
            playerList[i] = transform.GetChild(i).gameObject;
        }

        // Make sure none of them are visible at the beginning...
        foreach(GameObject go in playerList) {
            go.SetActive(false);
        }

        // Toggle on the last used player
        if (playerList[PlayerPrefs.GetInt("currPlayer", 0)]) // if it exists...
        {
            playerList[PlayerPrefs.GetInt("currPlayer", 0)].SetActive(true);
        }
    }



    public void rightPlayer() 
    {

        index = PlayerPrefs.GetInt("currPlayer", 0);
    	// Toggle off the current character
        playerList[index].SetActive(false);
        index++;
    	if (index > playerList.Length - 1) {
    		index = 0;
    	}

        playerList[index].SetActive(true);
        PlayerPrefs.SetInt("currPlayer", index);
        print(index);

}

    public void leftPlayer() 
    {
    	
        index = PlayerPrefs.GetInt("currPlayer", 0);
        // Toggle off the current character
        playerList[index].SetActive(false);
        index--;
        if (index < 0) {
            index = playerList.Length - 1;
        }

        playerList[index].SetActive(true);
        PlayerPrefs.SetInt("currPlayer", index);


    }
}
