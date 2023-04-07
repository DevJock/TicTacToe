//using BeardedManStudios.Network;
//using BeardedManStudios.Network.Unity;
//using UnityEngine;
//using UnityEngine.UI;
//using System.Net;
//using System.Net.NetworkInformation;

//public class MENUNetworkStarter : MonoBehaviour
//{
//	public string hostIP;    
//	public string myIP;                                                                   // IP address
//	public int port;                                                                              // Port number
//	public Networking.TransportationProtocolType protocolType = Networking.TransportationProtocolType.UDP;  // Communication protocol
//	public int playerCount = 2;                                                                            // Maximum player count -- excluding this server
//	public string sceneName = "gameScene_NW";                                                                       // Scene to load                                    // The server browser scene
//	public bool proximityBasedUpdates = false;                                                              // Only send other player updates if they are within range
//	public float proximityDistance = 5.0f;                                                                  // The range for the players to be updated within
//	private NetWorker socket = null;                                                                        // The initial connection socket
//	public float packetDropSimulationChance = 0.0f;
//	public int networkLatencySimulationTime = 0;
//	public string masterServerIp = string.Empty;                                                            // If this has a value then it will register itself on the master server at this location
//	public bool useNatHolePunching = false;
//	public bool showBandwidth = false;
//	public bool isBusyFindingLan = false;


//	private void Awake()
//	{
//		ForgeMasterServer.SetIp(masterServerIp);
//		System.Net.IPAddress[] a = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
//		for (int i = 0; i < a.Length; i++)
//		{
//			myIP += a[i];
//		}
//		myIP = myIP.Trim();
//	}


//	public void Start()
//	{
//		// Assign the text for the input to be whatever is set by default
//		//ipAddressInput.text = host;
//		// These devices have no reason to fire off a firewall check as they are not behind a local firewall
//		#if !UNITY_IPHONE && !UNITY_WP_8_1 && !UNITY_ANDROID
//		// Check to make sure that the user is allowing this connection through the local OS firewall
//		Networking.InitializeFirewallCheck((ushort)port);
//		#endif
//	}

//	/// <summary>
//	/// This method is called when a player connects or disconnects in order to update the player count on Arbiter
//	/// </summary>
//	/// <param name="player">The player that just connected or disconnected</param>
//	private void UpdatePlayerCount(NetworkingPlayer player)
//	{
//		ForgeMasterServer.UpdateServer(masterServerIp, socket.Port, socket.Players.Count);
//	}

//	/// <summary>
//	/// This method is called when the host server button is clicked
//	/// </summary>
//	public void StartServer()
//	{
//		// Create a host connection
//		socket = Networking.Host((ushort)port, protocolType, playerCount, false, useNat: useNatHolePunching);
//		socket.TrackBandwidth = showBandwidth;
	
//		#if !NETFX_CORE
//		if (socket is CrossPlatformUDP)
//		{
//			((CrossPlatformUDP)socket).packetDropSimulationChance = packetDropSimulationChance;
//			((CrossPlatformUDP)socket).networkLatencySimulationTime = networkLatencySimulationTime;
//		}
//		#endif

//		if (!string.IsNullOrEmpty(masterServerIp))
//		{
//			socket.connected += delegate ()
//			{
//				ForgeMasterServer.RegisterServer(masterServerIp, (ushort)port, playerCount, "My Awesome Game Name", "Deathmatch", "Thank you for your support!", sceneName: sceneName);
//			};
			
//			socket.playerConnected += UpdatePlayerCount;
//			socket.playerDisconnected += UpdatePlayerCount;
//		}
//		Go();
//	}

//	public void StartClient()
//	{
//		//host = ipAddressInput.text;
//		if (string.IsNullOrEmpty(hostIP.Trim()))
//		{
//			Debug.Log("No ip address provided to connect to");
//			return;
//		}

//		socket = Networking.Connect(hostIP, (ushort)port, protocolType, false, useNatHolePunching);
//		if (!socket.Connected)
//		{
//			socket.ConnectTimeout = 5000;
//			socket.connectTimeout += ConnectTimeout;
//		}

//		#if !NETFX_CORE
//		if (socket is CrossPlatformUDP)
//			((CrossPlatformUDP)socket).networkLatencySimulationTime = networkLatencySimulationTime;
//		#endif
//		Go();
//	}

//	private void ConnectTimeout()
//	{
//		Debug.LogWarning("Connection could not be established");
//		Networking.Disconnect();
//	}

//	public void TCPLocal()
//	{
//		socket = Networking.Connect("127.0.0.1", (ushort)port, protocolType, false, useNatHolePunching);
//		Go();
//	}

//	public void StartClientLan()
//	{
//		if (isBusyFindingLan)
//			return;
//		isBusyFindingLan = true;
//		Networking.lanEndPointFound += FoundEndpoint;
//		Networking.LanDiscovery((ushort)port, 5000, protocolType, false);
//	}

//	private void FoundEndpoint(IPEndPoint endpoint)
//	{
//		isBusyFindingLan = false;
//		Networking.lanEndPointFound -= FoundEndpoint;
//		if (endpoint == null)
//		{
//			Debug.Log("No server found on LAN");
//			return;
//		}
//		string ipAddress = string.Empty;
//		ushort targetPort = 0;
//		#if !NETFX_CORE
//		ipAddress = endpoint.Address.ToString();
//		targetPort = (ushort)endpoint.Port;
//		#else
//					ipAddress = endpoint.ipAddress;
//					targetPort = (ushort)endpoint.port;
//		#endif
//           MainThreadManager.Run(() => {
//               socket = Networking.Connect(ipAddress, targetPort, protocolType, false, useNatHolePunching);
//               Go();
//           });
//	}

//	private void RemoveSocketReference()
//	{
//		socket = null;
//		Networking.networkReset -= RemoveSocketReference;
//	}

//	private void Go()
//	{
//		Networking.networkReset += RemoveSocketReference;
//		if (proximityBasedUpdates)
//			socket.MakeProximityBased(proximityDistance);
//		socket.serverDisconnected += delegate (string reason)
//		{
//			MainThreadManager.Run(() =>
//			{
//				Debug.Log("The server kicked you for reason: " + reason);
//				#if UNITY_EDITOR
//				UnityEditor.EditorApplication.isPlaying = false;
//				#endif
//			});
//		};

//		if (socket.Connected)
//			MainThreadManager.Run(LoadScene);
//		else
//			socket.connected += LoadScene;
//	}

//	private void LoadScene()
//	{
//		socket.connected -= LoadScene;
//		Networking.SetPrimarySocket(socket);
	
//		#if UNITY_5_3
//		UnitySceneManager.LoadScene(sceneName);
//		#else
//		Application.LoadLevel(sceneName);
//		#endif
//	}
//}