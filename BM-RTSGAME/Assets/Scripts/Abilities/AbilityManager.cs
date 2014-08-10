using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityManager : MonoBehaviour {


	Mouse mouseS;
	public List<Ability> listOfAbilities = new List<Ability>();
	List<Unit> currentlySelectedUnits = new List<Unit>();

	
	// Use this for initialization
	void Start () {
		mouseS = GameObject.Find("Main Camera").GetComponent<Mouse> ();
	}


	void Update(){
	}


	public void AddAbilityToList(Ability a){
		listOfAbilities.Add (a);
	}

}
