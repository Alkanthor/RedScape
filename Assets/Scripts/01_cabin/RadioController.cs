using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRTK;
public class RadioController : MonoBehaviour {

    public bool IsBatteryConnected;
    private SnapToColliderController _batterySnapTo;

    [SerializeField]
    private GameObject _powerButton;
    private VRTK_InteractableObject _powerButtonInteractable;

    [SerializeField]
    private GameObject _changeChannelButtonsParent;

    private GameObject _activeChangeChannelButton;
    private GameObject _hiddenChannelButton;

    private AudioGeneratedCodePlayer _radioCodePlayer;

    [SerializeField]
    private float _buttonPressStep = 0.005f;
    private bool _isPowerButtonPressed;
    private bool _isRadioOn;
    public bool IsRadioOn
    {
        get
        {
            return _isRadioOn;
        }
        set
        {
            _isRadioOn = value;
            if (_isRadioOn)
            {
                if (_hiddenChannelButton == _activeChangeChannelButton)
                {
                    this.GetComponentInChildren<AudioGeneratedCodePlayer>().CanPlayCode(true);
                }
            }
            else
            {
                this.GetComponentInChildren<AudioGeneratedCodePlayer>().CanPlayCode(false);
            }
        }
    }
    public bool IsPowerButtonPressed
    {
        get
        {
            return _isPowerButtonPressed;
        }
        set
        {
            _isPowerButtonPressed = value;
            if(_isPowerButtonPressed)
            {
                _powerButton.transform.position += transform.right * _buttonPressStep;
                //here the radio is really turned on
                if (_batterySnapTo.ObjectIsSnapped)
                {
                    Debug.Log("Radio is on");
                    IsRadioOn = true;
                }
            }
            else
            {
                _powerButton.transform.position -= transform.right * _buttonPressStep;
                Debug.Log("Radio is off");
                IsRadioOn = false;
            }
        }
    }

	// Use this for initialization
	void Start () {
        _batterySnapTo = GetComponentInChildren<SnapToColliderController>();
        _radioCodePlayer = GetComponentInChildren<AudioGeneratedCodePlayer>();
        _powerButtonInteractable = _powerButton.GetComponent<VRTK_InteractableObject>();
        _powerButtonInteractable.InteractableObjectUsed += OnPowerButtonUsed;
        var channelsButtons = _changeChannelButtonsParent.GetComponentsInChildren<Transform>();
        _hiddenChannelButton = channelsButtons[Random.Range(0, 1000) % channelsButtons.Length].gameObject;
        foreach (Transform child in channelsButtons)
        {
            if (child.name == _changeChannelButtonsParent.name) continue;
            var changeChannelButtonInteractable = child.gameObject.GetComponent<VRTK_InteractableObject>();
            changeChannelButtonInteractable.InteractableObjectUsed += OnChangeChannelButtonUsed;
        }
	}


    private void OnChangeChannelButtonUsed(object sender, InteractableObjectEventArgs e)
    {
        var button = (sender as VRTK_InteractableObject).gameObject;
        //is the same button
        if (_activeChangeChannelButton == button)
        {
            button.transform.position += button.transform.up * 2 * _buttonPressStep;
            _activeChangeChannelButton = null;
        }
        //is different button, we switch them
        else
        {

            if (_activeChangeChannelButton != null)
            {
                _activeChangeChannelButton.transform.position += _activeChangeChannelButton.transform.up * 2 *_buttonPressStep;
            }
            button.transform.position -= button.transform.up * 2 * _buttonPressStep;
            _activeChangeChannelButton = button;

        }

        if(_hiddenChannelButton == _activeChangeChannelButton)
        {
            if(IsRadioOn)
            {
                this.GetComponentInChildren<AudioGeneratedCodePlayer>().CanPlayCode(true);
            }
            else
            {
                this.GetComponentInChildren<AudioGeneratedCodePlayer>().CanPlayCode(false);
            }

        }
        else
        {
            this.GetComponentInChildren<AudioGeneratedCodePlayer>().CanPlayCode(false);
        }
        Debug.Log("Player pressed change channel button " + _activeChangeChannelButton);

    }

    private void OnPowerButtonUsed(object sender, InteractableObjectEventArgs e)
    {
   
        IsPowerButtonPressed = !IsPowerButtonPressed;
        Debug.Log("Player pressed radio power button " + IsPowerButtonPressed);

    }


}
