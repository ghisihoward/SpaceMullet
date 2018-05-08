using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour {

	public AudioClip menuMusic;
	public AudioClip mainMusic;
	private AudioSource audioSource;
	private Dictionary<string, AudioClip> audiodict;
	private string musicToPlay, musicPlaying;

	static AudioManager instance = null;
	void Awake () {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			audiodict = new Dictionary<string, AudioClip>();
			audiodict.Add ("Menu", menuMusic);
			audiodict.Add ("Credits", menuMusic);
			audiodict.Add ("Main", mainMusic);
			GameObject.DontDestroyOnLoad (gameObject);
			audioSource = GetComponent<AudioSource> ();

			musicToPlay = SceneManager.GetActiveScene ().name;
			this.ChangeMusic (musicToPlay);
		}
	}
	// audioMan.ChangeMusic("Main Music");
	public void ChangeMusic (string musicToPlay) {
		if (musicPlaying == null ||
			!audiodict[musicToPlay].Equals(audiodict[musicPlaying])
		) {
			// If we wanna do fades, here is the thing.
			audioSource.clip = audiodict [musicToPlay];
			audioSource.Play ();
			musicPlaying = musicToPlay;
		}
	}
	/* acesso retorno nome (TipoDeVariavel nomeDeVariavel, ...){
	 * 
	 * }
	 * 
	 */
}