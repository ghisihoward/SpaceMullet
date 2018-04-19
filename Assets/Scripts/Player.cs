using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private GameObject gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
	}

	void Update(){

		Vector2 velocityVector = this.GetComponent<Rigidbody2D> ().velocity;
		if (velocityVector != Vector2.zero) {
			float angle = Mathf.Atan2 (velocityVector.y, velocityVector.x) * Mathf.Rad2Deg - 90.0f;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "PlanetCore") {
			gameManager.GetComponent<GameManager> ().PlayerCollision ();
		}
	}
}
