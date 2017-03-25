using UnityEngine;
using System.Collections;

public class GridLayout : MonoBehaviour {

	public GameObject currentlySelectedPoint;

	public GameObject obstaclePrefab;

	public GameObject goal;
	public GameObject spawnPoint;

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
		
		LayoutGrid();

	}

	public void LayoutGrid ()
	{
		gridPoints = new GameObject[groundWidth, groundHeight];
		buildPointScripts = new BuildPoint[groundWidth * groundHeight];

		int buildPointScriptIndex = 0;

		for (int i = 0; i < groundWidth; i++) {
			for (int j = 0; j < groundHeight; j++) {
				float xPos = (i * unitWidth) - (groundWidth / 2) * unitWidth + (0.5f * unitWidth);
				float yPos = -0.49f;
				float zPos = (j * unitWidth) - (groundHeight / 2) * unitWidth + (0.5f * unitWidth);
				GameObject buildPointGameObject = Instantiate (buildPoint, new Vector3 (xPos, yPos, zPos), Quaternion.identity) as GameObject;
				gridPoints [i, j] = buildPointGameObject;
				BuildPoint buildPointScript = buildPointGameObject.GetComponent<BuildPoint> ();
				if (i == groundWidth - 1) {
					buildPointScript.spawningGround = true;
				}
				buildPointScripts [buildPointScriptIndex] = buildPointScript;
				buildPointScriptIndex += 1;
			}
		}

		// Set Goal Position
		float goalXPos = (0 * unitWidth) - (groundWidth / 2) * unitWidth + (0.5f * unitWidth);
		float goalZPos = ((Random.Range(0, groundHeight-1)) * unitWidth) - (groundHeight / 2) * unitWidth + (0.5f * unitWidth);
		goal.transform.position = new Vector3(goalXPos, goal.transform.position.y, goalZPos);

		// Set Spawn Position
		float spawnXPos = ((groundWidth-1) * unitWidth) - ((groundWidth / 2) * unitWidth) + (0.5f * unitWidth);
		float spawnZPos = ((Random.Range(0, groundHeight-1)) * unitWidth) - (groundHeight / 2) * unitWidth + (0.5f * unitWidth);
		spawnPoint.transform.position = new Vector3(spawnXPos, goal.transform.position.y, spawnZPos);

		// go through grid again and place obstacles
		for (int i = 0; i < groundWidth; i++) {
			int blockToAvoid1 = Random.Range(0, groundWidth-1);
			int blockToAvoid2 = Random.Range(0, groundWidth-1);
			int blockToAvoid3 = Random.Range(0, groundWidth-1);
//			Debug.Log("blockToAvoid: " + blockToAvoid);
			for (int j = 0; j < groundHeight; j++) {
//				int blockToAvoid = Random.Range (1, groundWidth - 1);
				float xPos = (i * unitWidth) - (groundWidth / 2) * unitWidth + (0.5f * unitWidth);
				float yPos = -0.49f;
				float zPos = (j * unitWidth) - (groundHeight / 2) * unitWidth + (0.5f * unitWidth);
//				int luckyNum = Random.Range (0, 10);
//				Debug.Log("blockToAvoid: " + blockToAvoid);
//				Debug.Log("i: " + i);
				if (j != blockToAvoid1 && j != blockToAvoid2 && j != blockToAvoid3 ){
					if(i%2 != 0 && i != groundWidth-1) {
						GameObject obstacle = Instantiate (obstaclePrefab, new Vector3 (xPos, yPos, zPos), Quaternion.identity) as GameObject;
					}
				};
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
