using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

	// GAME STATES
	private enum GameState { Menu, Ready, Playing, Paused, GameOver }
	private GameState currentState = GameState.Menu;

	// TIMER
	private Text timerText, metersText;
	private float secondsCount = 0f, distance = 0f;
	private int minuteCount = 0;

	// GAME OBJECTS
	private GameObject player, pauseMenu;
	private GameSettings gameSettings;
	private Vector3 playerInitialPos, cameraInitialPos;


	void Start () {
		timerText = GameObject.Find ("TextBox_Time").GetComponent<Text> ();
		metersText = GameObject.Find ("TextBox_Distance").GetComponent<Text> ();

		player = GameObject.FindGameObjectWithTag ("Player");
		pauseMenu = GameObject.FindGameObjectWithTag ("PauseMenu");
		gameSettings = GameObject.FindGameObjectWithTag ("GameSettings").GetComponent<GameSettings> ();
		playerInitialPos = player.transform.position;
		cameraInitialPos = Camera.main.transform.position;
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

		if (Blitzkrieg.GetGameObjectPosition(player).y < -0.009) {
			currentState = GameState.GameOver;
			pauseMenu.SetActive (true);
		}
	} 

	public void UpdateUI () {
		distance = player.transform.position.y - playerInitialPos.y;
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

	public void PlayerCollision () { 
		currentState = GameState.GameOver;
		pauseMenu.SetActive (true);
	}

	public void VerticalSwipe (float swipeMagnitude) {
		if (currentState == GameState.Ready) {
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

	public void PushPlayer(Vector2 dir, float force){
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
		} else if (currentState == GameState.Paused) {
			currentState = GameState.Playing;
		} else if (currentState == GameState.GameOver) {
			this.resetPlayer ();
			this.resetCamera ();
		}
		pauseMenu.SetActive (false);
	}

	public void PauseButton () {
		if (currentState == GameState.Playing) {
			currentState = GameState.Paused;
			pauseMenu.SetActive (true);
		}
	}

	public void resetPlayer () {
		player.SetActive (false);
		player.transform.position = playerInitialPos;
		player.transform.rotation = Quaternion.identity;
		player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
		player.GetComponent<Rigidbody2D> ().angularVelocity = 0f;
		player.SetActive (true);
		currentState = GameState.Ready;
	}

	public void resetCamera () {
		Camera.main.transform.position = cameraInitialPos;
	}
}