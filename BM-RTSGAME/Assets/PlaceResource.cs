using UnityEngine;
using System.Collections;

public class PlaceResource : MonoBehaviour {

	public GameObject Resource;
	public Vector3 ResourcePosition;

	private GameObject Astar;

	// Use this for initialization
	void Awake()
	{
		Instantiate (Resource, transform.position+ResourcePosition, Quaternion.identity);

		//Astar = GameObject.Find ("A*");
		//Astar.GetComponent<AstarPath>().Scan();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
