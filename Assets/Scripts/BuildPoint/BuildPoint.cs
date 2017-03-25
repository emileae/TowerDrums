using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class BuildPoint : MonoBehaviour {

	private Blackboard blackboard;

	public MeshRenderer renderer;

	public BoxCollider collider;

	public bool selected = false;
	public bool cleared = false;// has tree or doesnt have a tree

	// is this the column that spawns bad guys
	public bool spawningGround = false;

	// new Tomoe build options
	public GameObject buildOptions;

	public GameObject Tree;

	public GameObject soundWave;

	public Drum drumScript;

	// Placed to fell a tree
//	public GameObject N;
//	public GameObject S;
//	public GameObject W;
//	public GameObject E;

	// The drums
	public bool drumsVisible = false;
//	public GameObject[] drums;
	public GameObject high;
	public GameObject middle;
	public GameObject low;

	// Selected Tower
	public bool towerSelected = false;

	// Use this for initialization
	void Start () {

		if (blackboard == null) {
			blackboard = GameObject.Find("Blackboard").GetComponent<Blackboard>();
		}

		renderer = GetComponent<MeshRenderer>();
		collider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (selected) {
			renderer.material.color = Color.red; 
		} else {
			renderer.material.color = Color.white; 
		}

		if (selected && cleared && !drumsVisible && !towerSelected) {
			drumsVisible = true;
			ShowDrums ();
		}

	}

	void ShowDrums ()
	{
//		for (int i = 0; i < drums.Length; i++) {
//			drums[i].SetActive(true);
//		}
		buildOptions.SetActive(true);
	}
	public void HideDrums(){
//		for (int i = 0; i < drums.Length; i++) {
//			drums[i].SetActive(false);
//		}
		buildOptions.SetActive(false);
	}

	public void SetDrum (int drum)
	{
		if (!blackboard.startAttack) {
			Debug.Log("START ATTACKKKKKK: " + blackboard.currentWave);
			blackboard.StartAttack();
		}

		soundWave.SetActive (true);
		SoundWave soundWaveScript = soundWave.GetComponent<SoundWave> ();
		buildOptions.SetActive (false);
		towerSelected = true;
		blackboard.DeselectAllPoints ();
//		collider.enabled = true;
		switch (drum) {
		case 0:
			low.SetActive (true);
			drumScript = low.GetComponent<Drum> ();
			if (blackboard.money >= drumScript.cost) {
				soundWaveScript.waveRange = drumScript.waveRange;
				soundWaveScript.drumScript = drumScript;
				blackboard.money -= drumScript.cost;
				drumScript.play = true;
				blackboard.numLowDrums += 1;
			} else {
				low.SetActive (false);
				soundWave.SetActive (false);
			}
			break;
		case 1:
			middle.SetActive (true);
			drumScript = middle.GetComponent<Drum> ();
			if (blackboard.money >= drumScript.cost) {
				soundWaveScript.waveRange = drumScript.waveRange;
				soundWaveScript.drumScript = drumScript;
				blackboard.money -= drumScript.cost;
				drumScript.play = true;
				blackboard.numMiddleDrums += 1;
			} else {
				middle.SetActive (false);
				soundWave.SetActive (false);
			}
			break;
		case 2:
			high.SetActive (true);
			drumScript = high.GetComponent<Drum> ();
			if (blackboard.money >= drumScript.cost) {
			soundWaveScript.waveRange = drumScript.waveRange;
			soundWaveScript.drumScript = drumScript;
			blackboard.money -= drumScript.cost;
			drumScript.play = true;
			blackboard.numHighDrums += 1;
			} else {
				high.SetActive (false);
				soundWave.SetActive (false);
			}
			break;
		default:
			Debug.Log ("Error BuildPoint.cs couldn't build a drum");
			break;
		}

	}

}
