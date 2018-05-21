using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
public class LockerController : MonoBehaviour
{

    public UnityEvents.UnityEventGameObjectBool OnIsDoorOpen;

    private AudioSource _sound;
    public AudioClip OpenSound;
    public AudioClip LockSound;

    [SerializeField]
    private float _doorMinLimitJoint = 0;
    [SerializeField]
    private float _doorMaxLimitJoint = 110;

    private bool _canOpenDoor;

    [SerializeField]
    private HingeJoint _doorJoint;

    // Use this for initialization
    void Start()
    {
        if (OnIsDoorOpen == null) OnIsDoorOpen = new UnityEvents.UnityEventGameObjectBool();
        _sound = this.GetComponent<AudioSource>();
        
        _canOpenDoor = false;
        _doorJoint.limits = new JointLimits()
        {
            min = 0,
            max = 0
        };

    }

    public void CanOpenDoor(GameObject door, bool canOpen)
    {
        if(door.name == this.gameObject.name)
        {
            //we already opened the safe
            if (_canOpenDoor == true) return;

            _canOpenDoor = canOpen;
            if (_canOpenDoor)
            {
                _sound.clip = OpenSound;
                _sound.Play();
                _doorJoint.limits = new JointLimits()
                {
                    min = _doorMinLimitJoint,
                    max = _doorMaxLimitJoint,
                };
            }
            else
            {
                _sound.clip = LockSound;
                _sound.Play();
                _doorJoint.limits = new JointLimits()
                {
                    min = 0,
                    max = 0
                };
            }

            OnIsDoorOpen.Invoke(door, canOpen);
            Debug.Log(this.name + "joint limits: min " + _doorJoint.limits.min + ", max " + _doorJoint.limits.max);
        }
        
    }


}
