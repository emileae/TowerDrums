using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drum : MonoBehaviour {

	public Blackboard blackboard;

	public bool drumOn = false;

	public BuildPoint buildPoint;

	public bool lowDrum;
	public bool middleDrum;
	public bool highDrum;

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
	void Start ()
	{

		if (blackboard == null) {
			blackboard = GameObject.Find ("Blackboard").GetComponent<Blackboard> ();
		}

		audio = GetComponent<AudioSource> ();

		if (gameObject.name == "Low") {
			lowDrum = true;
			middleDrum = false;
			highDrum = false;
		}else if (gameObject.name == "Middle") {
			lowDrum = false;
			middleDrum = true;
			highDrum = false;
		}else if (gameObject.name == "High") {
			lowDrum = false;
			middleDrum = false;
			highDrum = true;
		}
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
			Debug.Log("Start playing....... " + blackboard.currentWave);
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
		//costPerBeat = costPerBeat * blackboard.currentWave;
//		Debug.Log("costPerBeat: " + costPerBeat);
//		Debug.Log("costPerBeat: " + costPerBeat);

		Debug.Log("Play the drum: " + blackboard.currentWave);
		
		if (blackboard.money >= costPerBeat) {
			// drum beat cost
			blackboard.money -= costPerBeat;
			if (lowDrum) {
//				Debug.Log("PLAY LOW DRUMMMMM");
				blackboard.PlayLowDrum ();
			}
			if (middleDrum) {
				blackboard.PlayMiddleDrum ();
			}
			if (highDrum) {
				blackboard.PlayHighDrum ();
			}
			playing = false;
		} else {
//			play = false;
			DeactivateSoundWave ();
		}

	}

	public void ToggleDrum ()
	{
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
		Debug.Log("Soundwave deactivateDDDDDDD");
		drumOn = false;
		play = false;
		if (buildPoint.soundWave != null) {
			buildPoint.soundWave.SetActive (false);
		}

		if (lowDrum) {
			blackboard.numLowDrums -= 1;
			blackboard.StopLowDrum ();
		}
		if (middleDrum) {
			blackboard.numMiddleDrums -= 1;
			blackboard.StopMiddleDrum ();
		}
		if (highDrum) {
			blackboard.numHighDrums -= 1;
			blackboard.StopHighDrum ();
		}
	}

}
