using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	public Blackboard blackboard;
	public GameObject enemyS;
	public GameObject enemyM;
	public GameObject enemyL;

	// Use this for initialization
	void Start () {
		if (blackboard == null) {
			blackboard = GameObject.Find("Blackboard").GetComponent<Blackboard>();
		}

//		SpawnWave();
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
		GameObject enemy;

		Debug.Log("ES: " + blackboard.currentWave + " - " + blackboard.numEnemiesS);
		Debug.Log("EM: " + blackboard.currentWave + " - " + blackboard.numEnemiesM);
		Debug.Log("EL: " + blackboard.currentWave + " - " + blackboard.numEnemiesL);

		if (blackboard.numEnemiesS > 0) {
			enemy = Instantiate (enemyS, transform.position, Quaternion.identity) as GameObject;
			blackboard.numEnemiesS -= 1;
			Enemy enemyScript = enemy.GetComponent<Enemy>();
			enemyScript.destination = blackboard.destination;
			blackboard.enemies.Add(enemy);
		}else if (blackboard.numEnemiesM > 0 && blackboard.numEnemiesS <= 0){
			enemy = Instantiate (enemyM, transform.position, Quaternion.identity) as GameObject;
			blackboard.numEnemiesM -= 1;
			Enemy enemyScript = enemy.GetComponent<Enemy>();
			enemyScript.destination = blackboard.destination;
			blackboard.enemies.Add(enemy);
		}else if (blackboard.numEnemiesL > 0 && blackboard.numEnemiesS <= 0 && blackboard.numEnemiesM <= 0){
			enemy = Instantiate (enemyL, transform.position, Quaternion.identity) as GameObject;
			blackboard.numEnemiesL -= 1;
			Enemy enemyScript = enemy.GetComponent<Enemy>();
			enemyScript.destination = blackboard.destination;
			blackboard.enemies.Add(enemy);
		}

		blackboard.currentEnemies += 1;

		if (blackboard.currentEnemies < blackboard.enemiesToKill) {
			StartCoroutine(SpawnEnemy());
		}
	}
}
