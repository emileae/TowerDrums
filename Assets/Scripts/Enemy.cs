using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public Blackboard blackboard;

	public int killValue = 10;

	public bool killed = false;

	public int health = 100;
	public bool beingAttacked = false;
	public Transform destination;

	private NavMeshAgent agent;

	// Use this for initialization
	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();

		if (blackboard == null) {
			blackboard = GameObject.Find("Blackboard").GetComponent<Blackboard>();
		}
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
			Debug.Log ("DEAD!!!!");
			health = 0;
		}

		if (agent.remainingDistance <= 1.0f) {
			Debug.Log("Arrived safely");
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
		Debug.Log ("TakeDamage");
		health -= damage;
		if (health <= 0) {
			killed = true;
			blackboard.money += killValue;
			blackboard.enemiesKilled += 1;
			gameObject.SetActive(false);
		}
	}

}
