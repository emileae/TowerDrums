using UnityEngine;
using System.Collections;

public class Pole : MonoBehaviour {

	public BuildPoint buildPoint;

	public GameObject fallenTree;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver ()
	{
		if (buildPoint.selected && !buildPoint.cleared) {
//			Debug.Log ("Over " + gameObject.name);
			fallenTree.SetActive(true);
		}
	}

	void OnMouseExit ()
	{
		if (buildPoint.selected && !buildPoint.cleared) {
//			Debug.Log ("Exiting " + gameObject.name);
			fallenTree.SetActive(false);
		}
	}

	void OnMouseDown(){
		if (buildPoint.selected && !buildPoint.cleared) {
			buildPoint.Tree.SetActive(false);
			buildPoint.cleared = true;
			fallenTree.SetActive(true);
		}
	}

}
