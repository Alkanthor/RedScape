using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRTK;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager Instance;

    [SerializeField]
    private GameObject SDKSetupObject;

    [SerializeField]
    private GameObject _leftController;
    [SerializeField]
    private GameObject _rightController;
    [SerializeField]
    private GameObject _playArea;

    private GameObject _playerCamera;
    private GameObject _playerArea;

    void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
        {
            //if not, set instance to this
            Instance = this;
            DontDestroyOnLoad(this);
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
        var manager = SDKSetupObject.GetComponent<VRTK_SDKManager>();
        manager.LoadedSetupChanged += OnLoadedSetupChanged;
        
	}

    private void OnLoadedSetupChanged(VRTK_SDKManager sender, VRTK_SDKManager.LoadedSetupChangeEventArgs e)
    {
    

        _playerCamera = GameObject.FindGameObjectWithTag(UnityStrings.TAG_MAIN_CAMERA);
        var sdkSetup = SDKSetupObject.GetComponentInChildren<VRTK_SDKSetup>();
        if (sdkSetup != null)
        {
            _playerArea = SDKSetupObject.GetComponentInChildren<VRTK_SDKSetup>().gameObject;
        }
        MainGameManager.Instance.PlayerManagerInitialized = true;

    }

    public void AdjustPlayerPosition(Vector3 position)
    {
        _playerArea.transform.position = position - _playerCamera.transform.position;
    }
   
}
