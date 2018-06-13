using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowClick : MonoBehaviour {

	public float nClicks = 0, clicksNeeded = 20;
	public float moveSpeed = 1f;
	public Vector2 maxVelocity = new Vector2(40f, 40f);
	private bool holding = false, activated = false;
	private Vector2 destination, direction;
	private Transform mullet;

	// Update is called once per frame
	void Start () {
		mullet = gameObject.transform.GetChild (0);
		mullet.GetComponent<Animator> ().enabled = true;
	}

	void Update () {
		if (activated) {
			if (Input.GetMouseButtonDown (0)) {
				holding = true;
			} else if (Input.GetMouseButtonUp (0)) {
				holding = false;
			} 

			if (holding) {
				destination = (Vector2)Camera.main.ScreenToWorldPoint (Input.mousePosition);
				direction = destination - (Vector2)this.transform.position;
				this.transform.GetComponent<Rigidbody2D> ().AddForce ((Vector3)direction * moveSpeed);
			}

			if (this.transform.GetComponent<Rigidbody2D> ().velocity.magnitude > maxVelocity.magnitude) {
				this.transform.GetComponent<Rigidbody2D> ().velocity = maxVelocity;
			}
		} else {
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);

				if (hit != null && hit.collider != null && hit.collider.tag == "MenuMullet") {
					nClicks += 1;
					mullet.GetComponent<Animator> ().SetTrigger ("PlaySquish");

					if (nClicks >= clicksNeeded) {
						activated = true;
						GameSettings gameSettings = GameObject.FindWithTag ("GameSettings").GetComponent<GameSettings> ();
						gameSettings.devMode = true;
						transform.Find ("Mullet").GetComponent<SpriteRenderer> ().sprite = gameSettings.mulletSpriteCosmonaut;
					}
				}
			}
		}
	}
}
