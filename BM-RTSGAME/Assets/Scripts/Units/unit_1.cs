using UnityEngine;
using System.Collections;

public class unit_1 : Unit {



	// Use this for initialization
	void Start () {
		base.Start();

		health = 100;
		attackSpeed = 0.8f;
		attackRange = 10f;
		visionRange = 10;
		projectileSpeed = 30;


	}
	
	// Update is called once per frame
	void Update () {
		base.FixedUpdate ();

		if (!isMoving)
			isAttacking = true;
		else if(isMoving)
			isAttacking = false;


		//if(isSelected)
			//Debug.Log ("START PATH: ATTACKING: "+isAttacking+". MOVING: "+isMoving);

	}


	//void OnCollisionEnter(Collision c){
	//	Debug.Log ("UNIT COLLUSION");
	//	base.OnCollisionEnter (c);
	//}


	public override void Attack(GameObject obj){
		if(isAttacking){
			//pathfinder.EndPath();
			Vector3 direction = obj.transform.position - transform.position;
			float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg+90;
			//Debug.Log(attackTimer);
			if(attackTimer == 0){
				bulletObject = (GameObject)Instantiate(Resources.Load("bullet_1",typeof(GameObject)));
				bulletScript = bulletObject.GetComponent<Projectile>();
				bulletScript.damage = attackDamage;
				bulletObject.transform.position = transform.position+direction.normalized;
				bulletObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //Rotation is set to be the direction it is flying in.
				bulletObject.GetComponent<Rigidbody> ().velocity = direction.normalized*projectileSpeed;
				attackTimer+=0.01f;
			}
			else if(attackTimer < attackSpeed && attackTimer > 0){
				attackTimer += Time.fixedDeltaTime;
			}
			else if(attackTimer > attackSpeed)
				attackTimer = 0;
			//base.Attack (obj);
			if (isSelected) {
				//Debug.Log ("individual attack");
				//Debug.Log ("I'M ATTACKING " + obj.name);
			}
		}
	}


	public override void StopAttack(){
		isAttacking = false;
	}
}
