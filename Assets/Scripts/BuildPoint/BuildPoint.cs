using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class BuildPoint : MonoBehaviour {

	private Blackboard blackboard;

	public MeshRenderer renderer;

	public BoxCollider collider;

	public bool selected = false;
	public bool cleared = false;// has tree or doesnt have a tree

	// new Tomoe build options
	public GameObject buildOptions;

	public GameObject Tree;

	public GameObject soundWave;

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
		soundWave.SetActive(true);
		SoundWave soundWaveScript = soundWave.GetComponent<SoundWave>();
		Drum drumScript;
		buildOptions.SetActive(false);
		towerSelected = true;
		blackboard.DeselectAllPoints();
		switch (drum) {
		case 0:
			low.SetActive(true);
			drumScript = low.GetComponent<Drum>();
//			soundWaveScript.timeScaleFactor = drumScript.beat;
			soundWaveScript.waveRange = drumScript.waveRange;
			blackboard.money -= drumScript.cost;
			drumScript.play = true;
			break;
		case 1:
			middle.SetActive(true);
			drumScript = middle.GetComponent<Drum>();
//			soundWaveScript.timeScaleFactor = drumScript.beat;
			soundWaveScript.waveRange = drumScript.waveRange;
			blackboard.money -= drumScript.cost;
			drumScript.play = true;
			break;
		case 2:
			high.SetActive(true);
			drumScript = high.GetComponent<Drum>();
//			soundWaveScript.timeScaleFactor = drumScript.beat;
			soundWaveScript.waveRange = drumScript.waveRange;
			blackboard.money -= drumScript.cost;
			drumScript.play = true;
			break;
		default:
			Debug.Log("Error BuildPoint.cs couldn't build a drum");
			break;
		}
	}

}
