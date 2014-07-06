using UnityEngine;
using System.Collections;
using Pathfinding;

public class Pathfindinger : MonoBehaviour {
	public Vector3 targetPosition;
	private CharacterController controller;
	Unit unitScript;

	Seeker seeker;
	public Path path;
	public bool wasAPressed = false;

	public float speed = 0.001f;
	public float otherSpeed = 10f;
	public float nextWaypointDistance = 3;
	private int currentWaypoint = 0;
	bool AlmostendofPath = false;
	bool endPath = false;
	float timer = 0;
	float tester;

	// Use this for initialization
	void Start () {
		seeker = GetComponent<Seeker> ();
		controller = GetComponent<CharacterController> ();
		unitScript = GetComponent<Unit> ();

		//seeker.StartPath (transform.position, targetPosition, OnPathComplete);

	}

	void OnPathComplete(Path p) {
		//Debug.Log ("YAY! COmpleted the Path! Error? " + p.error);
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}

	}


	void Update(){

		//Debug.Log ("IS PATH BEING DRAWN: "+seeker.isPathBeingDrawn);
		if (Input.GetKeyDown (KeyCode.A))
			wasAPressed = true;

		if (Input.GetKeyDown (KeyCode.H))
				EndPath ();


		if(Input.GetMouseButtonDown(1)){
			RaycastHit hit;

			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100f) && unitScript.GetSelection()){
				targetPosition = hit.point;
				//if(seeker.isPathBeingDrawn)
				//	return;

				Debug.Log(wasAPressed);

				if(wasAPressed){
					Debug.Log("SHOULD ATTACK MOVE");
					unitScript.isAttackMoving = true;
					//EndPath();
					wasAPressed = false;
				}
				else{
					Debug.Log("WONT ATTACK MOVE");
					unitScript.isAttackMoving = false;
				}

				SetPath(targetPosition);
			//	unitScript.isMoving = true;
			}
		}

		//if(
	}
	

	void FixedUpdate () {

		tester = currentWaypoint + 1;
		if (path == null) {
			//unitScript.isMoving = false;
			return;	
		}

		//Debug.Log (path);

		//Debug.Log((currentWaypoint)+" "+path.vectorPath.Count);
		if (tester == path.vectorPath.Count ) {
			AlmostendofPath = true;
			//Debug.Log("ALMOST AT END OF PATH");
			if(timer < 1){
				timer += Time.deltaTime;
			}
			else{
				AlmostendofPath = false;
				timer = 0;
			}
		}

		if (currentWaypoint >= path.vectorPath.Count) {
			//Debug.Log("End of Path Reached"+path.vectorPath.Count+" "+currentWaypoint);
			path = null;
			unitScript.isMoving = false;
			return;
		}
		//Debug.Log ("IS MOVING");
		//unitScript.isMoving = true;

		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		//Debug.Log (dir);
		//controller.SimpleMove (dir);
		if(unitScript.isMoving)
			transform.position += dir * otherSpeed * Time.fixedDeltaTime; //The actual movement of the character.
		//transform.rigidbody.velocity = dir * otherSpeed * Time.fixedDeltaTime;
		//transform.rigidbody.AddForce (dir * otherSpeed * Time.fixedDeltaTime);
		//transform.Translate (dir * otherSpeed * Time.fixedDeltaTime);


		if (Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) { 
			//Debug.Log("INCREMENTINGPATH!");

			if(tester == path.vectorPath.Count && AlmostendofPath && Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]) >= 0.1f){ //letting the unit walk a little longer when it's almost at the end.
				return;
			}

			currentWaypoint++;
			//endPath = false;
			return;
		}

	}

	public void SetPath(Vector3 target){ //Setting the path (from the mouseclick) and starts to walk it.
		//Debug.Log ("SETTING PATH");
		seeker.StartPath(transform.position, target, OnPathComplete);
		unitScript.isMoving = true;
		//Debug.Log ("START PATH: ATTACKING: "+unitScript.isAttacking+". MOVING: "+unitScript.isMoving);
		currentWaypoint = 0;
	}

	public void EndPath(){ //ends the path (used for attacking)
		path = null;
		unitScript.isMoving = false;
		//if(unitScript.isSelected)
			//sDebug.Log ("END PATH: ATTACKING: "+unitScript.isAttacking+". MOVING: "+unitScript.isMoving);
	}
	

}
