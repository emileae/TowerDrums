using UnityEngine;
using System.Collections;

public class SoundWave : MonoBehaviour {

	private Vector3 originalScale;

	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.localScale += new Vector3 (0.1F, 0.1F, 0);
		if (transform.localScale.x >= 10) {
			transform.localScale = originalScale;
		}
	}
}
