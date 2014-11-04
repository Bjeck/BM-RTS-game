using UnityEngine;
using System.Collections;

public class NetworkManagerScript1 : MonoBehaviour {

	private const string typeName = "Electro";
	private const string gameName = "TestGame";

	//=========================================================================== NETWORK MENU
	void OnGUI() {
		// If the network is neither a server or a client. 
		if (!Network.isClient && !Network.isServer) {
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server")) {
				GetComponent<StartServerScript>().StartServer(typeName,gameName);
			}

			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts")) {
				GetComponent<JoinServerScript> ().RefreshHostList(typeName);

			}

			HostData[] tempHostList = GetComponent<JoinServerScript> ().hostList;
		
			print(tempHostList);

			if (tempHostList != null){
				for (int i = 0; i < tempHostList.Length; i++) {
					// Spawns a button if there are any games in hostList.
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), tempHostList[i].gameName))
					
					GetComponent<JoinServerScript>().JoinServer(tempHostList[i]);
				}
			}
		}
	}



}
