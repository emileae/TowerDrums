using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public Blackboard blackboard;

	public int killValue = 10;

	public bool killed = false;

	public int health = 100;
	public int speed = 10;

	public bool beingAttacked = false;
	public Transform destination;

	private NavMeshAgent agent;
	public GameObject splat;

	// tons of adjustments for the current waave

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		agent.autoRepath = true;

		if (blackboard == null) {
			blackboard = GameObject.Find("Blackboard").GetComponent<Blackboard>();
		}

		agent.speed = speed + blackboard.currentWave;

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (destination != null) {
			agent.SetDestination (destination.position);
		}

		if (beingAttacked) {
			health -= 1;
		}

		if (health < 0) {
//			Debug.Log ("DEAD!!!!");
			health = 0;
		}

		if (agent.remainingDistance <= 1.0f) {
//			Debug.Log("Arrived safely");
//			blackboard.Lose();
		}

		if (Vector3.Distance(transform.position, agent.destination) <= 1f)
		{
			blackboard.Lose();
		}

	}

	void OnTriggerEnter (Collider col)
	{
		if (col.CompareTag ("Drum")) {
			Drum drumScript = col.gameObject.GetComponent<Drum> ();
			if (drumScript.doesDamage) {
				beingAttacked = true;
			}
		}
	}

	void OnTriggerExit(Collider col){
		beingAttacked = false;
	}

	public void TakeDamage (int damage)
	{
		health -= (damage - blackboard.currentWave);
		if (health <= 0) {
			killed = true;
			blackboard.money += (killValue / blackboard.currentWave);
			blackboard.enemiesKilled += 1;
			gameObject.SetActive(false);
		}
		GameObject blood = Instantiate (splat, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z), Quaternion.Euler(-90, Random.Range(0, 90), 0)) as GameObject;
		blackboard.blood.Add(blood);
	}

}
