using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour {
    public static ComputerManager Instance;


    public GameObject CaptchaScreen;
    public GameObject CodeScreen;
    public GameObject CameraScreen;

    public int computerStage;

	// Use this for initialization
	void Start () {
        computerStage = 1;
        UpdateScreen();
	}

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

    // Update is called once per frame
    void Update () {
		
	}

    public void NextStage()
    {
        computerStage++;
        UpdateScreen();
    }

    private void UpdateScreen()
    {
        switch(computerStage)
        {
            case 1:
                CaptchaScreen.SetActive(true);
                CodeScreen.SetActive(false);
                CameraScreen.SetActive(false);
                break;
            case 2:
                CaptchaScreen.SetActive(false);
                CodeScreen.SetActive(true);
                CameraScreen.SetActive(false);
                break;
            case 3:
                CaptchaScreen.SetActive(false);
                CodeScreen.SetActive(false);
                CameraScreen.SetActive(true);
                break;
        }
    }
}
