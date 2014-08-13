using UnityEngine;
using System.Collections;

public class MineScript : MonoBehaviour {

	public bool player1;
	int damage = 60;
	public ParticleSystem mineExp;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider c){
		if (c.gameObject.tag == "Unit"){
			if (c.gameObject.GetComponent<Unit> ().player1 != player1) {
				Debug.Log("EXPLODE!");
				mineExp = (ParticleSystem)Instantiate (mineExp);
				mineExp.transform.position = transform.position;
				mineExp.Play ();
				c.GetComponent<Unit>().health -= damage;
				Destroy(this.gameObject);
			}
		}
	}

}
