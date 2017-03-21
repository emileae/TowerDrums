using UnityEngine;
using System.Collections;

public class Blackboard : MonoBehaviour {

	public int money = 1000;
	public int enemiesKilled = 0;

	public Transform destination;

	public int waves = 3;
	private int currentWave = 0;
	public float waveSpawnTime = 20.0f;

	public int enemiesPerWave = 10;
	public float enemySpawnTime = 0.5f;

	public int currentEnemies = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
