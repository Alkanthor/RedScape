using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerCabin01 : MonoBehaviour {


    public static LevelManagerCabin01 Instance;

    //flags
    public bool CanOpenSafe { get; set; }



    public string SafeCode { get; set; }

    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
        {
            //if not, set instance to this
            Instance = this;
        }
        //If instance already exists and it's not this:
        else if (Instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

    }

    public void SetSafeCode(string code)
    {
        SafeCode = code;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
