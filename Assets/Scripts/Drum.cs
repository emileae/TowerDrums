using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drum : MonoBehaviour {

	public Blackboard blackboard;

	public bool drumOn = false;

	public BuildPoint buildPoint;

	public int cost = 10;
	public int costPerBeat = 1;

	// the audio source determines the hit points, so a deep sound hits harder than a higher pitched sound
	AudioSource audio;

	// wave
	public float waveRange = 2.0f;

	// beat is the drum/tower's attack frequency
	public float beat = 0.5f;
	public bool play = false;
	public bool playing = false;

	public bool doesDamage = false;

	public List<Enemy> enemyScripts = new List<Enemy>(); 
	public int hitPoints;

	public bool turnedOff = false;

	// volume is the drum/tower's health
	[Range(0.0f, 1.0f)] public float volume = 0.5f;

	// Use this for initialization
	void Start () {

		if (blackboard == null) {
			blackboard = GameObject.Find("Blackboard").GetComponent<Blackboard>();
		}

		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (blackboard.enemiesKilled >= blackboard.currentEnemies){
//			blackboard.defeated = true;
//		}
		audio.volume = volume;
		if (play && !playing) {
			playing = true;
			StartCoroutine (PlayDrum ());
		}

		if (blackboard.silent && !turnedOff) {
			play = false;
			turnedOff = true;
//			DeactivateSoundWave ();
		}

	}

	IEnumerator PlayDrum ()
	{
		yield return new WaitForSeconds (beat);
		if (blackboard.money >= costPerBeat) {
			// drum beat cost
			blackboard.money -= costPerBeat;
			audio.Play ();
			playing = false;
		} else {
//			play = false;
			DeactivateSoundWave ();
		}

	}

	public void ToggleDrum ()
	{
		Debug.Log("TOGGLE DRUMMMMMMMM");
		if (drumOn) {
			DeactivateSoundWave ();
		}else{
			ActivateSoundWave ();
		};
	}

	void ActivateSoundWave ()
	{
		drumOn = true;
		buildPoint.soundWave.SetActive(true);
		play = true;
	}
	void DeactivateSoundWave ()
	{
		drumOn = false;
		play = false;
		if (buildPoint.soundWave != null) {
			buildPoint.soundWave.SetActive (false);
		}
	}

}
