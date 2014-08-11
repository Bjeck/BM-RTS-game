using UnityEngine;
using System.Collections;

public class PlaceResource : MonoBehaviour {

	public GameObject Resource;
	public Vector3 ResourcePosition;

	private GameObject Astar;

	// Use this for initialization
	void Start()
	{
		(Network.Instantiate (Resource, transform.position+ResourcePosition, Quaternion.identity, 0) as GameObject).transform.parent = gameObject.transform;

		//Astar = GameObject.Find ("A*");
		//Astar.GetComponent<AstarPath>().Scan();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
