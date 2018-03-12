using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibratingRoomTeleport : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Teleporter collided with player collisiton enter");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Teleporter collided with player trigger enter");
    }
    public void TeleportChange()
    {
        Debug.Log("Teleporter collided with player");
    }
}
