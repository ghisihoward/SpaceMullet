using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	private GameSettings gameSettings;
	private GameObject sectors, gameWorld;
	private List<Sprite> surfaces, overlays, rings;

	public void Start () {
		gameWorld = GameObject.FindGameObjectWithTag ("GameWorld");
		gameSettings = GameObject.FindGameObjectWithTag ("GameSettings").GetComponent<GameSettings> ();
		sectors = gameWorld.transform.Find("Sectors").gameObject;
		this.setUpTexturesResources ();
	}

	public void GenerateLevel () {
		Instantiate (gameSettings.sectorPrefab, gameWorld.transform.position, Quaternion.identity, sectors.transform);
	}

	public void CleanGameWorld () {
		foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet"))
			GameObject.Destroy (planet);

		foreach (GameObject sector in GameObject.FindGameObjectsWithTag("Sector"))
			GameObject.Destroy (sector);
	}

	private void setUpTexturesResources () {
		surfaces = loadTextures("Planets/Surfaces/");
		overlays = loadTextures("Planets/Overlays/");
		rings = loadTextures("Planets/Rings/");
	}

	private List<Sprite> loadTextures (string folder) {
		try {
			return Resources.LoadAll(folder, typeof(Sprite)).Cast<Sprite>().ToList();
		} catch (UnityException e) {
			Debug.Log ("Loading image database failed:");
			Debug.Log (e);
			return null;
		}
	}

	public Sprite GetRandomSurface () {
		return surfaces [Random.Range (0, surfaces.Count)];
	}

	public Sprite GetRandomOverlay () {
		return overlays [Random.Range (0, overlays.Count)];
	}

	public Sprite GetRandomRing () {
		return rings [Random.Range (0, rings.Count)];
	}
}
