using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public enum GameState { Playing, Paused }
	public GameState currentState = GameState.Paused;

	void Update () {
		switch (currentState) {
		case GameState.Paused:
			// do stuff
			break;
		case GameState.Playing:
			// do stuff
			break;
		}
	}
}
