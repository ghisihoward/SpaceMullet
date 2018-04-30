using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


	public GameObject planet;
	public int quantidadeDePlanetas;
	private GameSettings gameSettings;
	private GameObject gameWorld;

	public void GenerateLevel () {
		gameSettings = GameObject.FindWithTag ("GameSettings").GetComponent<GameSettings> ();
		gameWorld = GameObject.FindGameObjectWithTag ("GameWorld");

		Random.InitState (10);

		for (int i = 0; i < quantidadeDePlanetas; i++) {
			float spawnX = Random.Range (-20, 20);
			float spawnY = Random.Range (0, 40);
			Vector3 spawnPosition = new Vector3 (spawnX, spawnY, 0);

			GameObject newPlanet = Instantiate (planet, gameWorld.transform);
			newPlanet.transform.localPosition = spawnPosition;
			newPlanet.GetComponent<Planet> ().SetRandomPlanet (
				gameSettings.minGrav, gameSettings.maxGrav, gameSettings.minOrbit, gameSettings.maxOrbit
			);
		}
	}

}
