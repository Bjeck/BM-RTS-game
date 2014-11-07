using UnityEngine;
using System.Collections;

public class SpawnPlayerScript : MonoBehaviour {

	[SerializeField]
	/// <summary>
	/// The description of script.
	/// </summary>
	private string DescriptionOfScript = "SPAWNS A PLAYER ON THE NETWORK.";

	/// <summary>
	/// The prefab of the player.
	/// </summary>
	public GameObject player;

	/// <summary>
	/// Update this instance.
	/// </summary>
	public void Update(){

		// If T is presses, a player is spawned.
		if (Input.GetKeyDown (KeyCode.T)) {

			// Spawn the prefab assigned from the inspector.
			SpawnPlayer();
		}
	}

	/// <summary>
	/// Instantiates a player on this position.
	/// </summary>
	public void SpawnPlayer(){
		Network.Instantiate(player, transform.position, Quaternion.identity,0);
	}


}