using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public int damage = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider c){
		if (c.tag == "Obstacle") {
			Destroy(this.gameObject);
		}
		else if(c.tag == "Unit"){
			c.GetComponent<Unit>().health -= damage;
			Destroy(this.gameObject);
		}
	}
}
