using UnityEngine;
using System.Collections;

public class SpawnPlayerScript : MonoBehaviour {

	//=========================================================================== SPAWN

	public GameObject PlayerPrefab; 
	public Vector3 PlacePlayerAt;
	

	// Spawning a player, but only if it is below the max of players set. 
	public void SpawnPlayer() {

		networkView.RPC ("PlayerInstantiation", RPCMode.All);
	}

	[RPC]
	private void PlayerInstantiation(){
		Network.Instantiate (PlayerPrefab, PlacePlayerAt, Quaternion.identity, 0);
	}
	
	private void SpawnMap() {
		//Network.Instantiate (MapPrefab, PlaceMapAt, Quaternion.identity, 0);
	}
}
