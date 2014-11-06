using UnityEngine;
using System.Collections;

public class SpawnPlayerScript : MonoBehaviour {

	public GameObject player;

	public void Update(){
		if (Input.GetKeyDown (KeyCode.T)) {
			SpawnPlayer();
		}
	}

	public void SpawnPlayer(){
		Network.Instantiate(player, transform.position, Quaternion.identity,0);
	}


}