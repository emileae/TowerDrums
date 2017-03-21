using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drum : MonoBehaviour {

	public Blackboard blackboard;

	public bool defeated = false;// determine whether drums need to be stopped when all enemies are defeated

	public BuildPoint buildPoint;

	public int cost = 10;
	public int costPerBeat = 1;

	// the audio source determines the hit points, so a deep sound hits harder than a higher pitched sound
	AudioSource audio;

	// beat is the drum/tower's attack frequency
	public float beat = 0.5f;
	public bool play = false;
	public bool playing = false;

	public bool doesDamage = false;

	public List<Enemy> enemyScripts = new List<Enemy>(); 
	public int hitPoints;

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
		if (blackboard.enemiesKilled >= blackboard.currentEnemies){
			defeated = true;
		}
		audio.volume = volume;
		if (play && !playing && !defeated) {
			playing = true;
			StartCoroutine (PlayDrum ());
		}

	}

	IEnumerator PlayDrum ()
	{
		yield return new WaitForSeconds (beat);
		if (blackboard.money >= costPerBeat) {
			// drum beat cost
			blackboard.money -= costPerBeat;

			// Enemy handling
			if (enemyScripts.Count > 0 && doesDamage) {
				for (int i = 0; i < enemyScripts.Count; i++) {
					if (!enemyScripts [i].killed) {
						enemyScripts [i].TakeDamage (hitPoints);
					}
				}
			}

			// resets etc.
			doesDamage = !doesDamage;
			Debug.Log ("Boom! " + doesDamage);
			audio.Play ();
			playing = false;
		} else {
			play = false;
		}
	}

	void OnMouseDown ()
	{
		if (blackboard.money >= cost) {
			buildPoint.towerSelected = true;
			play = true;
			volume = 0.5f;
			Debug.Log ("Selected a Tower");
			for (int i = 0; i < buildPoint.drums.Length; i++) {
				if (buildPoint.drums [i] != gameObject) {
					buildPoint.drums [i].SetActive (false);
				}
			}
			blackboard.money -= cost;
		}

		transform.position = new Vector3(transform.position.x, 1.7f, transform.position.z);
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Enemy")) {
			Enemy enemyScript = col.gameObject.GetComponent<Enemy>();
			enemyScripts.Add(enemyScript);
		}
	}

	void OnTriggerExit (Collider col)
	{
		if (col.CompareTag ("Enemy")) {
			Enemy enemyScript = col.gameObject.GetComponent<Enemy>();
			enemyScripts.Remove(enemyScript);
		}
	}

}
