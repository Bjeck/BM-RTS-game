using UnityEngine;
using System.Collections;

public class NetworkscriptContentManager : MonoBehaviour {
	
	/// <summary>
	/// The map prefab.
	/// </summary>
	public GameObject MapPrefab;

	/// <summary>
	/// The prefab of the player.
	/// </summary>
	public GameObject player;

	/// <summary>
	/// Whether the map is spawned.
	/// </summary>
	public bool MapSpawned = false; 

	/// <summary>
	/// Manager of spawning.
	/// </summary>
	public void ContentManager(){

		if (Network.isServer) {
				Instantiate (MapPrefab, transform.position, Quaternion.identity);
				Instantiate (player, 	transform.position, Quaternion.identity);	
		} 
		if (Network.isClient) {
				Instantiate (player, 	transform.position, Quaternion.identity);	
		}
	}

}
