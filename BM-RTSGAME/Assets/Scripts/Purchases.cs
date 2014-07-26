using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Purchases : MonoBehaviour {

	[System.Serializable]
	public class UnitClass 
	{
		public string Name;
		public string Description;
		public float Speed;
		public int Costs_Watt;
		public Color SkinColor;
		public Color ArmorColor; 
		[HideInInspector]
		public bool bUsed;
	}
	
	public UnitClass[] Units; 

	[System.Serializable]
	public class BuildingClass 
	{
		public string Name;
		public string Description;
		public int Costs_Watt;
		public Color InnerColor;
		public Color OuterColor; 
		[HideInInspector]
		public bool bUsed;
	}
	
	public BuildingClass[] Buildings; 


	void Start(){



	}
}