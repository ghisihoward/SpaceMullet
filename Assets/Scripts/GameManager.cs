using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private enum GameState {
		Menu,
		Ready,
		Playing,
		Paused,
		GameOver
	}

	private GameState currentState = GameState.Menu;
	private GameObject player, pauseMenu;
	private Vector3 initialPos;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		pauseMenu = GameObject.FindGameObjectWithTag ("PauseMenu");
		initialPos = player.transform.position;
	}

	void Update () {
		switch (currentState) {
			case GameState.Paused:
			// do stuff
				break;
			case GameState.Playing:
			// do stuff
				break;
		}

		// TODO
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
		}
	}
}
