using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Blackboard : MonoBehaviour {

	public EnemySpawner enemySpawner;
	public GridLayout gridLayout;

	// Goal blood
	public GameObject goalBlood;

	// money indicator
	public bool increaseMoney = false;
	public MoneyIndicator moneyIndicator;


	public int originalNumEnemiesS = 10;
	public int originalNumEnemiesM = 4;
	public int originalNumEnemiesL = 2;

	public int numEnemiesS = 10;
	public int numEnemiesM = 4;
	public int numEnemiesL = 2;

	public bool defeated = false;
	public int maxMoney = 1000;
	public int money = 1000;
	public int enemiesToKill;
	public int enemiesKilled = 0;

	public Transform destination;

//	public int waves = 3;
	public int currentWave = 1;
	public float waveSpawnTime = 20.0f;
	public bool currentWaveActive = false;

	public float enemySpawnTime = 0.5f;

	public int currentEnemies = 0;

	public bool silent = false;

	// start attack
	public bool incomingWave = false;
	public bool startAttack = false;

	// win lose audio
	public AudioSource victorySound;
	public AudioSource loseSound;

	// general drum audio
	public AudioSource lowDrum;
	public int numLowDrums = 0;
	private bool lowDrumPlaying = false;
	public AudioSource middleDrum;
	public int numMiddleDrums = 0;
	private bool middleDrumPlaying = false;
	public AudioSource highDrum;
	public int numHighDrums = 0;
	private bool highDrumPlaying = false;

	// blood / enemy list
	public List<GameObject> enemies = new List<GameObject>();
	public List<GameObject> blood = new List<GameObject>();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
	{
//		if (!currentWaveActive) {
//			currentWaveActive = true;
//			defeated = false;
//			Debug.Log ("currentWaveActive: " + currentWaveActive);
//			currentWave += 1;
//			currentEnemies = 0;
//			enemiesPerWave += (currentWave * enemiesPerWave);
//			enemySpawner.SpawnWave ();
//		}

//		if (currentEnemies == 0) {
//			defeated = true;
//		}

		if (money <= 0 && enemiesKilled < enemiesToKill && !defeated) {
			Lose();
		}

//		Debug.Log("enemiesKilled: " + currentWave + " - " + enemiesKilled);
//		Debug.Log("enemiesToKill: " + currentWave + " - " + enemiesToKill);
//		Debug.Log("startAttack: " + currentWave + " - " + startAttack);
		if (enemiesKilled >= enemiesToKill && startAttack) {
			Win();
		}

		if (money > maxMoney) {
			money = maxMoney;
		}

		moneyIndicator.ScaleIndicator(money, maxMoney);

	}

	public void PlayLowDrum ()
	{
		if (!lowDrumPlaying) {
			lowDrum.Play ();
			lowDrum.loop = true;
			lowDrumPlaying = true;
		}
	}
	public void PlayMiddleDrum ()
	{
		if (!middleDrumPlaying) {
			middleDrum.Play ();
			middleDrum.loop = true;
			middleDrumPlaying = true;
		}
	}
	public void PlayHighDrum ()
	{
		if (!highDrumPlaying) {
			highDrum.Play ();
			highDrum.loop = true;
			highDrumPlaying = true;
		}
	}
	public void StopLowDrum ()
	{
		if (lowDrumPlaying) {
			lowDrum.Stop ();
			lowDrum.loop = false;
			lowDrumPlaying = false;
		}
	}
	public void StopMiddleDrum ()
	{
		if (middleDrumPlaying) {
			middleDrum.Stop ();
			middleDrum.loop = false;
			middleDrumPlaying = false;
		}
	}
	public void StopHighDrum ()
	{
		if (highDrumPlaying) {
			highDrum.Stop ();
			highDrum.loop = false;
			highDrumPlaying = false;
		}
	}

	public void Lose(){
		highDrum.Stop();
		middleDrum.Stop();
		lowDrum.Stop();
		lowDrumPlaying = false;
		middleDrumPlaying = false;
		highDrumPlaying = false;
		defeated = true;
		Debug.Log("You LOSE!!!!!!!!");
		Debug.Log ("Turn off all drums and sound waves......");
		silent = true;
		ResetGridPoints();
		goalBlood.SetActive(true);
		loseSound.Play();

		StartCoroutine(ResetGame ());
	}
	public void Win(){
		Debug.Log("You WIN!!!!!!!!");
		highDrum.Stop();
		middleDrum.Stop();
		lowDrum.Stop();
		lowDrumPlaying = false;
		middleDrumPlaying = false;
		highDrumPlaying = false;
		silent = true;
		incomingWave = false;
		startAttack = false;
		enemiesKilled = 0;
		currentEnemies = 0;

//		money = maxMoney / currentWave;
		money = maxMoney;

		victorySound.Play();
		ResetGridPoints();

		currentWave += 1;
	}

	IEnumerator ResetGame ()
	{
		yield return new WaitForSeconds(3.0f);
		SceneManager.LoadScene ("Main", LoadSceneMode.Single);
	}

	public void StartAttack ()
	{
		numEnemiesS = originalNumEnemiesS*currentWave;
		numEnemiesM = originalNumEnemiesM*currentWave;
		numEnemiesL = originalNumEnemiesL*currentWave;
		enemiesToKill = numEnemiesS + numEnemiesM + numEnemiesL;
		Debug.Log("Start Attack: " + enemiesToKill);
		silent = false;
		startAttack = true;
		enemySpawner.SpawnWave ();
	}

	public void  DeselectAllPoints ()
	{
		gridLayout.currentlySelectedPoint = null;

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

	void ResetGridPoints(){
		gridLayout.currentlySelectedPoint = null;

		for (int i = 0; i < gridLayout.buildPointScripts.Length; i++) {
			gridLayout.buildPointScripts [i].selected = false;
			gridLayout.buildPointScripts[i].collider.enabled = true;
			gridLayout.buildPointScripts[i].buildOptions.SetActive(false);
			gridLayout.buildPointScripts [i].towerSelected = false;
			gridLayout.buildPointScripts[i].drumsVisible = false;
			gridLayout.buildPointScripts[i].HideDrums();
			gridLayout.buildPointScripts[i].low.SetActive(false);
			gridLayout.buildPointScripts[i].middle.SetActive(false);
			gridLayout.buildPointScripts[i].high.SetActive(false);
			gridLayout.buildPointScripts[i].soundWave.SetActive (false);
//			if (gridLayout.buildPointScripts [i].drumsVisible && !gridLayout.buildPointScripts [i].towerSelected) {
//				gridLayout.buildPointScripts[i].drumsVisible = false;
//				gridLayout.buildPointScripts[i].HideDrums();
//			}
		}

		// clean up blood
		for(int i = 0; i<blood.Count; i++){
			Destroy(blood[i]);
		}
		// clean up enemies
		for(int i = 0; i<enemies.Count; i++){
			Destroy(enemies[i]);
		}

		blood = new List<GameObject>();
		enemies = new List<GameObject>();

	}

//	IEnumerator NextWave(){
//		yield return new WaitForSeconds(5.0f);
//		Debug.Log("New wave.....");
//		StartAttack ();
//	}
}
