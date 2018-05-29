using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using VRTK;
public class LevelManagerPrisonCell00 : MonoBehaviour {


    public static LevelManagerPrisonCell00 Instance;

    private bool _gameStarted;
    public bool GameStarted
    {
        get { return _gameStarted; }
        set
        {

            if (value == true && !_gameStarted)
            {
                StartGame();
            }
            _gameStarted = value;
        }
    }
    private bool _teleportAppear;
    public bool TeleportAppear
    {
        get
        {
            return _teleportAppear;
        }
        set
        {

            _teleportAppear = value;
           
        }
    }
    public GameObject Teleport;

    private bool _grabbedTeleport;
    public bool GrabbedTeleport
    {
        get
        {
            return _grabbedTeleport;
        }
        set
        {
            
            if(!_grabbedTeleport && value)
            {
                PlayerManager.Instance.LeftControllerType = UnityEnums.ControllerType.NORMAL;
                PlayerManager.Instance.RightControllerType = UnityEnums.ControllerType.NORMAL;

            }
            _grabbedTeleport = value;
        }
    }
    private bool _isOutside;
    public bool IsOutside
    {
        get
        {
            return _isOutside;
        }
        set
        {

            _isOutside = value;
            if(_isOutside)
            {
                Debug.Log("Is Outside Of Cell");
                PlayerManager.Instance.LeftControllerType = UnityEnums.ControllerType.DISABLED_TELEPORT;
                PlayerManager.Instance.RightControllerType = UnityEnums.ControllerType.DISABLED_TELEPORT;
            }


        }
    }
    private bool _usedTeleportTwice;
    public bool UsedTeleportTwice
    {
        get
        {
            return _usedTeleportTwice;
        }
        set
        {
            _usedTeleportTwice = value;
            if(IsOutside && UsedTeleportTwice)
            {
                Debug.Log("TODO: END LEVEL...CHANGE SCENE");
                GameSceneManager.Instance.LoadNextScene(true);
            }

        }
    }
    private int _usedTeleportOutside = 0;

    //Awake is always called before any Start functions
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

    // Use this for initialization
    void Start () {
        GameStarted = false;
        TeleportAppear = false;
        GrabbedTeleport = false;
        IsOutside = false;
        UsedTeleportTwice = false;

        PlayerManager.Instance.LeftControllerType = UnityEnums.ControllerType.NO_TELEPORT;
        PlayerManager.Instance.RightControllerType = UnityEnums.ControllerType.NO_TELEPORT;
    }
	
	// Update is called once per frame
	void Update () {
        //set teleport in main thread
        if(TeleportAppear && Teleport != null)
        {
            Teleport.SetActive(true);
        }
	}


    private async Task WaitForTeleportAppear()
    {
        var random = new System.Random();
        int waitTime = random.Next(3, 6);
        await Task.Delay(TimeSpan.FromSeconds(waitTime));
        TeleportAppear = true;
    }
    public void StartGame()
    {
        Debug.Log("game started");
        Task.Run(async () => await WaitForTeleportAppear());
    }
}
