using UnityEngine;
using System.Collections;

public class LoadingProperties : MonoBehaviour {

	[SerializeField]
	/// <summary>
	/// The description of script.
	/// </summary>
	private string DescriptionOfScript = "STORAGE SCRIPT FOR LOADING PROPERTIES.";

	/// <summary>
	/// Viewing distance or "fog" distance, called by "UnitCollisionDetection".
	/// </summary>
	public int ViewDistance;

}
