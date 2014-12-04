using UnityEngine;
using System.Collections;

public class NetworkManagerScript1 : MonoBehaviour {

	[SerializeField]
	/// <summary>
	/// The description of script.
	/// </summary>
	private string DescriptionOfScript = "ACTIVATES NETWORKING FROM GUI COMMANDS.";

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
	/// Raises the GU event.
	/// </summary>
	void OnGUI() {

		// If the network is neither a server or a client.
		if (!Network.isClient && !Network.isServer) {

			// If the user presses this button, the server function is called.
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server")) {

				// The server function is called along with the type and game name.
				GetComponent<StartServerScript>().StartServer(GameType,GameName, GameDescription);
			}

			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts")) {
				GetComponent<JoinServerScript> ().RefreshHostList(GameType);
			}

			HostData[] tempHostList = GetComponent<JoinServerScript> ().hostList;

			if (tempHostList != null){
				for (int i = 0; i < tempHostList.Length; i++) {
					// Spawns a button if there are any games in hostList.
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), tempHostList[i].gameName+"\n "+tempHostList[i].comment)){
					 	GetComponent<JoinServerScript>().JoinServer(tempHostList[i]);
					}
				}
			}
		}
	}



}
