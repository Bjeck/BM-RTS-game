using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	/// <summary>
	/// The type name of the server. Can be overwritten in the inspector.
	/// </summary>
	public string GameType = "StrategyElectro";
	
	/// <summary>
	/// The game name of the server. Can be overwritten in the inspector.
	/// </summary>
	public string GameName = "The ultimate game";
	
	/// <summary>
	/// The game description of the server. Can be overwritten in the inspector.
	/// </summary>
	public string GameDescription = "All welcome!";
	
	/// <summary>
	/// The number of players.
	/// </summary>
	public int NumberOfPlayers = 2;

	/// <summary>
	/// Raises the GU event.
	/// </summary>
	void OnGUI() {
		
		// If the network is neither a server or a client.
		if (!Network.isClient && !Network.isServer) {
			
			// If the user presses this button, the server function is called.
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server")) {
				
				// The server function is called along with the type and game name.
				GetComponent<NetworkscriptStartServer>().StartServer(GameType,GameName, GameDescription);
			}
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts")) {
				GetComponent<NetworkscriptJoinServer> ().RefreshHostList(GameType);
			}
			
			HostData[] tempHostList = GetComponent<NetworkscriptJoinServer> ().hostList;
			
			if (tempHostList != null){
				for (int i = 0; i < tempHostList.Length; i++) {
					// Spawns a button if there are any games in hostList.
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), tempHostList[i].gameName+"\n "+tempHostList[i].comment)){
						GetComponent<NetworkscriptJoinServer>().JoinServer(tempHostList[i]);
					}
				}
			}
		}
	}
	
	
	
}
