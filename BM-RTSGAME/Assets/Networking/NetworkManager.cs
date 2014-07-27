public class NetworkManager : MonoBehaviour {
	
	////////////////////////////////////////////////////////////////
	/////////////////// CREATING A SERVER //////////////////////////
	////////////////////////////////////////////////////////////////
	
	private const string typeName = "Electro";
	private const string gameName = "TestGame";
	
	/// <summary>
	/// Creates a server by initializing it on the network and registers it to the master server.
	/// </summary>
	private void StartServer()
	{
		// Initializes server - MAX_AMOUNT_OF_PLAYERS, PORT_NUMBER
		Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
		
		// Register game at master server
		MasterServer.RegisterHost(typeName, gameName);
		
		// Setting master server as local. 
		MasterServer.ipAddress = "127.0.0.1";
	}
	
	/// <summary>
	/// Prints message if server is initialized.
	/// </summary>
	void OnServerInitialized() {	Debug.Log("Server Initializied");	}
	
	/// <summary>
	/// Start button for initiating server. 
	/// </summary>
	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				StartServer();
		}
	}
	
	////////////////////////////////////////////////////////////////
	/////////////////// JOINING A SERVER ///////////////////////////
	////////////////////////////////////////////////////////////////
	// List of hosts
	private HostData[] hostList;
	
	/// <summary>
	/// Asking for the list of servers.
	/// </summary>
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	/// <summary>
	/// Checking if list is received, and if true, it puts the content into hostList.
	/// </summary>
	/// <param name="msEvent">Ms event.</param>
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <param name="hostData">Host data.</param>
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		Debug.Log("Server Joined");
	}
	
}
