using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScroller : MonoBehaviour {

	private Rigidbody2D mulletBody;
	public Vector2 inputVector;
	public float speedModifier;

	void Start () {
		mulletBody = GameObject.FindGameObjectWithTag ("Player").GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		inputVector = mulletBody.velocity;

		Vector2 outputVector = new Vector2(0,0);
		outputVector.x = Mathf.Repeat (inputVector.x * speedModifier * Time.time, 1);
		outputVector.y = Mathf.Repeat (inputVector.y * speedModifier * Time.time, 1);

		GetComponent<MeshRenderer>().sharedMaterial.SetTextureOffset ("_MainTex", outputVector);
	}
}
