using UnityEngine;
using System.Collections;
using Photon;

public class NetworkInit : UnityEngine.MonoBehaviour {

	
	private TypedLobby lobbyName = new TypedLobby("New_Lobby", LobbyType.Default);
	private const string roomName = "RoomName";
	private RoomInfo[] roomsList;
	public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("Beta");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		if (!PhotonNetwork.connected)
		{
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		}
		else if (PhotonNetwork.room == null)
		{
			// Create Room
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
				//PhotonNetwork.CreateRoom(roomName + Guid.NewGuid().ToString("N"), true, true, 5);
			//	PhotonNetwork.CreateRoom(roomName, new RoomOptions() { maxPlayers = 2, isOpen = true, isVisible = true}, lobbyName);
			PhotonNetwork.CreateRoom(roomName, new RoomOptions() { maxPlayers = 2, isOpen = true, isVisible = true}, lobbyName);
			// Join Room
			if (roomsList != null)
			{
				for (int i = 0; i < roomsList.Length; i++)
				{
					if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join " + roomsList[i].name))
						PhotonNetwork.JoinRoom(roomsList[i].name);
				}
			}
		}
	}

	void OnConnectedToMaster()
	{
		PhotonNetwork.JoinLobby(lobbyName);
	}

	void OnReceivedRoomListUpdate()
	{
		roomsList = PhotonNetwork.GetRoomList();
	}

	void OnJoinedRoom()
	{
		PhotonNetwork.Instantiate(playerPrefab.name, Vector3.up * 5, Quaternion.identity, 0);
		Debug.Log("Connected to Room");
	}

	void OnJoinedLobby () 
	{
		Debug.Log ("Joined Lobby");
	}

}
