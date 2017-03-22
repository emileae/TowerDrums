using UnityEngine;
using System.Collections;
using System;

public class GridLayout : MonoBehaviour {

	public GameObject currentlySelectedPoint;

	public int groundWidth = 10;
	public int groundHeight = 10;
	public float unitWidth = 10.0f;

	// plane array
	public GameObject[,] gridPoints;
	public BuildPoint[] buildPointScripts;// this doesn't have to be a 2D array

	public GameObject buildPoint;

	// Use this for initialization
	void Start ()
	{
		gridPoints = new GameObject[groundWidth, groundHeight];
		buildPointScripts = new BuildPoint[groundWidth * groundHeight];

		int buildPointScriptIndex = 0;

		for (int i = 0; i < groundWidth; i++) {
			for (int j = 0; j < groundHeight; j++) {
				float xPos = (i * unitWidth) - (groundWidth / 2) * unitWidth;
				float yPos = -0.49f;
				float zPos = (j * unitWidth) - (groundHeight / 2) * unitWidth;
				GameObject buildPointGameObject = Instantiate (buildPoint, new Vector3 (xPos, yPos, zPos), Quaternion.identity) as GameObject;
				gridPoints[i, j] = buildPointGameObject;
				buildPointScripts[buildPointScriptIndex] = buildPointGameObject.GetComponent<BuildPoint>();
				buildPointScriptIndex += 1;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
