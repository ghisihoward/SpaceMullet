using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	private GameSettings gameSettings;
	private GameObject sectors, gameWorld;
	private List<Sprite> gravityWells, overlays, overlaysRare, rings, surfaces, weirdPlanets;

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
		gravityWells = loadTextures("Planets/GravityWells/");
		overlays = loadTextures("Planets/Overlays/");
		overlaysRare = loadTextures("Planets/OverlaysRare/");
		rings = loadTextures("Planets/Rings/");
		surfaces = loadTextures("Planets/Surfaces/");
		weirdPlanets = loadTextures("Planets/WeirdPlanets/");
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

	public Sprite GetRandomOverlay () {
		return overlays [Random.Range (0, overlays.Count)];
	}

	public Sprite GetRandomOverlayRare () {
		return overlaysRare [Random.Range (0, overlaysRare.Count)];
	}

	public Sprite GetRandomRing () {
		return rings [Random.Range (0, rings.Count)];
	}

	public Sprite GetRandomSurface () {
		return surfaces [Random.Range (0, surfaces.Count)];
	}

	public Sprite GetRandomWeirdPlanet () {
		return weirdPlanets [Random.Range (0, weirdPlanets.Count)];
	}

	public Sprite GetProportionalGravityWell (float strPercentage) {
		int gravSelection = Mathf.RoundToInt((gravityWells.Count - 1) * strPercentage);
		return gravityWells [gravSelection];
	}
}
