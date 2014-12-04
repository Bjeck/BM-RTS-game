using UnityEngine;
using System.Collections;

public class NetworkscriptJoinServer : MonoBehaviour {

	/// <summary>
	/// List of hosts for refresh.
	/// </summary> 
	public HostData[] hostList;
	
	/// <summary>
	/// Requests a list of games available.
	/// </summary>
	/// <param name="typeName">Type name.</param>
	public void RefreshHostList(string typeName) {
		MasterServer.RequestHostList(typeName);
	}
	
	/// <summary>
	/// Receives the list of games and stores it into hostList.
	/// </summary>
	/// <param name="msEvent">Ms event.</param>
	void OnMasterServerEvent(MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.HostListReceived) {
			hostList = MasterServer.PollHostList ();
		}
	}
	
	/// <summary>
	/// Asks to join a game.
	/// </summary>
	/// <param name="hostData">Host data.</param>
	public void JoinServer(HostData hostData) {
		//Debug.Log("Joining Server: "+hostData);
		Network.Connect(hostData);
	}
	
	/// <summary>
	/// On connected to server, do something. 
	/// </summary>
	void OnConnectedToServer() {
		//Debug.Log("Server Joined");
		GetComponent<NetworkscriptContentManager> ().ContentManager ();
		
	}

}
