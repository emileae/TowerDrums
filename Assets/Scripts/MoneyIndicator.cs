using UnityEngine;
using System.Collections;

public class MoneyIndicator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ScaleIndicator (int money, int maxMoney)
	{

		float scaleFraction = (money * 1.0f) / (maxMoney * 1.0f);
		if (scaleFraction <= 0) {
			scaleFraction = 0;
		}
		transform.localScale = new Vector3(1f, 1f, scaleFraction);
	}

}
