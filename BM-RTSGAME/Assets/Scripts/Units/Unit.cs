using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	public bool isSelected = false;
	bool underConstruction = true;
	public SpriteRenderer sprtR;
	Mouse mouseScript;
	public Pathfindinger pathfinder;
	Vector3 diff;
	LayerMask layerMask = (1 << 12); //unit layermask
	RaycastHit hit;
	Collider[] unitsAroundMe;
	public float distanceToEnemy = Mathf.Infinity;
	GameObject closestEnemy = null;
	public GameObject target = null;
	public bool isMoving = false;
	public bool isAttackMoving = false;
	public bool checkDistanceToTarget = false; //Only used for direct attacking.
	public bool isHoldingPosition = false;
	public bool isTargetDirectTarget = false; //used when right-clicking directly on a unit.

	public bool isAttacking = false;
	public int health = 10;
	public float visionRange = 10;
	public int attackDamage = 10;
	public float attackRange = 5; 
	public float attackSpeed = 1;
	protected float attackTimer = 0;
	public float projectileSpeed = 30; //just some default values so they ain't 0.
	public GameObject bulletObject;
	public Projectile bulletScript;


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
		closestEnemy = null; //resets testing values for next runthrough of the function
		distanceToEnemy = Mathf.Infinity;

		transform.position = new Vector3 (transform.position.x, transform.position.y, 0);

		if (renderer.isVisible && Input.GetMouseButton (0) && mouseScript.isDrawingBox()) { //for selection box. makes itself selection if it is inside the box.
			Vector3 camPos = Camera.main.WorldToScreenPoint (transform.position);
			camPos.y = Mouse.InvertMouseY (camPos.y);
			if(mouseScript.selection.Contains (camPos) && !mouseScript.CheckUnitInList(this))
				mouseScript.AddUnitSelection(this);
			if(!mouseScript.selection.Contains (camPos) && !mouseScript.ShiftKeyDown())
				mouseScript.RemoveUnitSelection(this);
		}



	//FOR ATTACKING
		//if (isSelected)
			//Debug.Log (target+" "+isTargetDirectTarget);

		if(target == null){ 	
			unitsAroundMe = Physics.OverlapSphere (transform.position, visionRange, layerMask); //creates a sphere around unit and checks if any collisions with units happen inside it.
			int i = 0;
			foreach(Collider c in unitsAroundMe){
				if(c.transform.gameObject == transform.gameObject) //it can hit itself, but it shouldn't do anything when it does that.
				{}
				else{
					if(distanceToEnemy > Vector3.Distance(c.transform.position,transform.position)){ //checks which of the enemies in range is the closest. This one it will attack
						closestEnemy = c.gameObject;
						distanceToEnemy = Vector3.Distance(closestEnemy.transform.position,transform.position);
					}
					i++;
				}
			}
			target = closestEnemy;
		}

		if (target != null){
			distanceToEnemy = Vector3.Distance(target.transform.position,transform.position);
			if(distanceToEnemy > visionRange && !isTargetDirectTarget) { //if target is out of visionRange, top targeting it. Except if it's a direct target.
				target = null;
			}
		}


		if(distanceToEnemy <= attackRange && target != null){ //If enemy is within range, Attack! (also check if we're attackmoving or not, because if not, don't attack..(!)

				if(isAttackMoving){ //if we're attackmoving, stop walking first.
					pathfinder.EndPath();
				}

			if(!isMoving) {
				Attack (target); //attacks closest enemy
			}
		}
		else if(distanceToEnemy > attackRange && target != null && !checkDistanceToTarget && !isMoving && !isHoldingPosition){ //if inside vision range but outside attackrange, move closer and continously check when we're in range.
			//Debug.Log("CHECK DISTANCE MOVE");
			checkDistanceToTarget = true;
			pathfinder.SetPath(target.transform.position);
		}



	}

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


	public void Die(){ //The unit dies. We should probably add some explosion effects or something cool :D
		mouseScript.RemoveUnitSelection(this);
		Destroy (this.gameObject);

	}


}
