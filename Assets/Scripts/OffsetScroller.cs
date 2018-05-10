using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScroller : MonoBehaviour {
	
	private GameObject camera;
	public float speedModifier = 0.0002f;
	public Vector2 inputVector;
	public Material newMaterial;
	public Vector2 outputVector = Vector2.zero;
	Vector2 lastPos;
	Vector2 velocityDiff;

	void Start () {
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		lastPos = (camera.transform.position);
		newMaterial = new Material (GetComponent<MeshRenderer> ().material);
		GetComponent<MeshRenderer> ().material = newMaterial;
	}

	void Update () {
		if (float.IsNaN(outputVector.x))
			outputVector = Vector2.zero;
	}

	void LateUpdate(){
		velocityDiff = ((new Vector2 (camera.transform.position.x, camera.transform.position.y) - lastPos) / Time.deltaTime);

		lastPos = camera.transform.position;
		inputVector = velocityDiff;

		outputVector.x += inputVector.x * speedModifier;
		outputVector.y += inputVector.y * speedModifier;
		outputVector.x = Mathf.Repeat (outputVector.x, 1);
		outputVector.y = Mathf.Repeat (outputVector.y, 1);

		GetComponent<MeshRenderer> ().sharedMaterial.SetTextureOffset ("_MainTex", outputVector);
	}
}
