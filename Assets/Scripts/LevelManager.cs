using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


	public GameObject planetPrefab, sectorPrefab;
	private GameSettings gameSettings;
	private GameObject gameWorld;

	public int quantidadeDePlanetas;

	public void Start () {
		gameSettings = GameObject.FindWithTag ("GameSettings").GetComponent<GameSettings> ();
		gameWorld = GameObject.FindGameObjectWithTag ("GameWorld");
	}

	public void GenerateLevel (bool alt = false) {
		if (!alt) {
			this.GenerateOld ();
		} else {
			this.GenerateNew ();
		}
	}

	public void GenerateOld () {
		Random.InitState (10);

		for (int i = 0; i < quantidadeDePlanetas; i++) {
			float spawnX = Random.Range (-20, 20);
			float spawnY = Random.Range (0, 40);
			Vector3 spawnPosition = new Vector3 (spawnX, spawnY, 0);

			GameObject newPlanet = Instantiate (planetPrefab, gameWorld.transform);
			newPlanet.transform.localPosition = spawnPosition;
			newPlanet.GetComponent<Planet> ().SetRandomPlanet (
				gameSettings.minGrav, gameSettings.maxGrav, gameSettings.minOrbit, gameSettings.maxOrbit
			);
		}
	}

	public void GenerateNew () {
		Instantiate (sectorPrefab, gameWorld.transform);
	}

}
