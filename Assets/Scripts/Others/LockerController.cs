using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
public class LockerController : MonoBehaviour
{
    public AudioSource Sound;

    [SerializeField]
    private float _doorMinLimitJoint = 0;
    [SerializeField]
    private float _doorMaxLimitJoint = 110;

    public GameObject _lockerText;


    private bool _canOpenDoor;

    [SerializeField]
    private HingeJoint _doorJoint;

    // Use this for initialization
    void Start()
    {
        Sound = this.GetComponent<AudioSource>();
        Sound.clip = (AudioClip)Resources.Load("Sounds/Radio/lock", typeof(AudioClip));
        _canOpenDoor = false;
        _doorJoint.limits = new JointLimits()
        {
            min = 0,
            max = 0
        };

    }

    public void CanOpenDoor(bool canOpen)
    {

        //we already opened the safe
        if (_canOpenDoor == true) return;

        _canOpenDoor = canOpen;
        if (_canOpenDoor)
        {
            Sound.Play();
            _lockerText.GetComponentInChildren<Text>().text = "Open";
            _lockerText.GetComponentInChildren<Text>().color = Color.green;
            _doorJoint.limits = new JointLimits()
            {
                min = _doorMinLimitJoint,
                max = _doorMaxLimitJoint,
            };
        }
        else
        {
            _lockerText.GetComponentInChildren<Text>().text = "Locked";
            _lockerText.GetComponentInChildren<Text>().color = Color.red;
            _doorJoint.limits = new JointLimits()
            {
                min = 0,
                max = 0
            };
        }

        Debug.Log(this.name + "joint limits: min " + _doorJoint.limits.min + ", max " + _doorJoint.limits.max);
    }


}
