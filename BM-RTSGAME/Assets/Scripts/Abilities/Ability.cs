using UnityEngine;
using System.Collections;

/// <summary>
/// The Base Ability Class. This is not actually an ability in itself, only the superclass for other types of abilities.
/// </summary>


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
	public GUIStyle guiStyle;

	// Use this for initialization
	public void Start () {
		//Debug.Log ("START FROM ABILITY");
		caster = this.transform.parent.gameObject;
		//Debug.Log (caster);
		mouseS = GameObject.Find("Main Camera").GetComponent<Mouse> ();
		aMan = GameObject.Find("AbilityManager").GetComponent<AbilityManager> ();
		guiScript = GameObject.Find ("SpawnGUI").GetComponent<UserInterfaceGUI> ();
	}

	// Update is used to see if the player wants to use an ability.
	public void Update () {

		if (Input.GetKeyDown (keyToUse) && caster.GetComponent<Unit>().isSelected) {
			//Debug.Log("ABILITY KEY WAS PRESSED");
			Do ();		
		}
	}

	//Do the ability. What this does depends of the ability, but they has to pass this memory check.
	public virtual bool Do(){
		//Here we check cost and current energy available etc.
		//Debug.Log(caster.GetComponent<Unit> ().memory);
		if (caster.GetComponent<Unit> ().memory - cost < 0) {
			Debug.Log("NOT ENOUGH memory to cast");
			return false;
		}

		if (aMan.listOfCurrentlyCastableAbilities.Count > 0) //Right now this just sees if another ability is being cast, to avoid multicasting. Should probably be changed if we want shift-casting or queueing or something.
			return false;

		aMan.AddAbilityToList (this);
		//Debug.Log ("I'M DOING AN ABILITY");
		return true;
	}
}
