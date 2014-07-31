using UnityEngine;
using System.Collections;

public class NetworkManagerScript : MonoBehaviour {
	
	private const string typeName = "Electro";
	private const string gameName = "TestGame";

	public Vector2 SpawnPlayerAt = new Vector3 (0.0f, 0.0f, 0.0f);
	public GameObject playerPrefab;

	[HideInInspector] 
	public bool ServerStarted = false;

	private void StartServer()
	{
		// Initializes server - MAX_AMOUNT_OF_PLAYERS, PORT_NUMBER
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		
		// Register game at master server
		MasterServer.RegisterHost(typeName, gameName);

	}
	
	/// <summary>
	/// Prints message if server is initialized.
	/// </summary>
	void OnServerInitialized() {	
		Debug.Log("Server Initializied");	
		SpawnPlayer();
	}

	// List of hosts
	private HostData[] hostList;

	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}

	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}

	private void JoinServer(HostData hostData)
	{
		Debug.Log("Joining Server - NonHost");
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
		SpawnPlayer();
	}

	
	private void SpawnPlayer()
	{
		Network.Instantiate(playerPrefab, SpawnPlayerAt, Quaternion.identity, 0);
	}
	
	void OnGUI()
	{

		//-------------------- JOIN
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();
					ServerStarted = true;
				
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList();

			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
					JoinServer(hostList[i]);
				}
			}
		}
	}


	

}
