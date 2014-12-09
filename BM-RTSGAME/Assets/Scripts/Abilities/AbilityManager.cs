using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The Ability Manager Keeps a full list of all the abilities currently available to the player. These are only the abilities that are currently usable, meaning from the units that are selected.
/// </summary>

public class AbilityManager : MonoBehaviour {


	Mouse mouseS;
	public List<Ability> researchedAbilities = new List<Ability>();
	public List<Ability> listOfCurrentlyCastableAbilities = new List<Ability>();
	List<Unit> currentlySelectedUnits = new List<Unit>();

	public bool isTargetingAbility = false;

	
	// Use this for initialization
	void Start () {
		mouseS = GameObject.Find("Main Camera").GetComponent<Mouse> ();
	}


	void Update(){
	}


	public void AddAbilityToList(Ability a){
		listOfCurrentlyCastableAbilities.Add (a);
	}

}
