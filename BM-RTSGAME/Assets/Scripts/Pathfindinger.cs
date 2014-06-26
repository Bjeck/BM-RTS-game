using UnityEngine;
using System.Collections;
using Pathfinding;

public class Pathfindinger : MonoBehaviour {
	public Vector3 targetPosition;
	private CharacterController controller;

	Seeker seeker;
	public Path path;

	public float speed = 0.001f;
	public float otherSpeed = 10f;
	public float nextWaypointDistance = 3;
	private int currentWaypoint = 0;

	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker> ();
		controller = GetComponent<CharacterController> ();

		seeker.StartPath (transform.position, targetPosition, OnPathComplete);

	}

	void OnPathComplete(Path p) {
		Debug.Log ("YAY! COmpleted the Path! Error? " + p.error);
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}

	}


	void Update(){

		if(Input.GetMouseButtonDown(1)){
			//Plane playerPlane = new Plane(Vector3.up, transform.position);
			//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//float hitdist = 0.0f;
			RaycastHit hit;

			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100f)){
				targetPosition = hit.point;
				seeker.StartPath(transform.position, targetPosition, OnPathComplete);
			}
		}
	}
	

	void FixedUpdate () {
		if (path == null) {
			return;		
		}
		if (currentWaypoint >= path.vectorPath.Count) {
			Debug.Log("End of Path Reached");
			return;
		}

		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		Debug.Log (dir);
		//controller.SimpleMove (dir);
		transform.position += dir * otherSpeed * Time.fixedDeltaTime;
		//transform.rigidbody.velocity += dir * otherSpeed * Time.fixedDeltaTime;
		//transform.rigidbody.AddForce (dir * otherSpeed * Time.fixedDeltaTime);
		//transform.Translate (dir * otherSpeed * Time.fixedDeltaTime);

		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}


	}
}
