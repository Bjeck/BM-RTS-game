﻿using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {

	private const string typeName = "Electro";
	private const string gameName = "TestGame";

	public int players;
	private int playerCount = 0;
	public GameObject playerPrefab;
	public GameObject Map;
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
		playerCount++;
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
		//SpawnPlayer();
	}

	void OnPlayerConnected(NetworkPlayer player){
		playerCount++;
	}

	//=========================================================================== SPAWN

	// Spawning a player, but only if it is below the max of players set. 
	private void SpawnPlayer() {
		if(playerCount<=(players)){

			tmpPlaceField = GameObject.Find ("BasicsSpawn");
			Vector2 playerPosition = tmpPlaceField.GetComponent<PlaceFields>().PlayerPositions[playerCount];
			// playerPosition
			Network.Instantiate(playerPrefab, playerPosition, Quaternion.identity, 0);
			print(tmpPlaceField.GetComponent<PlaceFields>().PlayerPositions[playerCount]);
		}
	}

	// Spawns the map. Only the creator can do this. And not god ;) or.. well, maybe he can...
	private void SpawnMap() {
		Instantiate(Map, new Vector2(0.0f,0.0f), Quaternion.identity);
	}

	void Start(){

		SpawnMap();
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
