using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour {

	private GameObject gameManager;
	private GameSettings gameSettings;
	private Vector2 mousePressed, mouseReleased;

	private float mouseDeltaX = 0f, mouseDeltaY = 0f;
	private float posDeltaPositive = 0f, posDeltaNegative = 0f;
	private float swipeStart = 0f, swipeInterval = 0f;

	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		gameSettings = GameObject.FindGameObjectWithTag ("GameSettings").GetComponent <GameSettings> ();
		mousePressed = new Vector2 ();
		mouseReleased = new Vector2 ();
	}
	
	void Update () {
		if (Input.GetMouseButton (0)) {
			if (!EventSystem.current.IsPointerOverGameObject((Application.platform == RuntimePlatform.WindowsEditor ? -1 : (Input.GetTouch(0).fingerId)))) {
				if ((Input.mousePosition).x > Screen.width/2){
					gameManager.GetComponent<GameManager> ().PushPlayer (Vector2.right, gameSettings.controlForce);

				}
				else if ((Input.mousePosition).x < Screen.width/2){
					gameManager.GetComponent<GameManager> ().PushPlayer (Vector2.left, gameSettings.controlForce);
				}
			}

		}

		if (Input.GetMouseButtonDown (0)) {
			mousePressed = CastRayToClick (Input.mousePosition.x, Input.mousePosition.y);
			swipeStart = Time.time;
		}

		if (Input.GetMouseButtonUp (0)) {
			mouseReleased = CastRayToClick (Input.mousePosition.x, Input.mousePosition.y);
			mouseDeltaX = mouseReleased.x - mousePressed.x;
			mouseDeltaY = mouseReleased.y - mousePressed.y;

			posDeltaPositive = mouseDeltaY - mouseDeltaX;
			posDeltaNegative = mouseDeltaY + mouseDeltaX;

			swipeInterval = Mathf.Min (Time.time - swipeStart, gameSettings.maxSwipeTime);

			// Mullet launching gameplay mechanic.
			if (mouseDeltaY > 0 && posDeltaPositive > 0.02 && posDeltaNegative > 0.02) {
				this.trySwipe (new Vector2 (
					(mouseDeltaX * 50 * gameSettings.accelerationForce) / swipeInterval, 
					(mouseDeltaY * 50 * gameSettings.accelerationForce) / swipeInterval
				));
			}
		}

		if (gameSettings.devMode) {
			if (Input.GetKeyDown (KeyCode.K))
				gameManager.GetComponent<GameManager> ().PlayerCollision ();
		}
	}

	private Vector2 CastRayToClick (float mouseX, float mouseY) {
		return new Vector2 (
			Camera.main.ScreenToViewportPoint (new Vector3 (mouseX, mouseY, 0)).x,
			Camera.main.ScreenToViewportPoint (new Vector3 (mouseX, mouseY, 0)).y
		);
	}

	public void trySwipe (Vector2 swipe) {
		if (swipe.magnitude > gameSettings.minSwipeMagnitude)
			gameManager.GetComponent<GameManager> ().VerticalSwipe (swipe);
	}
}
