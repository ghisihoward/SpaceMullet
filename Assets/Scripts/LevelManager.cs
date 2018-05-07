using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


	public GameObject planetPrefab, sectorPrefab;
	private GameObject sectors, gameWorld;

	public void Start () {
		gameWorld = GameObject.FindGameObjectWithTag ("GameWorld");
		sectors = gameWorld.transform.Find("Sectors").gameObject;
	}

	public void GenerateLevel () {
		Instantiate (sectorPrefab, gameWorld.transform.position, Quaternion.identity, sectors.transform);
	}

}
