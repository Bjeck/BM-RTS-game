using UnityEngine;
using System.Collections;

public class NetworkscriptStartServer : MonoBehaviour {
		
	/// <summary>
	/// Starts a server and registers it at unity's maters server.
	/// </summary>
	/// <param name="typeName">Type name.</param>
	/// <param name="gameName">Game name.</param>
	public void StartServer(string typeName, string gameName, string GameDescription) {
		
		// Initiatlizes dependign on (max amount of players, port)
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName, GameDescription);
		//print ("Server hosted - IP: "+MasterServer.ipAddress+" - Port: "+MasterServer.port);
		//print (Network.sendRate);

	}
	
	/// <summary>
	/// Is initiated when the server IS created.
	/// </summary>
	void OnServerInitialized() {
		//Debug.Log("Server Initializied");
		GetComponent<NetworkscriptContentManager> ().ContentManager ();
	}
}
