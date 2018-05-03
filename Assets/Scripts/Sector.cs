using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour {

	private GameSettings gameSettings;
	private enum LifeState { Birth, Spread }
	private LifeState currentState = LifeState.Birth;

	public void Start () {
		// Set size based on gamesettings
		// gameSettings = GameObject.FindWithTag ("GameSettings").GetComponent<GameSettings> ();
	}

	public void OnTriggerStay2D (Collider2D coll) {
		if (coll.tag == "SectorCollider" && currentState == LifeState.Birth) {
			this.Spread ();
			currentState = LifeState.Spread;
		} 
	}

	// Review this sickness please.
	public void Spread() {
		// Gets the width and height of this Sector Collider.
		float xOffset = this.GetComponent<BoxCollider2D> ().size.x;
		float yOffset = this.GetComponent<BoxCollider2D> ().size.y;

		// Flips the x if Sector is on the left part of the screen.
		// This will be used to position objects later, so it needs to be negative.
		if (Blitzkrieg.GetGameObjectXFromCenter (this.gameObject) < 0)
			xOffset *= -1;

		// Creates the position for the new Sector.
		float newX = this.transform.position.x + xOffset;
		float newY = this.transform.position.y + yOffset;

		// Tries to Spread
		this.SpreadNode (new Vector3 (newX, transform.position.y, 0));
		this.SpreadNode (new Vector3 (transform.position.x, newY, 0));

		// On the case if the sector is on the center, also creates on the other side.
		if (Blitzkrieg.GetGameObjectXFromCenter (this.gameObject) == 0) {
			newX = this.transform.position.x - xOffset;
			this.SpreadNode (new Vector3 (newX, transform.position.y, 0));
		}

		this.Infect ();
	}

	private void SpreadNode (Vector3 newPosition) {
		// Gets the width and height of this Sector Collider.
		float xOffset = this.GetComponent<BoxCollider2D> ().size.x;
		float yOffset = this.GetComponent<BoxCollider2D> ().size.y;
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
		Instantiate (gameObject, newPosition, Quaternion.identity);	
	}

	public void Infect () {
		// Generate and place a planet randomly inside it.
		this.Die ();
	}

	public void Die () {
		Destroy (this.gameObject);
	}
}
