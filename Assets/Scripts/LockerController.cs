using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class LockerController : MonoBehaviour {


    [SerializeField]
    private float _doorMinLimitJoint = 0;
    [SerializeField]
    private float _doorMaxLimitJoint = 110;


    private bool _canOpenDoor;

    [SerializeField]
    private HingeJoint _doorJoint;

	// Use this for initialization
	void Start () {
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
        if(_canOpenDoor)
        {

            _doorJoint.limits = new JointLimits()
            {
                min = _doorMinLimitJoint,
                max = _doorMaxLimitJoint,
            };
        }
        else
        {
            _doorJoint.limits = new JointLimits()
            {
                min = 0,
                max = 0
            };
        }

        Debug.Log(this.name + "joint limits: min " + _doorJoint.limits.min + ", max " + _doorJoint.limits.max);
    }


}
