﻿using UnityEngine;
using System.Collections;

//Access the DarkRift namespace
using DarkRift;

public class NetworkManager : MonoBehaviour
{

	/// <summary>
	/// 	The IP to connect to.
	/// </summary>
	public string serverIP = "127.0.0.1";

	/// <summary>
	/// 	The port to conenct to.
	/// </summary>
	public int serverPort = 4296;

	//The player that we will instantiate when someone joins.
	public GameObject playerObject;

	//A reference to our player
	Transform player;

	void Start ()
	{
		//Enable Background Running
		//Application.runInBackground = true;
		INIParser ini = new INIParser();
		// Open the save file. If the save file does not exist, INIParser automatically create
		// one
		ini.Open(Application.dataPath + "/MayaVerseLowPoly.ini");
		serverIP = ini.ReadValue("NetworkConfig","ServerIP","127.0.0.1");
		Debug.Log("ServerIP: " + serverIP);
		serverPort = ini.ReadValue("NetworkConfig","ServerPort",4296);
		Debug.Log ("ServerPort: " + serverPort.ToString());
		//Close file
		//Close file
		ini.Close();

		//Connect to the DarkRift Server using the Ip specified (will hang until connected or timeout)
		DarkRiftAPI.Connect (serverIP, serverPort);

		//Tell others that we've entered the game and to instantiate a player object for us.
		if (DarkRiftAPI.isConnected)
		{
			Debug.Log("Connected to the Server!");
		}
		else
		{
			Debug.LogError ("Failed to connect to DarkRift Server!");
		}
	}

	void OnApplicationQuit ()
	{
		//You will want this here otherwise the server wont notice until someone else sends data to this
		//client.
		DarkRiftAPI.Disconnect ();
	}
}
