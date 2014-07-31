using UnityEngine;
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

	private void SpawnPlayer(){
		if(playerCount<=(players)){

			tmpPlaceField = GameObject.Find ("BasicsSpawn");
			Vector2 playerPosition = tmpPlaceField.GetComponent<PlaceFields>().PlayerPositions[playerCount];

			Network.Instantiate(playerPrefab, playerPosition, Quaternion.identity, 0);
			print(tmpPlaceField.GetComponent<PlaceFields>().PlayerPositions[playerCount]);

			playerCount++;
		}
	}
	
	private void SpawnMap(){
		Instantiate(Map, new Vector2(0.0f,0.0f), Quaternion.identity);
	}


	
	void OnGUI()
	{

		//-------------------- JOIN
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server")){
				SpawnMap();
				StartServer();
				ServerStarted = true;
			}
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts")){
				RefreshHostList();
			}
			if (hostList != null){
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
					JoinServer(hostList[i]);
				}
			}
		}
	}

	

}
