using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsidePrisonCellDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //todo check for right other
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player is outside of prison cell beacuse of" + other.name);
        LevelManagerPrisonCell00.Instance.IsOutside = true;
    }

    
}
