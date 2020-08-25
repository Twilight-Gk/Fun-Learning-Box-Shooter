using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// make game manager public static so can access this from other scripts
	public static GameManager gm;

	// public variables
	public int score=0;

	public bool canBeatLevel = false;
	public int beatLevelScore=0;

	public float startTime=5.0f;
	
	public Text mainScoreDisplay;
	public Text mainTimerDisplay;

	public GameObject gameOverScoreOutline;

	public AudioSource musicAudioSource;

	public bool gameIsOver = false;

	public GameObject playAgainButtons;
	public string playAgainLevelToLoad;

   

	public GameObject nextLevelButtons;
	public string nextLevelToLoad;
    public GameObject nextlevelbutton1;
    public GameObject nextlevelbutton2;
    public GameObject nextlevelbutton3;
    public GameObject nextlevelbutton4;
    
    public GameObject gameoverquestion;

    public string[] question = new string[] { };
    public string[] choice1 = new string[] {  };
    public string[] choice2 = new string[] { };
    public string[] choice3 = new string[] { };
    public string[] choice4 = new string[] {  };
    public GameObject victory;
    private int option;
    private float currentTime;

	// setup the game
	void Start () {

		// set the current time to the startTime specified
		currentTime = startTime;

		// get a reference to the GameManager component for use by other scripts
		if (gm == null) 
			gm = this.gameObject.GetComponent<GameManager>();

		// init scoreboard to 0
		mainScoreDisplay.text = "0";

		// inactivate the gameOverScoreOutline gameObject, if it is set
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (false);
        if (gameoverquestion)
            gameoverquestion.SetActive(false);
		// inactivate the playAgainButtons gameObject, if it is set
		if (playAgainButtons)
			playAgainButtons.SetActive (false);

		// inactivate the nextLevelButtons gameObject, if it is set
		if (nextLevelButtons)
			nextLevelButtons.SetActive (false);
        if (victory)
        {
            victory.SetActive(false);
        }
	}

	// this is the main game event loop
	void Update () {
		if (!gameIsOver) {
			if (canBeatLevel && (score >= beatLevelScore)) {  // check to see if beat game
				BeatLevel ();
			} else if (currentTime < 0) { // check to see if timer has run out
				EndGame ();
			} else { // game playing state, so update the timer
				currentTime -= Time.deltaTime;
				mainTimerDisplay.text = currentTime.ToString ("0.00");				
			}
		}
	}

	void EndGame() {
		// game is over
		gameIsOver = true;

		// repurpose the timer to display a message to the player
		mainTimerDisplay.text = "GAME OVER";

		// activate the gameOverScoreOutline gameObject, if it is set 
		if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);
	
		// activate the playAgainButtons gameObject, if it is set 
		if (playAgainButtons)
			playAgainButtons.SetActive (true);

		// reduce the pitch of the background music, if it is set 
		if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}
	
	void BeatLevel() {
		// game is over
		gameIsOver = true;

        option=Random.Range(0, question.Length);

		// repurpose the timer to display a message to the player
		mainTimerDisplay.text = "LEVEL COMPLETE";
        

        // activate the gameOverScoreOutline gameObject, if it is set 
        if (gameOverScoreOutline)
			gameOverScoreOutline.SetActive (true);

       
        if (gameoverquestion)
            gameoverquestion.GetComponentInChildren<Text>().text = question[option];
            gameoverquestion.SetActive(true);

        // activate the nextLevelButtons gameObject, if it is set 
        if (nextLevelButtons)
        {
            nextlevelbutton1.GetComponentInChildren<TextMesh>().text = choice1[option];
            nextlevelbutton2.GetComponentInChildren<TextMesh>().text = choice2[option];
            nextlevelbutton3.GetComponentInChildren<TextMesh>().text = choice3[option];
            nextlevelbutton4.GetComponentInChildren<TextMesh>().text = choice4[option];
            nextLevelButtons.SetActive(true);
        }
        
       
     
           

        // reduce the pitch of the background music, if it is set 
        if (musicAudioSource)
			musicAudioSource.pitch = 0.5f; // slow down the music
	}

	// public function that can be called to update the score or time
	public void targetHit (int scoreAmount, float timeAmount)
	{
		// increase the score by the scoreAmount and update the text UI
		score += scoreAmount;
		mainScoreDisplay.text = score.ToString ();
		
		// increase the time by the timeAmount
		currentTime += timeAmount;
		
		// don't let it go negative
		if (currentTime < 0)
			currentTime = 0.0f;

		// update the text UI
		mainTimerDisplay.text = currentTime.ToString ("0.00");
	}

	// public function that can be called to restart the game
	public void RestartGame ()
	{
		// we are just loading a scene (or reloading this scene)
		// which is an easy way to restart the level
        SceneManager.LoadScene(playAgainLevelToLoad);
	}

	// public function that can be called to go to the next level of the game
	public void NextLevel ()
	{
		// we are just loading the specified next level (scene)
        SceneManager.LoadScene(nextLevelToLoad);
	}
	

}
