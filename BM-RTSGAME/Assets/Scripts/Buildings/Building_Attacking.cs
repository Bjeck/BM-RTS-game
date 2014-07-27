using UnityEngine;
using System.Collections;

public class Building_Attacking : Building {

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


	public float visionRange = 10;
	public float attackRange = 10;
	protected float attackTimer = 0;
	public int attackDamage = 10;
	public float attackSpeed = 1;
	public float projectileSpeed = 30; //just some default values so they ain't 0.



	public bool isTargetDirectTarget = false; //used when right-clicking directly on a unit.
	public bool isAttacking = false;
	public bool isTargetAUnit = false;

	
	// Use this for initialization
	public void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	public void Update () {
		base.Update ();



		closestEnemy = null; //resets testing values for runthrough of the function
		distanceToEnemy = Mathf.Infinity;
		
		//UNITS
		if(target == null){ //check if there are units around me.
			unitsAroundMe = Physics.OverlapSphere (transform.position, visionRange, unitlayerMask); //creates a sphere around unit and checks if any collisions with units happen inside it.
			int i = 0;
			foreach(Collider c in unitsAroundMe){
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
					//Debug.Log("MYSELF AND/OR OTHER UNITS ON MY TEAM AROUND ME. I WONT ATTACK THEM. "+c.transform.gameObject.GetComponent<Building>().player1+" "+this.player1);
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

		if(distanceToEnemy <= attackRange && target != null){ //If enemy is within range, Attack! (also check if we're attackmoving or not, because if not, don't attack..(!)
			RaycastHit hit;
			if (Physics.Raycast (transform.position,(target.transform.position-transform.position), out hit, attackRange)) {
				Debug.DrawRay(transform.position,(target.transform.position-transform.position));
				if(hit.transform.gameObject.tag == "Obstacle" || (hit.transform.gameObject.tag == "Building" && isTargetAUnit == true)){
					//	Debug.Log("THERE'S A SOMETHING THERE");
					return;
				}
			}
			//Debug.Log("ATTACKING");
			Attack (target); //attacks closest enemy
		}
	}

	public virtual void Attack(GameObject obj){
		//MAYBE WILL PUT SOMETHING HERE? Otherwise, it's specific for the individual units.
	}
	
	public virtual void StopAttack(){}

}
