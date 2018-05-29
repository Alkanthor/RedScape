using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using VRTK;


public class PlayerManager : MonoBehaviour {

    public static PlayerManager Instance;

    
    public UnityEvents.UnityEventObjectControllerInteractionEventArgs OnTriggerPressed;
    public UnityEvents.UnityEventObjectControllerInteractionEventArgs OnTriggerReleased;
    public UnityEvents.UnityEventObjectControllerInteractionEventArgs OnTriggerAxisChanged;

    public UnityEvents.UnityEventObjectControllerInteractionEventArgs OnTouchpadPressed;
    public UnityEvents.UnityEventObjectControllerInteractionEventArgs OnTouchpadReleased;
    public UnityEvents.UnityEventObjectControllerInteractionEventArgs OnTouchpadAxisChanged;
    public UnityEvents.UnityEventObjectControllerInteractionEventArgs OnTouchpadTouchStart;
    public UnityEvents.UnityEventObjectControllerInteractionEventArgs OnTouchpadTouchEnd;

    public UnityEvents.UnityEventObjectControllerInteractionEventArgs OnButtonOnePressed;
    public UnityEvents.UnityEventObjectControllerInteractionEventArgs OnButtonTwoPressed;

    [SerializeField]
    private GameObject SDKSetupObject;

    [SerializeField]
    private GameObject _leftControllerScripts;
    [SerializeField]
    private GameObject _rightControllerScripts;
    [SerializeField]
    private GameObject _playAreaScripts;

    private GameObject _playerCamera;
    private GameObject _playerArea;

    private const int LEFT_CONTROLLER_ID = 1;
    private const int RIGHT_CONTROLLER_ID = 2;

    private UnityEnums.ControllerType _leftControllerTypePrev = UnityEnums.ControllerType.NOT_INITIALIZED;
    private UnityEnums.ControllerType _leftControllerType = UnityEnums.ControllerType.NOT_INITIALIZED;
    public UnityEnums.ControllerType LeftControllerType
    {
        get { return _leftControllerType; }
        set
        {
            _leftControllerTypePrev = _leftControllerType;
            _leftControllerType = value;
            ChangeControllerSetup(LEFT_CONTROLLER_ID);
        }
    }

    private UnityEnums.ControllerType _rightControllerTypePrev = UnityEnums.ControllerType.NOT_INITIALIZED;
    private UnityEnums.ControllerType _rightControllerType = UnityEnums.ControllerType.NOT_INITIALIZED;
    public UnityEnums.ControllerType RightControllerType
    {
        get { return _rightControllerType; }
        set
        {
            _rightControllerTypePrev = _rightControllerType;
            _rightControllerType = value;
            ChangeControllerSetup(RIGHT_CONTROLLER_ID);
        }
    }

    private bool _isManagerInitialized = false;

    private VRTK_Pointer _leftControllerPointer;
    private VRTK_Pointer _rightControllerPointer;
    private VRTK_BasePointerRenderer _leftControllerPointerRenderer;
    private VRTK_BasePointerRenderer _rightControllerPointerRenderer;
    private VRTK_ControllerEvents _leftControllerEvents;
    private VRTK_ControllerEvents _rightControllerEvents;

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

        //init unity events if null
        if (OnTriggerPressed == null) OnTriggerPressed = new UnityEvents.UnityEventObjectControllerInteractionEventArgs();


        var manager = SDKSetupObject.GetComponent<VRTK_SDKManager>();

        //get controllers
        _leftControllerPointer = _leftControllerScripts.GetComponent<VRTK_Pointer>();
        _rightControllerPointer = _rightControllerScripts.GetComponent<VRTK_Pointer>();
        _leftControllerPointerRenderer = _leftControllerScripts.GetComponent<VRTK_BasePointerRenderer>();
        _rightControllerPointerRenderer = _rightControllerScripts.GetComponent<VRTK_BasePointerRenderer>();
        _leftControllerEvents = _leftControllerScripts.GetComponent<VRTK_ControllerEvents>();
        _rightControllerEvents = _rightControllerScripts.GetComponent<VRTK_ControllerEvents>();

        //TODO: subscribe to events
        manager.LoadedSetupChanged += OnLoadedSetupChanged;
        
	}


    private void OnLoadedSetupChanged(VRTK_SDKManager sender, VRTK_SDKManager.LoadedSetupChangeEventArgs e)
    {
    

        _playerCamera = GameObject.FindGameObjectWithTag(UnityStrings.TAG_MAIN_CAMERA);
        var sdkSetup = SDKSetupObject.GetComponentInChildren<VRTK_SDKSetup>();
        if (sdkSetup != null)
        {
            _playerArea = SDKSetupObject.GetComponentInChildren<VRTK_SDKSetup>().gameObject;

            MainGameManager.Instance.PlayerManagerInitialized = true;
        }


    }

    private void ChangeControllerSetup(int controllerId)
    {
       if(controllerId == LEFT_CONTROLLER_ID)
        {
            switch(LeftControllerType)
            {
                case UnityEnums.ControllerType.NO_TELEPORT:
                    EnableTeleport(_leftControllerPointer, _leftControllerPointerRenderer, false, false);
                    break;
                case UnityEnums.ControllerType.DISABLED_TELEPORT:
                    EnableTeleport(_leftControllerPointer, _leftControllerPointerRenderer, false, false);
                    break;
                case UnityEnums.ControllerType.NORMAL:
                    EnableTeleport(_leftControllerPointer, _leftControllerPointerRenderer, true, true);              
                    break;
                case UnityEnums.ControllerType.CAR_CONTROLLER:
                    EnableTeleport(_leftControllerPointer, _leftControllerPointerRenderer, false, false);
                    break;
                case UnityEnums.ControllerType.NOT_INITIALIZED:
                    break;
            }
        }
       else if(controllerId == RIGHT_CONTROLLER_ID)
        {
            switch (LeftControllerType)
            {
                case UnityEnums.ControllerType.NO_TELEPORT:
                    EnableTeleport(_rightControllerPointer, _rightControllerPointerRenderer, false, false);
                    break;
                case UnityEnums.ControllerType.DISABLED_TELEPORT:
                    EnableTeleport(_rightControllerPointer, _rightControllerPointerRenderer, false, false);
                    break;
                case UnityEnums.ControllerType.NORMAL:
                    EnableTeleport(_rightControllerPointer, _rightControllerPointerRenderer, true, true);
                    break;
                case UnityEnums.ControllerType.CAR_CONTROLLER:
                    EnableTeleport(_rightControllerPointer, _rightControllerPointerRenderer, false, false);
                    break;
                case UnityEnums.ControllerType.NOT_INITIALIZED:
                    break;
            }
        }
    }

    private void EnableTeleport(VRTK_Pointer controllerPointer, VRTK_BasePointerRenderer controllerRenderer, bool enableTeleport, bool enableTeleportRenderer)
    {
        controllerPointer.enableTeleport = enableTeleport;
        controllerRenderer.enabled = enableTeleportRenderer;

    }
    public void AdjustPlayerPosition(Vector3 position)
    {
        _playerArea.transform.position = position - _playerCamera.transform.position;
    }
   
}
