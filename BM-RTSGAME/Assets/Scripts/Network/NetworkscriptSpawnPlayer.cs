using UnityEngine;
using System.Collections;

public class NetworkscriptSpawnPlayer : MonoBehaviour {

	/// <summary>
	/// The prefab of the player.
	/// </summary>
	public GameObject player;

	/// <summary>
	/// Instantiates a player on this position.
	/// </summary>
	public void SpawnPlayer(){
		Network.Instantiate(player, transform.position, Quaternion.identity,0);
	}
	
	
}