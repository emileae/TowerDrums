using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public Blackboard blackboard;
	public GameObject enemy1;

	// Use this for initialization
	void Start () {
		if (blackboard == null) {
			blackboard = GameObject.Find("Blackboard").GetComponent<Blackboard>();
		}
		SpawnWave();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnWave ()
	{
		StartCoroutine(SpawnEnemy());
	}

	IEnumerator SpawnEnemy ()
	{
		yield return new WaitForSeconds (blackboard.enemySpawnTime);
		GameObject enemy = Instantiate (enemy1, transform.position, Quaternion.identity) as GameObject;
		blackboard.currentEnemies += 1;

		Enemy enemyScript = enemy.GetComponent<Enemy>();
		enemyScript.destination = blackboard.destination;

		if (blackboard.currentEnemies < blackboard.enemiesPerWave) {
			StartCoroutine(SpawnEnemy());
		}
	}
}
