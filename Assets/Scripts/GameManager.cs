﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

	// GAME STATES
	private enum GameState { Menu, Ready, Playing, Paused, GameOver }
	private GameState currentState;

	// TIMER
	private Text timerText, metersText;
	private float secondsCount = 0f, distance = 0f;
	private int minuteCount = 0;

	// GAME OBJECTS
	private GameObject player, pauseMenu, gameOverMenu, inputObject, tutorialObject;
	private ScoreManager scoreManager;
	private LevelManager levelManager;
	private GameSettings gameSettings;
	private Vector3 playerInitialPos, cameraInitialPos;

	private GameObject scoreBoard, newScoreField, overloadField;
	private UnityEngine.UI.Text scoreBoardNames, scoreBoardPoints, inputNewScore;
	private UnityEngine.UI.InputField scoreName;

	private float score = -1f;
	private bool writeName = false;

	void Start () {
		currentState = GameState.Menu;

		timerText = GameObject.Find ("TextBox_Time").GetComponent<Text> ();
		metersText = GameObject.Find ("TextBox_Distance").GetComponent<Text> ();

		player = GameObject.FindGameObjectWithTag ("Player");
		pauseMenu = GameObject.FindGameObjectWithTag ("PauseMenu");
		inputObject = GameObject.FindGameObjectWithTag ("InputField");
		newScoreField = GameObject.FindGameObjectWithTag ("ScoreField");
		overloadField = GameObject.FindGameObjectWithTag ("OverloadField");
		scoreBoard = GameObject.FindGameObjectWithTag ("HighScoreField");
		scoreBoardNames = scoreBoard.transform.Find ("Names").GetComponent<Text> ();
		scoreBoardPoints = scoreBoard.transform.Find ("Points").GetComponent<Text> ();
		scoreName = inputObject.transform.Find("NameField").GetComponent <InputField> ();
		gameOverMenu = GameObject.FindGameObjectWithTag ("GameOverMenu");
		scoreManager = GameObject.FindGameObjectWithTag ("ScoreManager").GetComponent<ScoreManager> ();
		gameSettings = GameObject.FindGameObjectWithTag ("GameSettings").GetComponent<GameSettings> ();
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
		tutorialObject = GameObject.FindGameObjectWithTag ("Tutorial");

		playerInitialPos = player.transform.position;
		cameraInitialPos = Camera.main.transform.position;

		this.PlayButton ();

		overloadField.SetActive (false);
		gameOverMenu.SetActive (false);
	}

	void Update () {
		if (currentState == GameState.Playing) {
			UpdateStats ();
			UpdateUI ();
		}
			
		if (Blitzkrieg.GetGameObjectPosition(player).y < -0.009) {
			this.PlayerDeath ();
		}
			
		if (currentState == GameState.GameOver && score != -1) {
			if (scoreManager.isHighScore (score)){
				if (!writeName && scoreManager.lastName != "") {
					scoreName.text = scoreManager.lastName;
					writeName = true;
				}
				if (Input.GetKey(KeyCode.Return)) InputNameSend ();
			}
			else if (inputObject.activeSelf) {
				inputObject.SetActive (false);
				overloadField.SetActive (true);
			}
		}
	} 

	public void InputNameSend () {
		if (scoreName.text != "") {				
			scoreManager.AddScore (scoreName.text, score);
			scoreName.text = "";
			CleanUp ();
		}
	}

	public void GameOvertoPause () {
		if (!inputObject.activeSelf) {
			CleanUp ();
		}
	}

	public void UpdateStats () {
		distance = player.transform.position.y - playerInitialPos.y;
		secondsCount += Time.deltaTime;

		if (distance < 0) {
			distance = 0;
		}

		if (secondsCount >= 60) {
			minuteCount++;
			secondsCount -= 60;
		} else if (minuteCount >= 60) {
			minuteCount = 00;
		}
	}

	public void UpdateUI () {
		metersText.text = "Distance: " + distance.ToString ("0.00") + "ly";
		timerText.text = string.Format (
			"Time: {0:0}:{1:00}", 
			Mathf.Floor (minuteCount), 
			Mathf.Floor (secondsCount)
		);
	}

	public void PlayerCollision () { 
		this.PlayerDeath ();
	}

	public void VerticalSwipe (Vector2 force) {
		if (currentState == GameState.Ready) {
			player.GetComponent<Rigidbody2D> ().AddForce (force);
			currentState = GameState.Playing;
			tutorialObject.transform.Find ("Finger").GetComponent<Animator>().SetTrigger("StartGame");
			tutorialObject.transform.Find ("Arrows").GetComponent<Animator>().SetTrigger("PlayArrows");
		}		
	}

	public void PushPlayer (Vector2 dir, float force) {
		if (currentState == GameState.Playing) {
			if (gameSettings.currentInputType == GameSettings.InputType.RotateLocal) {
				Vector2 resultForce = player.transform.rotation * dir * force;
				player.GetComponent<Rigidbody2D> ().AddForce (resultForce);
			} else {
				Vector2 resultForce = dir * force;
				player.GetComponent<Rigidbody2D> ().AddForce (resultForce);
			}
		}
	}

	public void PlayButton () {
		if (currentState == GameState.Menu) {
			currentState = GameState.Ready;
			Time.timeScale = 1f;
			levelManager.GenerateLevel ();
		} else if (currentState == GameState.Paused) {
			currentState = GameState.Playing;
			Time.timeScale = 1f;
		} else if (currentState == GameState.GameOver) {
			levelManager.CleanGameWorld ();
			levelManager.GenerateLevel ();
			this.resetPlayer ();
			this.resetCamera ();
		}
		gameOverMenu.SetActive (false);
		pauseMenu.SetActive (false);
	}

	public void PauseButton () {
		if (currentState == GameState.Playing || currentState == GameState.Ready) {
			currentState = GameState.Paused;
			pauseMenu.transform.Find ("Button_Pause").GetComponent<Image> ().sprite = gameSettings.buttonPause;
			pauseMenu.SetActive (true);
			Time.timeScale = 0f;
		}
	}

	public void PlayerDeath () {
		if (currentState != GameState.GameOver) {
			currentState = GameState.GameOver;
			gameOverMenu.SetActive (true);
			score = Mathf.Floor ((secondsCount + minuteCount * 60) * distance);
			scoreBoardNames.text = scoreManager.GetScoreAt (1).name + "\n" +
				scoreManager.GetScoreAt (2).name + "\n" +
				scoreManager.GetScoreAt (3).name + "\n" +
				scoreManager.GetScoreAt (4).name + "\n" +
				scoreManager.GetScoreAt (5).name;
			scoreBoardPoints.text = scoreManager.GetScoreAt (1).points+ "\n" +
				scoreManager.GetScoreAt (2).points + "\n" +
				scoreManager.GetScoreAt (3).points + "\n" +
				scoreManager.GetScoreAt (4).points + "\n" +
				scoreManager.GetScoreAt (5).points;
			newScoreField.transform.Find ("NewScoreField").GetComponent<Text> ().text = score.ToString();
			pauseMenu.transform.Find ("Button_Pause").GetComponent<Image> ().sprite = gameSettings.buttonRetry;
			Time.timeScale = 0f;
		} 
	}

	public void CleanUp () {
		inputObject.SetActive (true);
		overloadField.SetActive (false);
		gameOverMenu.SetActive (false);
		pauseMenu.SetActive (true);
		writeName = false;
		secondsCount = 0f;
		minuteCount = 0;
		distance = 0f;
		score = -1f;
		UpdateUI ();
	}

	public void resetPlayer () {
		player.SetActive (false);
		player.transform.position = playerInitialPos;
		player.transform.rotation = Quaternion.identity;
		player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
		player.GetComponent<Rigidbody2D> ().angularVelocity = 0f;
		player.SetActive (true);
		currentState = GameState.Ready;
		Time.timeScale = 1f;
	}

	public void resetCamera () {
		Camera.main.transform.position = cameraInitialPos;
	}
}
