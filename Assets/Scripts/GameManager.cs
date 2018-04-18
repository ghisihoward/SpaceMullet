using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private enum GameState { Menu, Ready, Playing, Paused, GameOver }
	private GameState currentState = GameState.Paused;
	private GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
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
}
