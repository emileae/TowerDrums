using UnityEngine;
using System.Collections;

public class SoundWave : MonoBehaviour {

	public Drum drumScript;

	private Vector3 originalScale;

	public Vector3 targetScale;
    public float smoothTime = 0.2F;
    private Vector3 velocity = Vector3.zero;

    public float waveRange = 1.0f;
    public float timeScaleFactor = 0.5f;


	public bool stopWave = false;

	// Use this for initialization
	void Start () {
		originalScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update ()
	{
//		transform.localScale += new Vector3 (0.1F, 0.1F, 0);
//		if (transform.localScale.x >= 10) {
//			transform.localScale = originalScale;
//		}

//		Vector3 scale = target.TransformPoint(new Vector3(0, 5, -10));
//		transform.localScale = Vector3.SmoothDamp (transform.localScale, targetScale, ref velocity, smoothTime);

//		Debug.Log("transform.localScale.x: " + transform.localScale.x);
//		Debug.Log("waveRange: " + waveRange);

		if (!stopWave) {
			if (transform.localScale.x >= waveRange) {
				transform.localScale = originalScale;
			} else {
//			transform.localScale = Vector3.SmoothDamp (transform.localScale, targetScale, ref velocity, smoothTime);
				transform.localScale = Vector3.Lerp (transform.localScale, targetScale, Time.deltaTime * timeScaleFactor);
			}
		} else {
			transform.localScale = originalScale;
		}

	}
}
