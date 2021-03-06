﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using VRTK;

public class PlayerManager : MonoBehaviour {

    public static PlayerManager Instance;

    [HideInInspector]
    public UnityEvents.UnityEventObjectControllerInteractionEventArgs OnCarReset;
    [HideInInspector]
    public UnityEvents.UnityEventObjectControllerInteractionEventArgs OnTeleportUsed;
    [HideInInspector]
    public UnityEvents.UnityEventObjectControllerInteractionEventArgs OnCarMove;
    [HideInInspector]
    public UnityEvents.UnityEventObjectControllerInteractionEventArgs OnCarStop;
    [HideInInspector]
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
    private VRTK_ControllerTooltips _leftControllerTooltips;
    private VRTK_ControllerTooltips _rightControllerTooltips;
    private TeleportBall _leftControllerTeleportBall;
    private TeleportBall _rightControllerTeleportBall;
    private bool _rightToggleTooltips = false;
    private bool _leftToggleTooltips = false;
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
        if (OnTeleportUsed == null) OnTeleportUsed = new UnityEvents.UnityEventObjectControllerInteractionEventArgs();
        if (OnCarMove == null) OnCarMove = new UnityEvents.UnityEventObjectControllerInteractionEventArgs();
        if (OnCarStop == null) OnCarStop = new UnityEvents.UnityEventObjectControllerInteractionEventArgs();
        if (OnCarReset == null) OnCarReset = new UnityEvents.UnityEventObjectControllerInteractionEventArgs();
    }

    // Use this for initialization
    void Start () {




        var manager = SDKSetupObject.GetComponent<VRTK_SDKManager>();

        //get controllers
        _leftControllerPointer = _leftControllerScripts.GetComponent<VRTK_Pointer>();
        _rightControllerPointer = _rightControllerScripts.GetComponent<VRTK_Pointer>();
        _leftControllerPointerRenderer = _leftControllerScripts.GetComponent<VRTK_BasePointerRenderer>();
        _rightControllerPointerRenderer = _rightControllerScripts.GetComponent<VRTK_BasePointerRenderer>();
        _leftControllerEvents = _leftControllerScripts.GetComponent<VRTK_ControllerEvents>();
        _rightControllerEvents = _rightControllerScripts.GetComponent<VRTK_ControllerEvents>();
        _leftControllerTooltips = _leftControllerScripts.GetComponentInChildren<VRTK_ControllerTooltips>();
        _rightControllerTooltips = _rightControllerScripts.GetComponentInChildren<VRTK_ControllerTooltips>();
        _leftControllerTeleportBall = _leftControllerScripts.GetComponentInChildren<TeleportBall>();
        _rightControllerTeleportBall = _rightControllerScripts.GetComponentInChildren<TeleportBall>();
        //TODO: subscribe to events
        manager.LoadedSetupChanged += OnLoadedSetupChanged;
        _leftControllerEvents.ButtonTwoPressed += OnButtonTwoPressedEvent;
        _rightControllerEvents.ButtonTwoPressed += OnButtonTwoPressedEvent;
        _leftControllerEvents.TouchpadReleased += OnTouchpadReleasedEvent;
        _rightControllerEvents.TouchpadReleased += OnTouchpadReleasedEvent;
        _leftControllerEvents.TouchpadAxisChanged += OnTouchpadAxisChangedEvent;
        _rightControllerEvents.TouchpadAxisChanged += OnTouchpadAxisChangedEvent;
        _leftControllerEvents.TouchpadTouchEnd += OnTouchpadTouchEndEvent;
        _rightControllerEvents.TouchpadTouchEnd += OnTouchpadTouchEndEvent;
        _leftControllerEvents.TriggerPressed += OnTriggerPressedEvent;
        _rightControllerEvents.TriggerPressed += OnTriggerPressedEvent;
        //initialize unityevents
        ToggleTooltips(false);

    }

    private void OnTriggerPressedEvent(object sender, ControllerInteractionEventArgs e)
    {
        var controllerEvents = sender as VRTK_ControllerEvents;

        if (controllerEvents.gameObject == _leftControllerEvents.gameObject)
        {
            if (_leftControllerType == UnityEnums.ControllerType.CAR_CONTROLLER)
            {
                OnCarReset.Invoke(sender, e);
            }
        }
        else if (controllerEvents.gameObject == _rightControllerEvents.gameObject)
        {
            if (_rightControllerType == UnityEnums.ControllerType.CAR_CONTROLLER)
            {
                OnCarReset.Invoke(sender, e);
            }
        }
    }

    private void OnTouchpadTouchEndEvent(object sender, ControllerInteractionEventArgs e)
    {
        var controllerEvents = sender as VRTK_ControllerEvents;

        if (controllerEvents.gameObject == _leftControllerEvents.gameObject)
        {
            if (_leftControllerType == UnityEnums.ControllerType.CAR_CONTROLLER)
            {
                OnCarStop.Invoke(sender, e);
            }
        }
        else if (controllerEvents.gameObject == _rightControllerEvents.gameObject)
        {
            if (_rightControllerType == UnityEnums.ControllerType.CAR_CONTROLLER)
            {
                OnCarStop.Invoke(sender, e);
            }
        }
    }

    private void OnTouchpadAxisChangedEvent(object sender, ControllerInteractionEventArgs e)
    {
        var controllerEvents = sender as VRTK_ControllerEvents;

        if (controllerEvents.gameObject == _leftControllerEvents.gameObject)
        {
            if (_leftControllerType == UnityEnums.ControllerType.CAR_CONTROLLER)
            {
                OnCarMove.Invoke(sender, e);
            }
        }
        else if (controllerEvents.gameObject == _rightControllerEvents.gameObject)
        {
            if (_rightControllerType == UnityEnums.ControllerType.CAR_CONTROLLER)
            {
                OnCarMove.Invoke(sender, e);
            }
        }
    }
    private void OnTouchpadReleasedEvent(object sender, ControllerInteractionEventArgs e)
    {
        var controllerEvents = sender as VRTK_ControllerEvents;

        if (controllerEvents.gameObject == _leftControllerEvents.gameObject)
        {
            if(_leftControllerType == UnityEnums.ControllerType.NORMAL || _leftControllerType == UnityEnums.ControllerType.DISABLED_TELEPORT)
            {
                OnTeleportUsed.Invoke(sender, e);
            }
        }
        else if (controllerEvents.gameObject == _rightControllerEvents.gameObject)
        {
            if (_rightControllerType == UnityEnums.ControllerType.NORMAL || _rightControllerType == UnityEnums.ControllerType.DISABLED_TELEPORT)
            {
                OnTeleportUsed.Invoke(sender, e);
            }
        }
    }

    private void OnButtonTwoPressedEvent(object sender, ControllerInteractionEventArgs e)
    {
        var controllerEvents = sender as VRTK_ControllerEvents;

       if(controllerEvents.gameObject == _leftControllerEvents.gameObject)
        {
            _leftToggleTooltips = !_leftToggleTooltips;
            ToggleTooltips(LEFT_CONTROLLER_ID, _leftToggleTooltips);
        } else if (controllerEvents.gameObject == _rightControllerEvents.gameObject)
        {
            _rightToggleTooltips = !_rightToggleTooltips;
            ToggleTooltips(RIGHT_CONTROLLER_ID, _rightToggleTooltips);
        }
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
            if(_leftToggleTooltips)
            {
                ToggleTooltips(LEFT_CONTROLLER_ID, false);
            }
            switch(LeftControllerType)
            {
                case UnityEnums.ControllerType.NO_TELEPORT:
                    ChangeTooltips(controllerId);
                    _leftControllerTeleportBall.gameObject.SetActive(false);
                    EnableTeleport(_leftControllerPointer, _leftControllerPointerRenderer, false, false);
                    break;
                case UnityEnums.ControllerType.DISABLED_TELEPORT:
                    ChangeTooltips(controllerId);
                    _leftControllerTeleportBall.gameObject.SetActive(true);
                    EnableTeleport(_leftControllerPointer, _leftControllerPointerRenderer, false, true);
                    break;
                case UnityEnums.ControllerType.NORMAL:
                    ChangeTooltips(controllerId);
                    _leftControllerTeleportBall.gameObject.SetActive(true);
                    EnableTeleport(_leftControllerPointer, _leftControllerPointerRenderer, true, true);              
                    break;
                case UnityEnums.ControllerType.CAR_CONTROLLER:
                    ChangeTooltips(controllerId);
                    _leftControllerTeleportBall.gameObject.SetActive(false);
                    EnableTeleport(_leftControllerPointer, _leftControllerPointerRenderer, false, false);            
                    break;
                case UnityEnums.ControllerType.NOT_INITIALIZED:
                    break;
            }
            if (_leftToggleTooltips)
            {
                ToggleTooltips(LEFT_CONTROLLER_ID, true);
            }
        }
       else if(controllerId == RIGHT_CONTROLLER_ID)
        {
            if (_rightToggleTooltips)
            {
                ToggleTooltips(RIGHT_CONTROLLER_ID, false);
            }
            switch (LeftControllerType)
            {
                case UnityEnums.ControllerType.NO_TELEPORT:
                    ChangeTooltips(controllerId);
                    _rightControllerTeleportBall.gameObject.SetActive(false);
                    EnableTeleport(_rightControllerPointer, _rightControllerPointerRenderer, false, false);
                    break;
                case UnityEnums.ControllerType.DISABLED_TELEPORT:
                    ChangeTooltips(controllerId);
                    _rightControllerTeleportBall.gameObject.SetActive(true);
                    EnableTeleport(_rightControllerPointer, _rightControllerPointerRenderer, false, true);
                    break;
                case UnityEnums.ControllerType.NORMAL:
                    ChangeTooltips(controllerId);
                    _rightControllerTeleportBall.gameObject.SetActive(true);
                    EnableTeleport(_rightControllerPointer, _rightControllerPointerRenderer, true, true);
                    break;
                case UnityEnums.ControllerType.CAR_CONTROLLER:
                    ChangeTooltips(controllerId);
                    _rightControllerTeleportBall.gameObject.SetActive(false);
                    EnableTeleport(_rightControllerPointer, _rightControllerPointerRenderer, false, false);
                    break;
                case UnityEnums.ControllerType.NOT_INITIALIZED:
                    break;
            }
            if (_rightToggleTooltips)
            {
                ToggleTooltips(RIGHT_CONTROLLER_ID, true);
            }
        }
    }

    public void ToggleTooltips(bool toggle)
    {
        _leftToggleTooltips = toggle;
        _rightToggleTooltips = toggle;
        ToggleTooltips(LEFT_CONTROLLER_ID, toggle);
        ToggleTooltips(RIGHT_CONTROLLER_ID, toggle);
    }
    private void ToggleTooltips(int controllerId, bool toggle)
    {
        if (controllerId == LEFT_CONTROLLER_ID)
        {
            _leftControllerTooltips.gameObject.SetActive(toggle);
        }
        else if (controllerId == RIGHT_CONTROLLER_ID)
        {
            _rightControllerTooltips.gameObject.SetActive(toggle);
        }
    }
    private void ChangeTooltips(int controllerId)
    {
        bool toggle = false;
        VRTK_ControllerTooltips tooltips = null;
        UnityEnums.ControllerType type = UnityEnums.ControllerType.NOT_INITIALIZED;
        if (controllerId == LEFT_CONTROLLER_ID)
        {
            tooltips = _leftControllerTooltips;
            type = LeftControllerType;
            toggle = _leftToggleTooltips;
        }
        else if (controllerId == RIGHT_CONTROLLER_ID)
        {
            tooltips = _rightControllerTooltips;
            type = RightControllerType;
            toggle = _rightToggleTooltips;
        }

        if(toggle)
        {
            ToggleTooltips(controllerId, false);
        }
        switch (type)
        {
            case UnityEnums.ControllerType.NO_TELEPORT:
                tooltips.gripText = UnityStrings.TOOLTIP_GENERAL_GRIP;
                tooltips.triggerText = UnityStrings.TOOLTIP_GENERAL_TRIGGER;
                tooltips.touchpadText = "";
                tooltips.buttonTwoText = UnityStrings.TOOLTOP_GENERAL_MENU_BUTTON;
                break;
            case UnityEnums.ControllerType.DISABLED_TELEPORT:
            case UnityEnums.ControllerType.NORMAL:
                tooltips.gripText = UnityStrings.TOOLTIP_GENERAL_GRIP;
                tooltips.triggerText = UnityStrings.TOOLTIP_GENERAL_TRIGGER;
                tooltips.touchpadText = UnityStrings.TOOLTIP_GENERAL_TOUCHPAD;
                tooltips.buttonTwoText = UnityStrings.TOOLTOP_GENERAL_MENU_BUTTON;
                break;
            case UnityEnums.ControllerType.CAR_CONTROLLER:
                tooltips.gripText = UnityStrings.TOOLTIP_CAR_GRIP;
                tooltips.triggerText = UnityStrings.TOOLTIP_CAR_TRIGGER;
                tooltips.touchpadText = UnityStrings.TOOLTIP_CAR_TOUCHPAD;
                tooltips.buttonTwoText = UnityStrings.TOOLTOP_GENERAL_MENU_BUTTON;
                break;
            case UnityEnums.ControllerType.NOT_INITIALIZED:
                break;
        }
        if(toggle)
        {
            ToggleTooltips(controllerId, true);
        }
    }
    private void EnableTeleport(VRTK_Pointer controllerPointer, VRTK_BasePointerRenderer controllerRenderer, bool enableTeleport, bool enableTeleportRenderer)
    {
        controllerPointer.enableTeleport = enableTeleport;
        controllerRenderer.enabled = enableTeleportRenderer;

    }

    public void AdjustPlayerPosition(Vector3 position)
    {
        var camOffset = (_playerCamera.transform.position - _playerArea.transform.position);
        _playerArea.transform.position = (position - camOffset);
    }

    public Vector3 GetPlayerPosition()
    {
        return _playerCamera.transform.position;
    }

}
