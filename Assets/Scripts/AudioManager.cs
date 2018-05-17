using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour {

	public AudioClip menuMusic;
	public AudioClip mainMusic;
	private AudioSource audioSource;
	private Dictionary<string, AudioClip> audioDict;
	private string musicToPlay, musicPlaying;

	static AudioManager instance = null;

	void Awake () {
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			audioDict = new Dictionary<string, AudioClip>();
			audioDict.Add ("Menu", menuMusic);
			audioDict.Add ("Credits", menuMusic);
			audioDict.Add ("Main", mainMusic);
			GameObject.DontDestroyOnLoad (gameObject);
			audioSource = GetComponent<AudioSource> ();

			musicToPlay = SceneManager.GetActiveScene ().name;
			this.ChangeMusic (musicToPlay);
		}
	}

	public void ChangeMusic (string musicToPlay) {
		if (musicPlaying == null ||
			!audioDict[musicToPlay].Equals(audioDict[musicPlaying])
		) {
			// If we wanna do fades, here is the thing.
			audioSource.clip = audioDict [musicToPlay];
			audioSource.Play ();
			musicPlaying = musicToPlay;
		}
	}
}