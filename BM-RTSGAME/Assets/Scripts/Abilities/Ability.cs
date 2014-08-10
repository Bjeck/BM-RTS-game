using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {

	public GameObject caster;
	public float coolDown = 0;
	public int cost = 0;
	public KeyCode keyToUse;
	public int damage;
	public GameObject targetCursor;
	public Sprite targetCursorGreen;
	public Sprite targetCursorRed;
	Mouse mouseS;
	protected AbilityManager aMan;
	public UserInterfaceGUI guiScript;

	// Use this for initialization
	public void Start () {
		//Debug.Log ("START FROM ABILITY");
		caster = this.gameObject;
		//Debug.Log (caster);
		mouseS = GameObject.Find("Main Camera").GetComponent<Mouse> ();
		aMan = GameObject.Find("AbilityManager").GetComponent<AbilityManager> ();
		guiScript = GameObject.Find ("SpawnGUI").GetComponent<UserInterfaceGUI> ();
	}

	// Update is called once per frame
	public void Update () {

		if (Input.GetKeyDown (keyToUse) && caster.GetComponent<Unit>().isSelected) {
			//Debug.Log("ABILITY KEY WAS PRESSED");
			Do ();		
		}
	}



	public virtual bool Do(){
		//Here we check cost and current energy available etc.
		//Debug.Log(caster.GetComponent<Unit> ().memory);
		if (caster.GetComponent<Unit> ().memory - cost < 0) {
			Debug.Log("NOT ENOUGH memory to cast");
			return false;
		}

		if (aMan.listOfAbilities.Count > 0)
			return false;

		aMan.AddAbilityToList (this);
		//Debug.Log ("I'M DOING AN ABILITY");
		return true;
	}
}
