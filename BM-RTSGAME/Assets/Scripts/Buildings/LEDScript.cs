﻿using UnityEngine;
using System.Collections;

public class LEDScript : Building_Attacking {
	
	// Use this for initialization
	public void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	public void Update () {
		base.Update ();
	}



	public override void Attack(GameObject obj){ //The Attack function. The parameter is the target unit/gameobject
	//	if(isAttacking){ 
			//if(isSelected)
			//Debug.Log("ATTACKING: "+obj);
			Vector3 direction = obj.transform.position - transform.position;
			float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg+90; 						//The direction and angle of the shot is calculated.
			
			if(attackTimer == 0){ 																		//Since the attack is dependent on a timer, it will only shoot a projetile when this is 0.
				bulletObject = (GameObject)Instantiate(Resources.Load("bullet_1",typeof(GameObject)));
				bulletScript = bulletObject.GetComponent<Projectile>();
				bulletScript.damage = attackDamage; 													//Creates, intantiates and sets the damage of the bullet.
				bulletScript.range = attackRange+1f;
				bulletScript.unitThatFiredMe = this.gameObject;
				bulletObject.transform.position = transform.position;
				bulletObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 		//Rotation is set to be the direction it is flying in.
				bulletObject.GetComponent<Rigidbody> ().velocity = direction.normalized*projectileSpeed;//Rotation and speed of the bullet.
				attackTimer+=0.01f; 																	//so the attackTimer is not forever 0.
			}
			else if(attackTimer < attackSpeed && attackTimer > 0){ 
				attackTimer += Time.fixedDeltaTime; 													//attackTimer will count itself up to a threshold, decided by attackSpeed. 
			}
			else if(attackTimer > attackSpeed) 															//When it goes above this, the unit will attack.
				attackTimer = 0;
			//base.Attack (obj);
	//	}
	}
	
	
	public override void StopAttack(){
		isAttacking = false;
	}


}
