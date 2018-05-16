using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

	private float gravForce  = 0;
	private float magnitude, mulletMass;
	private GameObject planetCore, planetOrbit, player;
	private GameSettings gameSettings;
	private LevelManager levelManager;
	private PlanetOrbit orbitScript;

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
		// Gotta load this here, because this is called before Start () is called.
		gameSettings = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager").GetComponent<LevelManager> ();
		planetCore = this.transform.Find ("Core").gameObject;
		planetOrbit = this.transform.Find ("Orbit").gameObject;
		orbitScript = planetOrbit.GetComponent<PlanetOrbit> ();

		// Create appearance.
		GameObject orbitSprite = planetOrbit.transform.Find ("OrbitSprite").gameObject;
		GameObject planetSprites = planetCore.transform.Find ("PlanetSprites").gameObject;

		// Check for Weird Planet Standalone
		if (gameSettings.chanceForWeirdPlanet > Random.Range (0, 100)) {
			planetSprites.GetComponent<SpriteRenderer> ().sprite = levelManager.GetRandomWeirdPlanet ();
			planetSprites.transform.Rotate (0, 0, Random.Range (0f, 360f));
		} else {
			// Make a normal planet otherwise
			// Add one surface
			planetSprites.GetComponent<SpriteRenderer> ().sprite = levelManager.GetRandomSurface ();
			planetSprites.GetComponent<SpriteRenderer> ().sortingOrder = -6;
			planetSprites.transform.Rotate (0, 0, Random.Range (0f, 360f));

			// Check for rare overlays
			if (gameSettings.chanceForWeirdOverlay > Random.Range (0, 100)) {
				GameObject weirdOverlay = new GameObject ("OverlayRare");
				weirdOverlay.transform.SetParent (planetSprites.transform, false);
				weirdOverlay.AddComponent<SpriteRenderer> ();
				weirdOverlay.GetComponent<SpriteRenderer> ().sprite = levelManager.GetRandomOverlayRare ();
				weirdOverlay.transform.Rotate (0, 0, Random.Range (0f, 360f));
			}

			// Add n overlays 
			for (int i = 0; i <= Random.Range(gameSettings.minNOverlays, gameSettings.maxNOverlays); i++){
				GameObject newOverlay = new GameObject ("OverlayN" + i);
				newOverlay.transform.SetParent (planetSprites.transform, false);
				newOverlay.AddComponent<SpriteRenderer> ();
				newOverlay.GetComponent<SpriteRenderer> ().sprite = levelManager.GetRandomOverlay ();
				newOverlay.GetComponent<SpriteRenderer> ().sortingOrder = -i;
				newOverlay.transform.Rotate (0, 0, Random.Range (0f, 360f));
			}

			// Check and add n rings
			if (gameSettings.chanceForRing > Random.Range (0, 100)) {
				for (int i = 0; i <= Random.Range(0, gameSettings.maxNRings); i++){
					GameObject newRing = new GameObject ("Ring" + i);
					newRing.transform.SetParent (planetSprites.transform, false);
					newRing.AddComponent<SpriteRenderer> ();
					newRing.GetComponent<SpriteRenderer> ().sprite = levelManager.GetRandomRing ();
					newRing.GetComponent<SpriteRenderer> ().sortingOrder = -i;
					newRing.transform.Rotate (0, 0, Random.Range (0f, 360f));
				}
			}
		}

		// Give the planet a random scale
		float newScale = Random.Range (gameSettings.minScale, gameSettings.maxScale);
		this.transform.localScale = new Vector3 (newScale, newScale, 1);

		// Give the planet a random orbit and custom orbit image
		float gravityScale = Random.Range (gameSettings.minGrav, gameSettings.maxGrav);
		this.SetGravitationForce (gravityScale);
		float gravStrPercent = (gravityScale - gameSettings.minGrav) / (gameSettings.maxGrav - gameSettings.minGrav);

		float orbitScale = Random.Range (gameSettings.minOrbit, gameSettings.maxOrbit);
		orbitScript.SetOrbit (orbitScale);
		orbitSprite.GetComponent<SpriteRenderer> ().sprite = levelManager.GetProportionalGravityWell (gravStrPercent);
	}
}
