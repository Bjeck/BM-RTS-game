using UnityEngine;
using System.Collections;

public class JoinServerScript : MonoBehaviour {

	// List of hosts for in-game search
	public HostData[] hostList;
	
	// Requests a list of games available.
	public void RefreshHostList(string typeName) {
		MasterServer.RequestHostList(typeName);
	}
	
	// Receives the list of games and stores it into hostList.
	void OnMasterServerEvent(MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.HostListReceived) {
			hostList = MasterServer.PollHostList ();
		}
	}
	
	//------------------------------------------------------------------------ JOIN A GAME
	// Asks to join a game.
	public void JoinServer(HostData hostData) {
		Debug.Log("Joining Server: "+hostData);
		Network.Connect(hostData);
	}
	
	// When connected, spawn a player.
	void OnConnectedToServer() {
		Debug.Log("Server Joined");
		GetComponent<SpawnPlayerScript>().SpawnPlayer();
	}
}
