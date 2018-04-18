using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {


	private GameObject gameManager;
	private Vector2 mousePressed, mouseReleased;

	private float posY;
	private float mouseDeltaX = 0f;
	private float mouseDeltaY = 0f;
	private float mulletAngle = 0f;
	private float initialTime = 0f;
	private float swipeInterval = 0f;
	private float posDeltaPositive = 0f;
	private float posDeltaNegative = 0f;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		mousePressed = new Vector2 ();
		mouseReleased = new Vector2 ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			mousePressed = CastRayToClick (Input.mousePosition.x, Input.mousePosition.y);
			initialTime = Time.time;
		}

		if (Input.GetMouseButtonUp (0)) {
			mouseReleased = CastRayToClick (Input.mousePosition.x, Input.mousePosition.y);
			mouseDeltaX = mouseReleased.x - mousePressed.x;
			mouseDeltaY = mouseReleased.y - mousePressed.y;

			posDeltaPositive = mouseDeltaY - mouseDeltaX;
			posDeltaNegative = mouseDeltaY + mouseDeltaX;

			swipeInterval = Time.time - initialTime;

			if (mouseDeltaY > 0) {
				// TODO
				// Review Math

				if (posDeltaPositive > 0.02 && posDeltaNegative > 0.02) {
					gameManager.GetComponent<GameManager> ().VerticalSwipe (new Vector2 ((mouseDeltaX * 50) / swipeInterval, (mouseDeltaY * 50) / swipeInterval));
				}
			}
		}
	}

	private Vector2 CastRayToClick (float mouseX, float mouseY) {
		return new Vector2 (
			Camera.main.ScreenToViewportPoint (new Vector3 (mouseX, mouseY, 0)).x,
			Camera.main.ScreenToViewportPoint (new Vector3 (mouseX, mouseY, 0)).y
		);
	}
}
