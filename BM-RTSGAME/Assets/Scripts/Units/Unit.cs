using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour {


	//NECESSARY STUFF.
	public SpriteRenderer sprtR;
	Mouse mouseScript;
	public Pathfindinger pathfinder;
	Vector3 targetKeeper;
	LayerMask unitlayerMask = (1 << 12); //unit layermask
	LayerMask buildinglayerMask = (1 << 10); //unit layermask
	
	RaycastHit hit;
	Collider[] unitsAroundMe;
	public float distanceToEnemy = Mathf.Infinity;
	GameObject closestEnemy = null;
	public GameObject target = null;
	public GameObject bulletObject;
	public Projectile bulletScript;


	//BOOLEANS
	public bool isSelected = false;
	bool underConstruction = true;
	public bool isMoving = false;
	public bool isAttackMoving = false;
	public bool checkDistanceToTarget = false; //Only used for direct attacking.
	public bool isHoldingPosition = false;
	public bool isTargetDirectTarget = false; //used when right-clicking directly on a unit.
	public bool checkLineOfSight = false;
	public bool isAttacking = false;
	public bool isTargetAUnit = false;

	//STATS
	public int health = 10;
	public float visionRange = 10;
	public int attackDamage = 10;
	public float attackRange = 5;
	public float attackSpeed = 1;
	protected float attackTimer = 0;
	public float projectileSpeed = 30; //just some default values so they ain't 0.
	public int memory = 0;

	public bool player1 = false;


	// Use this for initialization
	public void Start () {
		mouseScript = GameObject.Find("Main Camera").GetComponent<Mouse> ();
		pathfinder = GetComponent<Pathfindinger> ();

	}
	
	// Update is called once per frame
	public void FixedUpdate () {
		if (health <= 0) {
			Die ();
			return;
		}



	//FOR SELECTION

		transform.position = new Vector3 (transform.position.x, transform.position.y, -1);

		if (renderer.isVisible && Input.GetMouseButton (0) && mouseScript.isDrawingBox()) { //for selection box. makes itself selection if it is inside the box.
			Vector3 camPos = Camera.main.WorldToScreenPoint (transform.position);
			camPos.y = Mouse.InvertMouseY (camPos.y);
			if(mouseScript.selection.Contains (camPos) && !mouseScript.CheckUnitInList(this)){
				//Debug.Log("UNIT ADD");
				mouseScript.AddUnitSelection(this);
			}
			if(!mouseScript.selection.Contains (camPos) && !mouseScript.ShiftKeyDown()){
				mouseScript.RemoveUnitSelection(this);
				//Debug.Log("UNIT REMOVE");
			}
		}




	//FOR ATTACKING
		closestEnemy = null; //resets testing values for runthrough of the function
		distanceToEnemy = Mathf.Infinity;

		//UNITS
		if(target == null){ //check if there are units around me.
			unitsAroundMe = Physics.OverlapSphere (transform.position, visionRange, unitlayerMask); //creates a sphere around unit and checks if any collisions with units happen inside it.
			int i = 0;
			foreach(Collider c in unitsAroundMe){
				//Debug.Log("UNITS "+unitsAroundMe[i]);
				if(c.transform.gameObject == transform.gameObject || c.transform.gameObject.GetComponent<Unit>().player1 == this.player1) //it can hit itself, but it shouldn't do anything when it does that.
				{
					//Debug.Log("MYSELF AND/OR OTHER UNITS ON MY TEAM AROUND ME");
				}
				else{
					if(distanceToEnemy > Vector3.Distance(c.transform.position,transform.position)){ //checks which of the enemies in range is the closest. This one it will attack
						closestEnemy = c.gameObject;
						distanceToEnemy = Vector3.Distance(closestEnemy.transform.position,transform.position);
					}
					i++;
				}
			}
			target = closestEnemy;
			isTargetAUnit = true;
		}

		//BUILDINGS
		if (target == null) { //check if there are any buildings around me.
			unitsAroundMe = Physics.OverlapSphere (transform.position, visionRange, buildinglayerMask); //creates a sphere around unit and checks if any collisions with buildings happen inside it.

			int i = 0;
			foreach(Collider c in unitsAroundMe){
				if(c.transform.gameObject == transform.gameObject || c.transform.gameObject.GetComponent<Building>().player1 == this.player1)
				{
				//	Debug.Log("MYSELF AND/OR OTHER UNITS ON MY TEAM AROUND ME. I WONT ATTACK THEM. "+c.transform.gameObject.GetComponent<Building>().player1+" "+this.player1);
				}
				else{
					if(distanceToEnemy > Vector3.Distance(c.transform.position,transform.position)){ //checks which of the enemies in range is the closest. This one it will attack
						closestEnemy = c.gameObject;
						distanceToEnemy = Vector3.Distance(closestEnemy.transform.position,transform.position);
					}
					i++;
				}
			}
			target = closestEnemy;
			isTargetAUnit = false;
			//Debug.Log("BUilding is my target "+target+" "+closestEnemy);
		}

		//SELECT TARGET
		if (target != null){ //if we already have a target, check if we're still within range.
			distanceToEnemy = Vector3.Distance(target.transform.position,transform.position);
			if(distanceToEnemy > visionRange && !isTargetDirectTarget) { //if target is out of visionRange, top targeting it. Except if it's a direct target.
				target = null;
			}
		}

		//Debug.Log(target);
		if (target == null && isAttackMoving && !isMoving) {
			pathfinder.SetPath(targetKeeper);
		}

		if(distanceToEnemy <= attackRange && target != null){ //If enemy is within range, Attack! (also check if we're attackmoving or not, because if not, don't attack..(!)

				if(isAttackMoving){ //if we're attackmoving, stop walking first.
				targetKeeper = pathfinder.targetPosition;
					pathfinder.EndPath();
				}

			if(!isMoving) {
				//check if the target is in direct line of sight.
				RaycastHit hit;
				if (Physics.Raycast (transform.position,(target.transform.position-transform.position), out hit, attackRange)) {
					Debug.DrawRay(transform.position,(target.transform.position-transform.position));
					if(hit.transform.gameObject.tag == "Obstacle" || (hit.transform.gameObject.tag == "Building" && isTargetAUnit == true)){
					//	Debug.Log("THERE'S A SOMETHING THERE");
						checkLineOfSight=true;
						pathfinder.SetPath(target.transform.position);
						return;
					}
				}
				//Debug.Log("ATTACKING");
				Attack (target); //attacks closest enemy
			}
		}
		else if(distanceToEnemy > attackRange && target != null && !checkDistanceToTarget && !isMoving && !isHoldingPosition){ //if inside vision range but outside attackrange, move closer and continously check when we're in range.
			//Debug.Log("CHECK DISTANCE MOVE");
			checkDistanceToTarget = true;
			pathfinder.SetPath(target.transform.position);
		}



		
		if (!isMoving)
			isAttacking = true;
		else if(isMoving)
			isAttacking = false;

	} //end update



	public void SetSelection(bool s){
		isSelected = s;
		sprtR = GetComponent<SpriteRenderer> ();
		if (isSelected) {
			sprtR.color = Color.black;
		} else {
			sprtR.color = Color.white;
		}
	}

	public bool GetSelection(){
		return isSelected;
	}

	public virtual void Attack(GameObject obj){
			//MAYBE WILL PUT SOMETHING HERE? Otherwise, it's specific for the individual units.
	}

	public virtual void StopAttack(){}


	public void TakeMemory(int loss){
		memory = memory - loss;
		if(memory < 0)
			memory = 0;
	}


	public void Die(){ //The unit dies. We should probably add some explosion effects or something cool :D
		Debug.Log ("IM DEAD!");
		mouseScript.RemoveUnitSelection(this);
		Destroy (this.gameObject);

	}



}
