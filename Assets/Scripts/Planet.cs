using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	private float gravForce  = 0;
	private float magnitude, mulletMass;
	private GameObject planetCore, player;
	private GameSettings gameSettings;
	private LevelManager levelManager;
	private PlanetOrbit orbit;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		gameSettings = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
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
		if (this.transform.position.y <
			player.transform.position.y - gameSettings.maxPlanetPlayerOffset
		) {
			foreach(Transform child in transform) {
				GameObject.Destroy (child.gameObject);
			}

			DestroyObject (this);
		}
	}

	public void SetGravitationForce (float newG) {
		gravForce = newG;
	}

	public void SetRandomPlanet () {
		// Gotta load this here, because Start doesn't run before this is called.
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
		planetCore = transform.Find ("Core").gameObject;
		gameSettings = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
		orbit = this.transform.Find ("Orbit").gameObject.GetComponent<PlanetOrbit> ();

		float newScale = Random.Range (gameSettings.minScale, gameSettings.maxScale);

		this.transform.localScale = new Vector3 (newScale, newScale, 1);
		this.SetGravitationForce (Random.Range (gameSettings.minGrav, gameSettings.maxGrav));
		orbit.SetOrbit (Random.Range (gameSettings.minOrbit, gameSettings.maxOrbit));

		GameObject planetSprites = planetCore.transform.Find ("Planet Sprites").gameObject;
		planetSprites.GetComponent<SpriteRenderer> ().sprite = levelManager.GetRandomSurface ();

		GameObject planetOverlayOne = planetSprites.transform.Find ("Overlay 1").gameObject;
		planetOverlayOne.GetComponent<SpriteRenderer> ().sprite = levelManager.GetRandomOverlay ();

		GameObject planetOverlayTwo = planetSprites.transform.Find ("Overlay 2").gameObject;
		planetOverlayTwo.GetComponent<SpriteRenderer> ().sprite = levelManager.GetRandomOverlay ();
	}
}
