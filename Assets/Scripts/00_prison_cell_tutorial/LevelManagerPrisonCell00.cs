using System.Collections;
using System.Collections.Generic;
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
                EnableTeleport(true, true);
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
                EnableTeleport(false, true);
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
                SceneManager.LoadScene(2, LoadSceneMode.Single);
            }

        }
    }
    private int _usedTeleportOutside = 0;

    [Header("Control Level Events")]
    public UnityEvent TeleportAppearEvent;


    [SerializeField]
    private VRTK_Pointer LeftControllerPointer;
    [SerializeField]
    private VRTK_Pointer RightControllerPointer;
    [SerializeField]
    private VRTK_BasePointerRenderer LeftControllerPointerRenderer;
    [SerializeField]
    private VRTK_BasePointerRenderer RightControllerPointerRenderer;

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

        EnableTeleport(false, false);
        LeftControllerPointer.ActivationButtonReleased += OnTeleportUsed;
        RightControllerPointer.ActivationButtonReleased += OnTeleportUsed;
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTeleportUsed(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("Player used teleport");
        if(IsOutside)
        {
            
            UsedTeleportTwice = true;

        }
    }

    void EnableTeleport(bool enableTeleport, bool enableTeleportRenderer)
    {
        LeftControllerPointer.enableTeleport = enableTeleport;
        RightControllerPointer.enableTeleport = enableTeleport;
        LeftControllerPointerRenderer.enabled = enableTeleportRenderer;
        RightControllerPointerRenderer.enabled = enableTeleportRenderer;
    }
    IEnumerator WaitForTeleportAppear()
    {
        int waitTime = Random.Range(3, 6);
        yield return new WaitForSeconds(waitTime);
        TeleportAppearEvent.Invoke();
        TeleportAppear = true;
    }
    public void StartGame()
    {
        Debug.Log("game started");
        GameStarted = true;
        StartCoroutine(WaitForTeleportAppear());
    }
}
