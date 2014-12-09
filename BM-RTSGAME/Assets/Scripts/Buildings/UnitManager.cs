using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// SCRIPT THAT MANAGES ALL UNITS.
/// </summary>

public class UnitManager : MonoBehaviour {

	//Singleton stuff
	private static UnitManager _instance;

	public static UnitManager instance{
		get{
			if (_instance == null){
				_instance = GameObject.FindObjectOfType<UnitManager>();
			}
			return _instance;
		}
	}

	//Variables
	public List<GameObject> unitsInGame = new List<GameObject>();
	public class unitAbilityRef{
		public string unitID; //needs to be the identifier string given in unit's start()
		public string abilityID; //Needs to be the FULL string. Including the "Ability/"
	}
	public List<unitAbilityRef> ListofAbilityUnits = new List <unitAbilityRef>(); //This list has all information about what units has what abilities. Should never go above 9 items (with 9 abilities)

	// Use this for initialization
	void Start () {
	}


	public void DoesUnitHaveAbility(Unit u){ //function called by units when they spawn to check if they have abilities on them.
	//	Debug.Log("LOOKING FOR ABILITIES FOR "+u.identifier);
		foreach (unitAbilityRef uar in ListofAbilityUnits) {
			if(uar.unitID == u.identifier){
				GameObject upgradeAbility = (GameObject)Network.Instantiate(Resources.Load(uar.abilityID,typeof(GameObject)), transform.position, Quaternion.identity, 0);
				upgradeAbility.transform.parent = u.gameObject.transform;
				//Debug.Log("GIVEN ABILITY "+uar.abilityID+" TO "+u.identifier);
			}
		}
	}

	public void SetAbilityForUnit(string abilityName, string unitName){ //When an upgrade has been researched by the upgrade building this function is called to say that all units of this name should now have this ability.
	//	Debug.Log ("SETTING ABILITY"+abilityName+" "+unitName);
		ListofAbilityUnits.Add(new unitAbilityRef{unitID = unitName, abilityID = abilityName});


	}



}
