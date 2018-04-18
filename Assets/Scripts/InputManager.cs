using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {


	Vector2 mousePressed;
	Vector2 mouseReleased;

	private float mouseDelta = 0f;
	private float initialTime = 0f;
	private float resultTime = 0f;

	// Use this for initialization
	void Start () {
		mousePressed = new Vector2 ();
		mouseReleased = new Vector2 ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			
			mousePressed = Input.mousePosition;
			Debug.Log (mousePressed);

			initialTime = Time.time;
		}

		if (Input.GetMouseButtonUp (0)) {
			mouseReleased = Input.mousePosition;
			mouseDelta = mouseReleased.y - mousePressed.y;
			resultTime = Time.time - initialTime;

			if (mouseDelta > 0) {
				Debug.Log ("Para cima");
			}
		}
	}
}
