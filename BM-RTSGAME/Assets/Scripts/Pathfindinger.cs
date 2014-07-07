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

		if (Input.GetKeyDown (KeyCode.A)) //This is used for AttackMoving.
			wasAPressed = true;

		if (Input.GetKeyDown (KeyCode.H)){ //Used for holding Position.
			unitScript.isHoldingPosition = true;
			EndPath ();
		}

		if(Input.GetMouseButtonDown(1)){


			if (unitScript.isSelected) {
				LayerMask layermaskU = (1 << 12);
				RaycastHit hit;
				if(Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100f, layermaskU)){ //If, when you have a unit selected, you right click on another unit, set that unit as target (and attack)
					Debug.Log("SHOULD ATTACK THIS UNIT");
					unitScript.isTargetDirectTarget = true;
					unitScript.target = hit.transform.gameObject;
					return;
				}
			}

			RaycastHit Rayhit;

			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out Rayhit, 100f) && unitScript.GetSelection()){ //When you click on a spot, raycast and set the place as the target position.
				targetPosition = Rayhit.point;

				if(wasAPressed){
					//Debug.Log("SHOULD ATTACK MOVE");
					unitScript.isAttackMoving = true;
					wasAPressed = false;
				}
				else{
					//Debug.Log("WONT ATTACK MOVE");
					unitScript.isAttackMoving = false;
				}
				EndPath();
				unitScript.isTargetDirectTarget = false;
				SetPath(targetPosition);
			}
		}

		//if(
	}
	

	void FixedUpdate () {


		if (path == null) { //if there is no path, don't do anything.
			return;	
		}
		tester = currentWaypoint + 1;

		if (unitScript.checkDistanceToTarget) { //if we're targetting a unit, check if we're in range, and when we are, stop walking.
			//Debug.Log("IS CHECKING DISTANCE TO TARGET "+unitScript.distanceToEnemy+" "+unitScript.attackRange);
			if(unitScript.distanceToEnemy <= unitScript.attackRange){
				//Debug.Log("IS NOW WITHIN RANGE");
				EndPath();
				return;
			}
		}

		if (tester == path.vectorPath.Count ) { //if we're almost at the end of path, wait a little longer before ending the path.
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

		if (currentWaypoint >= path.vectorPath.Count) { //when we reach the end of the path.
			path = null;
			unitScript.isMoving = false;
			return;
		}

		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		//Debug.Log (dir);
		//controller.SimpleMove (dir);
		if(unitScript.isMoving)
			transform.position += dir * otherSpeed * Time.fixedDeltaTime; 	//The actual movement of the character.
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
		//Debug.Log ("SETTING PATH "+unitScript.checkDistanceToTarget);
		seeker.StartPath(transform.position, target, OnPathComplete);
		unitScript.isMoving = true;
		unitScript.isHoldingPosition = false;
		//Debug.Log ("START PATH: ATTACKING: "+unitScript.isAttacking+". MOVING: "+unitScript.isMoving);
		currentWaypoint = 0;
	}

	public void EndPath(){ //ends the path (used for attacking)
		path = null;
		unitScript.isMoving = false;
		unitScript.checkDistanceToTarget = false;
		//if(unitScript.isSelected)
			//sDebug.Log ("END PATH: ATTACKING: "+unitScript.isAttacking+". MOVING: "+unitScript.isMoving);
	}
	

}
