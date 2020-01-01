using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public GameObject menuUI, lostUI, gameplayUI, settingsUI;

	// create an empty GO for the sound settings menu
	public GameObject soundSettingsUI;

	private Button btnPlay, btnRight, btnLeft, btnSettings;

	// all the btns in settings
	private Button btnSetBack, btnSetSound, btnSetFAQ,
				   btnSetLeaderboard, btnSetAchievements, btnSetLike,
				   btnSetStatistics, btnSetDeleteProgress, btnSetNews,
				   btnSetCredits;

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

    // Enter sound settings window
    public void GOTOSOUNDSETTINGS(){
    	turnOnSoundSettingsUI();
    }

    // Leave sound settings window
    public void LEAVESOUNDSETTINGS(){
    	turnOffSoundSettingsUI();
    }

    public void UPDATESCORE(float scre) {
    	txtScore.text = scre.ToString();
    }
    public void REPLAY() {
        print("Spending money to replay!");
        keepPlaying();
    }
    public void NOREPLAY() {
        print("Not Spending money. Finish game.");
        Application.LoadLevel (Application.loadedLevelName);
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

    // These functions are to be used by public functions.

    private void keepPlaying() {
        inPlay = true;
        inLost = false;

        Time.timeScale = 1;
        gmRules.GAMESPEED = -75;  // This is the regular gamespeed
        turnOffLostUI();

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

    // Hide settingsUI but not bring back Main Menu UI
    private void turnOnSoundSettingsUI(){
    	settingsUI.SetActive(false);
    	soundSettingsUI.SetActive(true);
    }

    // Back to SettingsUI
    private void turnOffSoundSettingsUI(){
    	soundSettingsUI.SetActive(false);
    	settingsUI.SetActive(true);
    }

    // Show lostUI
    private void turnOnLostUI(){
        lostUI.SetActive(true);
    }

    // Hide Lost UI
    private void turnOffLostUI(){
        lostUI.SetActive(false);
    }

    IEnumerator LostTimer(int time)
    {
    	yield return new WaitForSeconds(time);
        Time.timeScale = 0;
        turnOnLostUI();
    }
}