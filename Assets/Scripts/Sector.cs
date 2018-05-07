using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour {

	public GameObject planetPrefab;
	private GameSettings gameSettings;
	private enum LifeState { Birth, Spread }
	private LifeState currentState = LifeState.Birth;
	private float xOffset, yOffset, newX, newY, planetX, planetY;

	public void Start () {
		// Set size based on gamesettings
		gameSettings = GameObject.FindWithTag ("GameSettings").GetComponent<GameSettings> ();
		this.gameObject.transform.localScale = new Vector3 (
			gameSettings.sectorScale, gameSettings.sectorScale, gameSettings.sectorScale);

		// Gets the width and height of this Sector Collider.
		xOffset = this.GetComponent<BoxCollider2D> ().size.x * gameSettings.sectorScale;
		yOffset = this.GetComponent<BoxCollider2D> ().size.y * gameSettings.sectorScale;

		// Loads prefab
		planetPrefab = gameSettings.planetPrefab;
	}

	public void OnTriggerStay2D (Collider2D coll) {
		if (coll.tag == "SectorCollider" && currentState == LifeState.Birth) {
			this.Spread ();
			currentState = LifeState.Spread;
		} 
	}

	// Review this sickness. ò_ó
	public void Spread() {
		// Creates the position for the new Sector.
		// Flips the x if Sector is on the left part of the screen.
		newX = this.transform.position.x;
		newY = this.transform.position.y + yOffset;
		if (Blitzkrieg.GetGameObjectXFromCenter (this.gameObject) < 0) {
			newX -= xOffset;
		} else {
			newX += xOffset;
		}

		// Tries to Spread
		this.SpreadNode (new Vector3 (newX, this.transform.position.y, 0));
		this.SpreadNode (new Vector3 (this.transform.position.x, newY, 0));

		// On the case if the sector is on the center, also creates on the other side.
		if (Blitzkrieg.GetGameObjectXFromCenter (this.gameObject) == 0) {
			newX = this.transform.position.x - xOffset;
			this.SpreadNode (new Vector3 (newX, this.transform.position.y, this.transform.position.z));
		}

		this.Infect ();
	}

	private void SpreadNode (Vector3 newPosition) {
		// Gets the width and height of this Sector Collider.
		Vector2 boxPoint = new Vector2 (this.transform.position.x, this.transform.position.y);
		Vector2 boxLengths = new Vector2 (xOffset, yOffset);

		// Gets margin position of the sector.
		if (this.gameObject.transform.position.x > newPosition.x) {
			boxPoint.x -= xOffset;
		} else if (this.gameObject.transform.position.x < newPosition.x) {
			boxPoint.x += xOffset;
		} else if (this.gameObject.transform.position.y < newPosition.y) {
			boxPoint.y += yOffset;
		}

		// Checks if a neighbor exists on that position, quits if it does.

		Collider2D[] colliders = Physics2D.OverlapBoxAll (boxPoint, boxLengths/2, 0f);
		foreach (Collider2D collider in colliders) {
			if (collider.tag == "Sector")
				return;
		}

		// Creates it if it does not.
		Instantiate (gameObject, newPosition, Quaternion.identity, this.transform.parent);
	}

	public void Infect () {
		// Infects only if sector lies within minimum limits.
		if (Vector2.Distance (this.gameObject.transform.position, Camera.main.transform.position) > gameSettings.spawnDistance) {
			// Generate and place a planet randomly inside this object limits.
			planetX = this.transform.position.x + ((Random.value - 0.5f) * xOffset);
			planetY = this.transform.position.y + ((Random.value - 0.5f) * yOffset);
			Vector3 planetPosition = new Vector3 (planetX, planetY, GameObject.FindGameObjectWithTag("GameWorld").transform.position.z);

			GameObject planet = Instantiate (planetPrefab, planetPosition, Quaternion.identity, this.transform.parent.parent.Find("Planets"));
			planet.GetComponent<Planet> ().SetRandomPlanet ();

		}	

		this.Die ();
	}

	public void Die () {
		Destroy (this.gameObject);
	}
}
