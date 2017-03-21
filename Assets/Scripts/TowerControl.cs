using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerControl : MonoBehaviour {

	public GameObject tree;

	public GridLayout gridLayout;
//	public List<BuildPoint> gridPointScripts = new List<BuildPoint>();// this doesn't have to be a 2D array

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SelectAPoint();
	}

	void  DeselectAllPoints ()
	{
		gridLayout.currentlySelectedPoint = null;
//		Debug.Log ("gridLayout: " + gridLayout);
//		Debug.Log ("gridLayout.buildPointScripts[i]: " + gridLayout.buildPointScripts [0]);
//		Debug.Log ("arr length: " + gridLayout.buildPointScripts.Length);
		for (int i = 0; i < gridLayout.buildPointScripts.Length; i++) {
			gridLayout.buildPointScripts [i].selected = false;
			if (gridLayout.buildPointScripts [i].drumsVisible && !gridLayout.buildPointScripts [i].towerSelected) {
				gridLayout.buildPointScripts[i].drumsVisible = false;
				gridLayout.buildPointScripts[i].HideDrums();
			}
		}
	}

	// TODO: have a way to deselect all selected buildpoints when one is selected

	void SelectAPoint ()
	{
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform.gameObject.tag == "buildPoint") {
//	                 Debug.Log ("Hit build point");
					BuildPoint buildPointScript = hit.transform.gameObject.GetComponent<BuildPoint> ();

					DeselectAllPoints ();
					buildPointScript.selected = !buildPointScript.selected;
					if (gridLayout.currentlySelectedPoint && !buildPointScript.selected) {
						gridLayout.currentlySelectedPoint = null;
					}
					gridLayout.currentlySelectedPoint = hit.transform.gameObject;
//	                 Instantiate(tree, hit.transform.position, Quaternion.identity);
	             }
	         }
		}
	}
}
