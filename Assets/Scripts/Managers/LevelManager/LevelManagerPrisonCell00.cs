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

    public bool GameStarted { get; set; }
    public bool TeleportAppear { get; set; }


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

    [Header("Control Level Events")]
    public UnityEvent TeleportAppearEvent;




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

	}


    private async Task WaitForTeleportAppear()
    {
        int waitTime = UnityEngine.Random.Range(3, 6);
        await Task.Delay(TimeSpan.FromSeconds(waitTime));
        TeleportAppearEvent.Invoke();
        TeleportAppear = true;
    }
    public void StartGame()
    {
        Debug.Log("game started");
        GameStarted = true;
        Task.Run(async () => await WaitForTeleportAppear());
    }
}
