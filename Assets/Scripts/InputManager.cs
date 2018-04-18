using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	private GameObject gameManager;
	private Vector2 mousePressed, mouseReleased;

	private float mouseDelta = 0f;
	private float initialTime = 0f;
	private float swipeInterval = 0f;

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
			mouseDelta = mouseReleased.y - mousePressed.y;

			swipeInterval = Time.time - initialTime;

			if (mouseDelta > 0) {
				// TODO
				// Review Math
				gameManager.GetComponent<GameManager> ().VerticalSwipe ((mouseDelta * 50)/ swipeInterval);
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
