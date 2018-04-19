﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

	//GAME STATES
	private enum GameState {Menu, Ready, Playing, Paused, GameOver}
	private GameState currentState = GameState.Menu;
	private GameObject player, pauseMenu, textTime;
	private Vector3 initialPos;

	//TIMER
	public Text timerText, metersText;
	private float secondsCount = 0f, distance = 0f;
	private int minuteCount = 0;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		pauseMenu = GameObject.FindGameObjectWithTag ("PauseMenu");
		initialPos = player.transform.position;
	}


	void Update () {
		if (
			currentState == GameState.Menu ||
			currentState == GameState.Paused ||
			currentState == GameState.GameOver
		) {
			Time.timeScale = 0f;
		} else {
			Time.timeScale = 1f;
		}

		UpdateUI ();
		// Verify if player is out of camera bounds.
	} 
	
	public void VerticalSwipe (float swipeMagnitude) {
		if (currentState == GameState.Paused) {
			// TODO Review Math
			Vector3 forceVector = new Vector3 (0, 1 * swipeMagnitude, 0);
			player.GetComponent<Rigidbody2D> ().AddForce (forceVector);
			currentState = GameState.Playing;
		}		
	}

	public void VerticalSwipe (Vector2 force) {
		if (currentState == GameState.Ready) {
			// TODO Review Math
			player.GetComponent<Rigidbody2D> ().AddForce (force);
			currentState = GameState.Playing;
		}		
	}

	public void PlayerCollision () { 
		currentState = GameState.GameOver;
		pauseMenu.SetActive (true);
	}

	public void PlayButton () {
		if (currentState == GameState.Menu) {
			currentState = GameState.Ready;
		} else if (currentState == GameState.Paused) {
			currentState = GameState.Playing;
		} else if (currentState == GameState.GameOver) {
			player.transform.position = initialPos;
			player.transform.rotation = Quaternion.identity;
			player.GetComponent<Rigidbody2D> ().velocity.Set (0, 0);
			player.GetComponent<Rigidbody2D> ().angularVelocity = 0;
			currentState = GameState.Ready;
		}
		pauseMenu.SetActive (false);
	}

	public void PauseButton () {
		if (currentState == GameState.Playing) {
			currentState = GameState.Paused;
			pauseMenu.SetActive (true);
			Time.timeScale = 0;
		}
	}
	public void UpdateUI () {
		distance = player.transform.position.y - initialPos.y;
		secondsCount += Time.deltaTime;

		if (secondsCount >= 60) {
			minuteCount++;
			secondsCount = 00f;
		} else if (minuteCount >= 60) {
			minuteCount = 00;
		}

		metersText.text = "Distance: " + distance.ToString ("0.00") + "m";
		timerText.text = string.Format (
			"Time: {0:0}:{1:00}", 
			Mathf.Floor (secondsCount / 60), 
			secondsCount % 60
		);
	}
}