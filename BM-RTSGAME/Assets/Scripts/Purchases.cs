using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Purchases : MonoBehaviour {

	[System.Serializable]
	public class UnitClass 
	{
		public GameObject UnitPrefab;
		public int Costs_Watt;
		[HideInInspector]
		public bool bUsed;
	}
	
	public UnitClass[] Units; 

	[System.Serializable]
	public class BuildingClass 
	{
		public GameObject BuildingPrefab;
		public int Costs_Watt;
		[HideInInspector]
		public bool bUsed;
	}
	
	public BuildingClass[] Buildings; 


	void Start(){



	}
}