using UnityEngine;
using System.Collections;

public class Blackboard : MonoBehaviour {

	public EnemySpawner enemySpawner;
	public GridLayout gridLayout;

	public bool defeated = false;
	public int money = 1000;
	public int enemiesKilled = 0;

	public Transform destination;

	public int waves = 3;
	private int currentWave = 0;
	public float waveSpawnTime = 20.0f;
	public bool currentWaveActive = false;

	public int enemiesPerWave = 10;
	public float enemySpawnTime = 0.5f;

	public int currentEnemies = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!currentWaveActive) {
			currentWaveActive = true;
			defeated = false;
			Debug.Log ("currentWaveActive: " + currentWaveActive);
			currentWave += 1;
			currentEnemies = 0;
			enemiesPerWave += (currentWave * enemiesPerWave);
			enemySpawner.SpawnWave ();
		}

		if (currentEnemies == 0) {
			defeated = true;
		}

		if (money <= 0) {
			Debug.Log("Turn off all drums and sound waves......");
		}
	}

	public void  DeselectAllPoints ()
	{
		gridLayout.currentlySelectedPoint = null;
//		Debug.Log ("gridLayout: " + gridLayout);
//		Debug.Log ("gridLayout.buildPointScripts[i]: " + gridLayout.buildPointScripts [0]);
//		Debug.Log ("arr length: " + gridLayout.buildPointScripts.Length);
		for (int i = 0; i < gridLayout.buildPointScripts.Length; i++) {
			gridLayout.buildPointScripts [i].selected = false;
			gridLayout.buildPointScripts[i].collider.enabled = true;
			gridLayout.buildPointScripts[i].buildOptions.SetActive(false);
			if (gridLayout.buildPointScripts [i].drumsVisible && !gridLayout.buildPointScripts [i].towerSelected) {
				gridLayout.buildPointScripts[i].drumsVisible = false;
				gridLayout.buildPointScripts[i].HideDrums();
			}
		}
	}
}
