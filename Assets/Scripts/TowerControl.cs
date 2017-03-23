using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerControl : MonoBehaviour {

	public Blackboard blackboard;

	public GameObject tree;

	public GridLayout gridLayout;
//	public List<BuildPoint> gridPointScripts = new List<BuildPoint>();// this doesn't have to be a 2D array

	// Use this for initialization
	void Start () {
		if (blackboard == null) {
			blackboard = GameObject.Find("Blackboard").GetComponent<Blackboard>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		SelectAPoint();
	}

	// Deselect points in Blackboard.cs

	void SelectAPoint ()
	{
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
//				Debug.Log ("TAG: " + hit.transform.gameObject.tag);
				if (hit.transform.gameObject.tag == "buildPoint") {
//					Debug.Log ("Hit build point");

					BuildPoint buildPointScript = hit.transform.gameObject.GetComponent<BuildPoint> ();
					if (!buildPointScript.spawningGround) {
						if (buildPointScript.selected) {
//						Debug.Log ("Deselect this point!!@!@!@!@!@");
							blackboard.DeselectAllPoints ();
						} else {

							Debug.Log ("Not selected........");

							buildPointScript.collider.enabled = false;

							if (!buildPointScript.towerSelected) {
								buildPointScript.buildOptions.SetActive (true);
							} else if (buildPointScript.towerSelected) {
								Debug.Log ("Has tower........");
								buildPointScript.drumScript.ToggleDrum ();
							}

							blackboard.DeselectAllPoints ();
							buildPointScript.selected = !buildPointScript.selected;
							if (gridLayout.currentlySelectedPoint && !buildPointScript.selected) {
								gridLayout.currentlySelectedPoint = null;
							}
							gridLayout.currentlySelectedPoint = hit.transform.gameObject;
						}
					}
				}else if (hit.transform.gameObject.tag == "MenuItem"){
	             	TomoeSelect tomoeScript = hit.transform.gameObject.GetComponent<TomoeSelect>();
					BuildPoint buildPointScript = gridLayout.currentlySelectedPoint.GetComponent<BuildPoint>();
					buildPointScript.SetDrum(tomoeScript.drumType);
	             }
	         }
		}
	}
}
