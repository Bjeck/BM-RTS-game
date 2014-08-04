using UnityEngine;
using System.Collections;

public class NetworkManagerScript1 : MonoBehaviour {

	private const string typeName = "Electro";
	private const string gameName = "TestGame";

	public GameObject playerPrefab;
	private GameObject tmpPlaceField;

	[HideInInspector]
	public bool ServerStarted = false;

	//=========================================================================== CREATE

	// Starts a server and registers it at unity's maters server.
	private void StartServer() {
		// Initiatlizes dependign on (max amount of players, port)
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}

	// Is initiated when the server IS created and hereafter spawns a player.
	void OnServerInitialized() {
		Debug.Log("Server Initializied");
		SpawnPlayer();
	}
	
	//=========================================================================== JOIN

	//------------------------------------------------------------------------ LIST OF GAMES
	// List of hosts for in-game search
	private HostData[] hostList;
	
	// Requests a list of games available.
	private void RefreshHostList() {
		MasterServer.RequestHostList(typeName);
	}
	
	// Receives the list of games and stores it into hostList.
	void OnMasterServerEvent(MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	//------------------------------------------------------------------------ JOIN A GAME
	// Asks to join a game.
	private void JoinServer(HostData hostData) {
		Debug.Log("Joining Server - NonHost");
		Network.Connect(hostData);
	}

	// When connected, spawn a player.
	void OnConnectedToServer() {
		Debug.Log("Server Joined");
		SpawnPlayer();
	}

	//=========================================================================== SPAWN

	// Spawning a player, but only if it is below the max of players set. 
	private void SpawnPlayer() {
		Network.Instantiate(playerPrefab, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity, 0);
	}
	
	//=========================================================================== NETWORK MENU
	void OnGUI() {

		// If the network is neither a server or a client. 
		if (!Network.isClient && !Network.isServer) {
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server")) {
				StartServer();
				ServerStarted = true;
			}

			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts")) {
				RefreshHostList();
			}

			if (hostList != null){
				for (int i = 0; i < hostList.Length; i++) {
					// Spawns a button if there are any games in hostList.
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
					JoinServer(hostList[i]);
				}
			}
		}
	}



}
