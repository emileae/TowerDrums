using UnityEngine;
using System.Collections;

public class BuildPoint : MonoBehaviour {

	public MeshRenderer renderer;

	public bool selected = false;
	public bool cleared = false;// has tree or doesnt have a tree

	public GameObject Tree;

	// Placed to fell a tree
	public GameObject N;
	public GameObject S;
	public GameObject W;
	public GameObject E;

	// The drums
	public bool drumsVisible = false;
	public GameObject[] drums;
//	public GameObject high;
//	public GameObject middle;
//	public GameObject low;

	// Selected Tower
	public bool towerSelected = false;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<MeshRenderer>();
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
//		high.SetActive (true);
//		middle.SetActive (true);
//		low.SetActive (true);
		for (int i = 0; i < drums.Length; i++) {
			drums[i].SetActive(true);
		}
	}
	public void HideDrums(){
//		high.SetActive(false);
//		middle.SetActive(false);
//		low.SetActive(false);
		for (int i = 0; i < drums.Length; i++) {
			drums[i].SetActive(false);
		}
	}
}
