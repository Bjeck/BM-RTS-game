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
	float distanceToEnemy = Mathf.Infinity;
	GameObject closestEnemy = null;
	public bool isMoving = false;
	public bool isAttackMoving = false;

	public bool isAttacking = false;
	public int health = 10;
	public float visionRange = 10;
	public int attackDamage = 10;
	public float attackRange = 10; 
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



		transform.position = new Vector3 (transform.position.x, transform.position.y, 0);

		if (renderer.isVisible && Input.GetMouseButton (0) && mouseScript.isDrawingBox()) { //for selection box. makes itself selection if it is inside the box.
			Vector3 camPos = Camera.main.WorldToScreenPoint (transform.position);
			camPos.y = Mouse.InvertMouseY (camPos.y);
			if(mouseScript.selection.Contains (camPos) && !mouseScript.CheckUnitInList(this))
				mouseScript.AddUnitSelection(this);
			if(!mouseScript.selection.Contains (camPos) && !mouseScript.ShiftKeyDown())
				mouseScript.RemoveUnitSelection(this);
		}

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

		if (closestEnemy != null)
			if(isAttackMoving){
				Debug.Log("IS ATTACK MOVING");
				pathfinder.EndPath();
			}

		if(!isMoving && closestEnemy != null) {
			//isAttacking = true;

			Attack (closestEnemy); //attacks closest enemy

		}

		closestEnemy = null; //resets testing values for next runthrough of the function
		distanceToEnemy = Mathf.Infinity;

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
