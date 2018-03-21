using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TeleportMalfunctionScript : MonoBehaviour {

    private VRTK_HeightAdjustTeleport VRTKTeleportScript;

    private bool playerTeleportedOuside = false;

    // Use this for initialization
    void Start () {
        VRTKTeleportScript = GameObject.Find("PlayArea").GetComponent<VRTK_HeightAdjustTeleport>();
        VRTKTeleportScript.Teleported += PlayerTeleported;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerTeleported(object sender, DestinationMarkerEventArgs e)
    {
        Debug.Log("PlayerTeleported");
        if(playerTeleportedOuside)
        {
            Application.LoadLevel(1);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //if (col.gameObject.name == "Camera (eye)")
        {
            Debug.Log("Player in hall");
        }
    }
}
