using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public GameObject menuUI, lostUI, gameplayUI, settingsUI;
	private Button btnPlay, btnRight, btnLeft, btnSettings;
	private GameRules gmRules;
	public bool inMenu, inPlay, inLost, inPause;
	public Text txtScore, txtHighscore;

    // Start is called before the first frame update
    void Start()
    {
    	inMenu = true;
    	inPlay = false;
    	inLost = false;
    	inPause = false;
        gmRules = GetComponent<GameRules>();

        txtHighscore.text = "Highscore: " + PlayerPrefs.GetInt("n_highscore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PLAY() {
    	inPlay = true;
    	inMenu = false;
    	turnOffMenuUI();
    	turnOngamePlayUI();
    }

    public void GOTOSETTINGS() {
        turnOnSettingsUI();
    }

    public void LEAVESETTINGS() {
        turnOffSettingsUI();
    }

    public void UPDATESCORE(float scre) {
    	txtScore.text = scre.ToString();
    }

    public void LOST() {
    	inPlay = false;
    	inLost = true;

    	gmRules.GAMESPEED = 0;

        // set the highscore!
        if (gmRules.SCORE > PlayerPrefs.GetInt("n_highscore", 0))
            PlayerPrefs.SetInt("n_highscore", (int)gmRules.SCORE);
            
    	StartCoroutine(LostTimer(2));
    }

    private void turnOngamePlayUI(){
    	gameplayUI.SetActive(true);
    }
    private void turnOffgamePlayUI(){
    	gameplayUI.SetActive(false);
    }
    private void turnOnMenuUI(){
    	menuUI.SetActive(true);
    }
    private void turnOffMenuUI(){
    	menuUI.SetActive(false);
    }
    private void turnOnSettingsUI(){
        menuUI.SetActive(false);
        settingsUI.SetActive(true);
    }
    private void turnOffSettingsUI(){
        menuUI.SetActive(true);
        settingsUI.SetActive(false);
    }

    IEnumerator LostTimer(int time)
    {
    	yield return new WaitForSeconds(time);
    	Application.LoadLevel (Application.loadedLevelName);
    	
    }
}
