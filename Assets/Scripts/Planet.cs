using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	//variables
	public float varGravitation = 1f;
	private GameObject planetCore;
	private GameObject gameSettings;


	void Start () {

		planetCore = transform.Find ("Core").gameObject;
		gameSettings = GameObject.Find ("GameSettings").gameObject;
	}
	
	public void SomethingInOrbit (GameObject bodyInOrbit) {
		Vector2 distance = planetCore.transform.position - bodyInOrbit.transform.position;
		Vector2 direction = distance;

		float magnitude = distance.sqrMagnitude;
		direction.Normalize();

		Rigidbody2D bodyRb = bodyInOrbit.GetComponent<Rigidbody2D> ();
		float mulletMass = gameSettings.GetComponent<GameSettings> ().mulletMass;

		bodyRb.AddForce (direction * mulletMass * varGravitation / magnitude);
	}
}
