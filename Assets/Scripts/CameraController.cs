using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	
	private GameObject player;
	private GameSettings gameSettings;
	private float lastPlayerY = 0;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		gameSettings = GameObject.Find ("GameSettings").GetComponent<GameSettings> ();
	}

	void Update () {
		if (
			(Blitzkrieg.GetGameObjectPosition (player).y > gameSettings.mulletPositionTreshold) && 
			(player.transform.position.y > lastPlayerY)
		){
			transform.position = transform.position + new Vector3 (0, player.transform.position.y - lastPlayerY, 0);
		}
		transform.position = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);
		lastPlayerY = player.transform.position.y;
	}
}
