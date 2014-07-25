using UnityEngine;
using System.Collections;

public class PlaceResource : MonoBehaviour {

	public GameObject Resource;
	public Vector3 ResourcePosition;

	// Use this for initialization
	void Start () {
		Instantiate (Resource, transform.position+ResourcePosition, Quaternion.identity);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
