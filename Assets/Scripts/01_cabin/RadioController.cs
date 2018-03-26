using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class RadioController : MonoBehaviour {

    public bool IsBatteryConnected;
    private SnapToColliderController _batterySnapTo;

    [SerializeField]
    private GameObject _powerButton;
    private VRTK_InteractableObject _powerButtonInteractable;

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
            if(_isRadioOn)
            {
                _radioCodePlayer.CanPlayCode(true);
            }
            else
            {
                _radioCodePlayer.CanPlayCode(false);
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
	}

    private void OnPowerButtonUsed(object sender, InteractableObjectEventArgs e)
    {

        IsPowerButtonPressed = !IsPowerButtonPressed;
        Debug.Log("Player pressed radio power button " + IsPowerButtonPressed);

    }

}
