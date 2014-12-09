using UnityEngine;
using System.Collections;

public class unit_3 : Unit {

	float shotSpread = 0.2f;
	float minSpread = 5f;

	// Use this for initialization
	void Start () {

		
		health = 150;
		attackSpeed = 0.9f;
		attackRange = 5f;
		visionRange = 6f;
		attackDamage = 14;
		projectileSpeed = 10;
		memory = 150;
		identifier = "unit_3";

		base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		base.FixedUpdate ();
		
	}
	
	
	public override void Attack(GameObject obj){ //The Attack function. The parameter is the target unit/gameobject
		if(isAttacking){
			if(obj == this.gameObject)
				return;
			//if(isSelected)
			//Debug.Log("ATTACKING: "+obj);
			Vector3 direction = obj.transform.position - transform.position;
			float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg+90; 						//The direction and angle of the shot is calculated.
			
			if(attackTimer == 0){ 																		//Since the attack is dependent on a timer, it will only shoot a projetile when this is 0.
				bulletObject = (GameObject)Instantiate(Resources.Load("bullet_2",typeof(GameObject)));
				bulletScript = bulletObject.GetComponent<Projectile>();
				bulletScript.damage = attackDamage;														//Creates, intantiates and sets the damage of the bullet.
				bulletScript.range = attackRange+1f;
				bulletScript.unitThatFiredMe = this.gameObject;
				bulletObject.transform.position = transform.position;
				bulletObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 		//Rotation is set to be the direction it is flying in.
				bulletObject.GetComponent<Rigidbody> ().velocity = direction.normalized*projectileSpeed;//Rotation and speed of the bullet.

				bulletObject = (GameObject)Instantiate(Resources.Load("bullet_2",typeof(GameObject)));
				bulletScript = bulletObject.GetComponent<Projectile>();
				bulletScript.damage = attackDamage;														//Creates, intantiates and sets the damage of the bullet.
				bulletScript.range = attackRange+1f;
				bulletScript.unitThatFiredMe = this.gameObject;
				bulletObject.transform.position = transform.position;
				bulletObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 		//Rotation is set to be the direction it is flying in.
				direction.x += Random.Range(-shotSpread,shotSpread)*minSpread;
				direction.y += Random.Range(-shotSpread,shotSpread)*minSpread;
				bulletObject.GetComponent<Rigidbody> ().velocity = direction.normalized*projectileSpeed;//Rotation and speed of the bullet.


				bulletObject = (GameObject)Instantiate(Resources.Load("bullet_2",typeof(GameObject)));
				bulletScript = bulletObject.GetComponent<Projectile>();
				bulletScript.damage = attackDamage;														//Creates, intantiates and sets the damage of the bullet.
				bulletScript.range = attackRange+1f;
				bulletScript.unitThatFiredMe = this.gameObject;
				bulletObject.transform.position = transform.position;
				bulletObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 		//Rotation is set to be the direction it is flying in.
				direction.x += Random.Range(-shotSpread,shotSpread)*minSpread;
				direction.y += Random.Range(-shotSpread,shotSpread)*minSpread;
				bulletObject.GetComponent<Rigidbody> ().velocity = direction.normalized*projectileSpeed;//Rotation and speed of the bullet.
				



				attackTimer+=0.01f; 																	//so the attackTimer is not forever 0.
			}
			else if(attackTimer < attackSpeed && attackTimer > 0){ 
				attackTimer += Time.fixedDeltaTime; 													//attackTimer will count itself up to a threshold, decided by attackSpeed. 
			}
			else if(attackTimer > attackSpeed) 															//When it goes above this, the unit will attack.
				attackTimer = 0;
			//base.Attack (obj);
		}
	}
	
	
	public override void StopAttack(){
		isAttacking = false;
	}
}
