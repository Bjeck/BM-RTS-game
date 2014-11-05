using UnityEngine;
using System.Collections;

public class StartServerScript : MonoBehaviour {

	// Starts a server and registers it at unity's maters server.
	public void StartServer(string typeName, string gameName) {
		
		// Initiatlizes dependign on (max amount of players, port)
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	// Is initiated when the server IS created and hereafter spawns a player.
	void OnServerInitialized() {
		Debug.Log("Server Initializied");
		GetComponent<SpawnPlayerScript> ().SpawnPlayer ();
	}
}
