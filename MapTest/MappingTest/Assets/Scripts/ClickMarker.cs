using UnityEngine;
using System.Collections;

public class ClickMarker : MonoBehaviour {

	public Camera camera;
	public GameObject mark;
	public bool startFollow = false;
	private Vector3 startPos = new Vector3( 0.0f, 0.0f, -4.0f);

	// Use this for initialization
	void Start () {
		mark.transform.position = startPos;
	}
	
	// Update is called once per frame
	void Update () {

		Ray ray = camera.ScreenPointToRay(new Vector3(200, 200, 0));
		Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

	}

}
