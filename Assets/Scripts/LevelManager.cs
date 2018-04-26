using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {


	public GameObject planet;

	public int quantidadeDePlanetas;
	void Start () {
		
		Random.seed = 10;

		for (int i = 0; i < quantidadeDePlanetas; i++) {

			float this_x = Random.Range (-20, 20);
			float this_y = Random.Range (0, 40);

			Instantiate(planet, new Vector3(this_x, this_y, 125), Quaternion.identity);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
