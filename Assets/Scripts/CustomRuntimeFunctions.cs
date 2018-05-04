using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CustomRuntimeFunctions : EditorWindow{

	[MenuItem("Window/Custom Runtime Functions")]
	public static void ShowWindow () {
		GetWindow<CustomRuntimeFunctions> ("CRF");
	}

	private void OnGUI () {
		if (GUILayout.Button ("Spread Sectors Once")) {
			this.SpreadSectors ();
		}
	}


	private void SpreadSectors () {
		GameObject[] sectors = GameObject.FindGameObjectsWithTag ("Sector");

		foreach (GameObject sector in sectors){
			sector.GetComponent<Sector> ().Spread ();
		}
	}
}
