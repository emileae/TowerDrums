using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GridLayout gridLayout;

    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    void Update ()
	{
//		Vector3 targetPosition =Camera.main.ScreenToWorldPoint(Input.mousePosition);
//		Vector3 followPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
		if (gridLayout.currentlySelectedPoint != null) {
			Vector3 targetPosition = gridLayout.currentlySelectedPoint.transform.position;
			Vector3 followPosition = new Vector3 (targetPosition.x, transform.position.y, targetPosition.z);
			Vector3 diffVector = transform.position - followPosition;
			transform.position = Vector3.SmoothDamp (transform.position, diffVector, ref velocity, smoothTime);

//			transform.LookAt(gridLayout.currentlySelectedPoint.transform);

		}
    }
}
