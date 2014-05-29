using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// Use this for initialization
	public int animalLayer = 8;
	void Start () {
		Physics.IgnoreLayerCollision(animalLayer, animalLayer,true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
