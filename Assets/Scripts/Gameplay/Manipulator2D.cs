using UnityEngine;
using System.Collections;

public class Manipulator2D : MonoBehaviour {
	
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
	void Update()
	{
		if (Input.touchCount == 1)
		{
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			if (collider2D == Physics2D.OverlapPoint(touchPos))
			{
				//your code
				
			}
		}
	}
}
