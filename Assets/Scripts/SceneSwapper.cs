using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour {

	private string sceneName;
	private Animator anim;
	private AudioManager audioManager;
	private GameSettings gameSettings;

	void Start () {
		anim = this.gameObject.GetComponent <Animator> ();
		gameSettings = GameObject.FindWithTag ("GameSettings").GetComponent <GameSettings> ();
		anim.speed = gameSettings.transitionSpeed;
	}

	public void animateExit (string name) {
		sceneName = name;
		anim.Play ("FadeOut");
	}

	// This is an Event called by the Animation FadeOut.
	private void changeScene () {
		//why it calls the scene when it should set the music hmmmm
		//ok it calls ChangeMusic when the scene is called buuut
		//it won't set the right music right????? because the other
		//script doesn't set the music, it's just a method to be used
		//    (>.>)

		GameObject audioManagerObject = GameObject.FindGameObjectWithTag ("AudioManager");
		// \(ツ)_ This guy is the object in the game called "Audio Manager"
		// He's so freaking *cool* that no scene *ever* has the power to delete him. Ever.

		audioManager = audioManagerObject.GetComponent<AudioManager> ();
		// Buuuut we need the Script on this guy. AudioManager.cs. 
		// So we used this ugly callback to his object on the game
		// which delivers us ONLY the ugly component we need. ¯\_(ツ)_/¯

		// ∠('-' 」∠)＿
		audioManager.ChangeMusic (sceneName); 
		// Now the *mood* is set, we have have the audioManager and the script
		// so we ask the *cool* dude to start playing music.


		SceneManager.LoadScene (sceneName);
		// \(ツ)_ *FINALLY* we change scene.
		// The cool dude is already playing the right music.
		// He won't stop.
		// He's that cool.
		// ( •_•)>⌐□-□ / (⌐□_□),

		// ヽ(^‿^)ノ

	}


}
