using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {

	public GameObject caster;
	public float coolDown = 0;
	public float cost = 0;
	public KeyCode keyToUse;
	public int damage;
	public GameObject targetCursor;
	public Sprite targetCursorGreen;
	public Sprite targetCursorRed;


	// Use this for initialization
	public void Start () {
		//Debug.Log ("START FROM ABILITY");
		caster = this.gameObject;
		//Debug.Log (caster);
	}
	
	// Update is called once per frame
	public void Update () {

		if (Input.GetKeyDown (keyToUse) && caster.GetComponent<Unit>().isSelected) {
			//Debug.Log("ABILITY KEY WAS PRESSED");
			Do ();		
		}
	}


	public virtual void Do(){
		//Debug.Log ("I'M DOING AN ABILITY");

	}
}
