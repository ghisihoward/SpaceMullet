using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	public float gravForce  = 0;
	private float magnitude, mulletMass;
	public GameObject planetCore;
	private GameSettings gameSettings;
	private GameObject player;

	void Start () {
		planetCore = transform.Find ("Core").gameObject;
		gameSettings = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	public void SomethingInOrbit (GameObject bodyInOrbit) {
		Vector2 distance = planetCore.transform.position - bodyInOrbit.transform.position;
		Vector2 direction = distance;

		Rigidbody2D bodyRb = bodyInOrbit.GetComponent<Rigidbody2D> ();
		mulletMass = gameSettings.mulletMass;

		magnitude = distance.sqrMagnitude;
		direction.Normalize();

		bodyRb.AddForce (direction * mulletMass * gravForce / magnitude);
	}

	void Update () {
		if (this.transform.position.y < player.transform.position.y - 5f) {
			foreach(Transform child in transform) {
				GameObject.Destroy (child.gameObject);
			}

			DestroyObject (this);
		}
	}

	public void SetGravitationForce (float newG) {
		gravForce = newG;
	}

	public void SetRandomPlanet (float minF, float maxF, float minO, float maxO) {
		gameSettings = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
		PlanetOrbit orbit = this.transform.Find ("Orbit").gameObject.GetComponent<PlanetOrbit> ();

		float newScale = Random.Range (gameSettings.minScale, gameSettings.maxScale);
		this.transform.localScale = new Vector3 (newScale, newScale, 1);
		this.SetGravitationForce (Random.Range (minF, maxF));
		orbit.SetOrbit (Random.Range (minO, maxO));
	}
}
