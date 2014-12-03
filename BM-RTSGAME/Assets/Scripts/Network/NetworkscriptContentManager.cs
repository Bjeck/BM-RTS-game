using UnityEngine;
using System.Collections;

public class NetworkscriptContentManager : MonoBehaviour {
	
	/// <summary>
	/// The map prefab.
	/// </summary>
	public GameObject MapPrefab;

	/// <summary>
	/// Manager of spawning.
	/// </summary>
	public void ContentManager(){

		Network.Instantiate (MapPrefab, transform.position, Quaternion.identity, 1);
		GetComponent<NetworkscriptSpawnPlayer> ().SpawnPlayer ();
	}
}
