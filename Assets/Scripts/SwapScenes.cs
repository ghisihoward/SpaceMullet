using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapScenes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClickLaunchButton () {
		SceneManager.LoadScene ("Main");
	}

	public void backToMenu () {
		SceneManager.LoadScene ("Menu");
	}

	public void creditsButton () {
		SceneManager.LoadScene ("Credits");
	}
}
