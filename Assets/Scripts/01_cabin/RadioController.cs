using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VRTK;
using Random = UnityEngine.Random;

public class RadioController : MonoBehaviour {

    public bool IsBatteryConnected;
    public AudioSource Sound;


    private SnapToColliderController _batterySnapTo;

    [SerializeField]
    private GameObject _powerButton;
    private VRTK_InteractableObject _powerButtonInteractable;

    [SerializeField]
    private GameObject _changeChannelButtonsParent;

    private GameObject _activeChangeChannelButton;
    private GameObject _hiddenChannelButton;
    private Transform[] _channelsButtons;
    private int _hiddenChannelButtonIndex;
    private GameObject _stationText;
    private GameObject _onOffText;

    private AudioGeneratedCodePlayer _radioCodePlayer;
    private AudioClip _beep;
    private AudioClip[] _radioClips;

    [SerializeField]
    private float _buttonPressStep = 0.005f;
    private bool _isPowerButtonPressed;
    private bool _isRadioOn;
    private bool _changeButtonTimeNotYet;
    private bool _canChangeButton;

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
                    Sound.clip = _beep;
                    this.GetComponentInChildren<AudioGeneratedCodePlayer>().CanPlayCode(true);
                }
                else
                {
                    if(_activeChangeChannelButton != null)
                    {
                        Sound.clip = _radioClips[GetRadioButtonIndex(_activeChangeChannelButton)];
                        Sound.Play();
                    }

                }
                _onOffText.GetComponentInChildren<Text>().text = "On";
                if (_activeChangeChannelButton != null)
                    _stationText.GetComponentInChildren<Text>().text = "Station: " + _activeChangeChannelButton.ToString().Replace("radio_button_", "");
                else
                    _stationText.GetComponentInChildren<Text>().text = "";
            }
            else
            {
                _onOffText.GetComponentInChildren<Text>().text = "Off";
                _stationText.GetComponentInChildren<Text>().text = "";
                Sound.Stop();
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
        _stationText = GameObject.Find("stationText");
        _stationText.GetComponentInChildren<Text>().text = "";
        _onOffText = GameObject.Find("onOffText");
        _onOffText.GetComponentInChildren<Text>().text = "Off";

        _beep = (AudioClip) Resources.Load("Sounds/Radio/beep-trimmed", typeof(AudioClip));
        _radioClips = Resources.LoadAll<AudioClip>("Sounds/Radio");

        _batterySnapTo = GetComponentInChildren<SnapToColliderController>();
        _radioCodePlayer = GetComponentInChildren<AudioGeneratedCodePlayer>();
        _powerButtonInteractable = _powerButton.GetComponent<VRTK_InteractableObject>();
        _powerButtonInteractable.InteractableObjectUsed += OnPowerButtonUsed;
        _channelsButtons = _changeChannelButtonsParent.GetComponentsInChildren<Transform>();

        do
        {
            _hiddenChannelButtonIndex = Random.Range(0, 1000) % _channelsButtons.Length;
            _hiddenChannelButton = _channelsButtons[_hiddenChannelButtonIndex].gameObject;
        }
        while (_hiddenChannelButton == _changeChannelButtonsParent);

        Debug.Log("hidden channel button " + _hiddenChannelButton.name);
        foreach (Transform child in _channelsButtons)
        {
            if (child.name == _changeChannelButtonsParent.name) continue;
            var changeChannelButtonInteractable = child.gameObject.GetComponent<VRTK_InteractableObject>();
            changeChannelButtonInteractable.InteractableObjectUsed += OnChangeChannelButtonUsed;
        }
	}

    private int GetRadioButtonIndex(GameObject button)
    {
        if (button == null) return -1;
        var index = -1;

        for(int i = 0; i < _channelsButtons.Length; ++i)
        {
            if(button.name == _channelsButtons[i].name)
            {
                index = i;
                break;
            }
        }
        return index;
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
                _activeChangeChannelButton.transform.position += _activeChangeChannelButton.transform.up * 2 * _buttonPressStep;
            }
            button.transform.position -= button.transform.up * 2 * _buttonPressStep;
            _activeChangeChannelButton = button;

        }

        Debug.Log("Player pressed change channel button " + _activeChangeChannelButton);

        if (IsRadioOn)
        {
            var radioIndex = GetRadioButtonIndex(_activeChangeChannelButton);
            if (_hiddenChannelButton == _activeChangeChannelButton)
            {
                Sound.Stop();
                Sound.clip = _beep;
                Sound.loop = false;
                this.GetComponentInChildren<AudioGeneratedCodePlayer>().CanPlayCode(true);
            }
            else
            {


                Sound.Stop();
                this.GetComponentInChildren<AudioGeneratedCodePlayer>().CanPlayCode(false);
                if (radioIndex < 0)
                {

                    Sound.loop = false;
                    Sound.clip = null;
                }
                else
                {
                    Sound.Stop();
                    Sound.loop = true;
                    Sound.clip = _radioClips[radioIndex];
                    Sound.Play();
                }



            }
            if(radioIndex < 0)
            {
                _stationText.GetComponentInChildren<Text>().text = "";
            }
            else
            {
                _stationText.GetComponentInChildren<Text>().text = "Station: 0" + radioIndex;
            }
            
        }
        else
        {
            Sound.Stop();
            this.GetComponentInChildren<AudioGeneratedCodePlayer>().CanPlayCode(false);
        }

    }

    private void OnPowerButtonUsed(object sender, InteractableObjectEventArgs e)
    {
   
        IsPowerButtonPressed = !IsPowerButtonPressed;
        Debug.Log("Player pressed radio power button " + IsPowerButtonPressed);
    }


}
