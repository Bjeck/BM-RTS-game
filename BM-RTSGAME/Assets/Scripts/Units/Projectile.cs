using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public int damage = 0;
	public float range = 1;
	public GameObject unitThatFiredMe = null;
	float distFromUnitThatFiredMe = 0;
	// Use this for initialization
	void Start () {
		Debug.Log(unitThatFiredMe);
	}
	
	// Update is called once per frame
	void Update () {
		if (unitThatFiredMe != null) { //Checking distance to unitThatFiredMe, and if larger than that unit's attack Range, destroy the bullet
			Debug.Log(unitThatFiredMe);
			distFromUnitThatFiredMe = Vector3.Distance(transform.position,unitThatFiredMe.transform.position);
			//Debug.Log(distFromUnitThatFiredMe);
			if(distFromUnitThatFiredMe > range){
				Destroy(this.gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider c){
		Debug.Log (c);
		if (c.gameObject == unitThatFiredMe) {
			return;		
		}
		if (c.tag == "Obstacle") {
			Destroy(this.gameObject);
		}
		else if(c.tag == "Unit"){
			c.GetComponent<Unit>().health -= damage;
			Destroy(this.gameObject);
		}
		else if(c.tag == "Building"){
			c.GetComponent<Building>().health -= damage;
			Destroy (this.gameObject);
		}
	}
}
