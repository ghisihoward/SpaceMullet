using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private GameObject gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "PlanetCore") {
			gameManager.GetComponent<GameManager> ().PlayerCollision ();
		}
	}
}
