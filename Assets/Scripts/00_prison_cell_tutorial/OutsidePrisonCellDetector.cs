using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsidePrisonCellDetector : MonoBehaviour {


    public string PlayerColliderName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //todo check for right other
    private void OnTriggerEnter(Collider other)
    {

        if(PlayerColliderName == other.name)
        {
            Debug.Log("Player is outside of prison cell beacuse of" + other.name);
            LevelManagerPrisonCell00.Instance.IsOutside = true;
        }

    }

    
}
