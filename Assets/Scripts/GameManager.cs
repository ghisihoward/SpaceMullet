using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	private enum GameState { Menu, Ready, Playing, Paused, GameOver }
	private GameState currentState = GameState.Menu;
	private GameObject player, pauseMenu;
	private Vector3 initialPos;

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

	public void PushPlayer(Vector2 force){
		if (currentState == GameState.Playing) {
			player.GetComponent<Rigidbody2D> ().AddForce (force);
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
			this.resetPlayer ();
		}
		pauseMenu.SetActive (false);
	}

	public void PauseButton () {
		if (currentState == GameState.Playing) {
			currentState = GameState.Paused;
			pauseMenu.SetActive (true);	
		}
	}

	public void resetPlayer() {
		player.SetActive (false);
		player.transform.position = initialPos;
		player.transform.rotation = Quaternion.identity;
		player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
		player.GetComponent<Rigidbody2D> ().angularVelocity = 0f;
		player.SetActive (true);
		currentState = GameState.Ready;
	}
}
