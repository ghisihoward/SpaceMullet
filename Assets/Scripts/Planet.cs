using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	public float gravitationForce;
	private GameObject planetCore;
	private GameObject gameSettings;
	private float magnitude, mulletMass;
	private GameObject player;


	void Start () {
		planetCore = transform.Find ("Core").gameObject;
		gameSettings = GameObject.Find ("GameSettings").gameObject;
		gravitationForce = Random.Range (2,10);
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	public void SomethingInOrbit (GameObject bodyInOrbit) {
		Vector2 distance = planetCore.transform.position - bodyInOrbit.transform.position;
		Vector2 direction = distance;

		Rigidbody2D bodyRb = bodyInOrbit.GetComponent<Rigidbody2D> ();
		mulletMass = gameSettings.GetComponent<GameSettings> ().mulletMass;

		magnitude = distance.sqrMagnitude;
		direction.Normalize();

		bodyRb.AddForce (direction * mulletMass * gravitationForce / magnitude);
	}
	void Update () {

		if (this.transform.position.y < player.transform.position.y - 5f) {

			foreach(Transform child in transform)
			{
				GameObject.Destroy (child.gameObject);
			}

			DestroyObject (this);
		}
	}
}
