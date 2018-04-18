using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	private GameObject player;
	float lastPlayerY = 0;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		if ((Blitzkrieg.GetGameObjectPosition (player).y > 0.3f) && (player.transform.position.y > lastPlayerY)){
			transform.position = transform.position + new Vector3 (0, player.transform.position.y - lastPlayerY, 0);
		}
		transform.position = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);
		lastPlayerY = player.transform.position.y;
	}
}
