using UnityEngine;
using System.Collections;

public class Manipulator : MonoBehaviour {

	//Transform objectSelectParticles;
	public Transform selectedObject;
	private Vector3 targetPos;
	private float targetMagnitude;
	public float lerpSpeed;
	public float moveForce;
	private bool trackObject;
	public GameObject testTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			GameObject animal;
			animal = hitTest();
			if (animal != null) {
				if (animal.tag == "Animal") {
					selectedObject = animal.transform;
					targetMagnitude = (selectedObject.position - transform.position).magnitude;
				}
			}
		}
		if (Input.GetMouseButton (0)) {
			Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward * 1000);
			targetPos = ray.GetPoint(targetMagnitude);
			testTarget.transform.position = Vector3.Lerp(testTarget.transform.position, targetPos, lerpSpeed);
		}
		if (Input.GetMouseButtonUp (0)) {
			selectedObject = null;
		}
		if (selectedObject != null) {
			//selectedObject.transform.position = Vector3.Lerp(selectedObject.transform.position, targetPos, lerpSpeed); 
			Vector3 targetDirection = targetPos - selectedObject.transform.position;
			if (selectedObject.rigidbody != null) {
				selectedObject.rigidbody.AddForce(targetDirection * moveForce);
			} else {
				selectedObject.rigidbody2D.AddForce(targetDirection * moveForce);
			}
		}
	}

	GameObject hitTest ()
	{
		GameObject hitObj;
		//	    if (Input.GetMouseButtonDown(0)) {
		RaycastHit hit;
		//Ray ray = Camera.main.ScreenPointToRay (Camera.main.transform.forward);
		Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward * 1000);
		Physics.Raycast (ray, out hit);
		hitObj = hit.transform.gameObject;
		//            if (hit.transform.gameObject == gameObject) {
		//                    Debug.Log("Hit " + hit.transform.gameObject.name);
		////					Debug.Log("GameObject " + gameObject.name);
		//                }
		//       }
		return hitObj;
	}
}
