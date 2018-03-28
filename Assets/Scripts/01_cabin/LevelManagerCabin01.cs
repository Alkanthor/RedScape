using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class LevelManagerCabin01 : MonoBehaviour {


    public static LevelManagerCabin01 Instance;

    //events
    public UnityEvents.UnityEventBool OnSafeCodeCheck;
    public UnityEvents.UnityEventBool OnLockerDoorCheck;
    //flags
    public bool CanOpenSafe { get; set; }
    public string SafeCode { get; set; }


    private GameObject _keyToLocker;
    private GameObject _lockerWithBatteryDoor;


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

    private void Start()
    {
        //check if it is initialized
        if (OnSafeCodeCheck == null) OnSafeCodeCheck = new UnityEvents.UnityEventBool();
        if (OnLockerDoorCheck == null) OnLockerDoorCheck = new UnityEvents.UnityEventBool();
    }
    public void SetKeyDoorCombination(GameObject key, GameObject door)
    {
        _keyToLocker = key;
        _lockerWithBatteryDoor = door;

        //add openLock listener to correct locker
        OnLockerDoorCheck.AddListener(_lockerWithBatteryDoor.GetComponent<LockerController>().CanOpenDoor);

        var lockerSnapDropZone =_lockerWithBatteryDoor.GetComponentInChildren<VRTK_SnapDropZone>();
        lockerSnapDropZone.ObjectSnappedToDropZone += (sender, e) =>
        {
            if(e.snappedObject == _keyToLocker)
            {
                OnLockerDoorCheck.Invoke(true);
            }
            else
            {
                OnLockerDoorCheck.Invoke(false);
            }
        };
        
    }
    public void CheckSafeCode(string code)
    {
        var correct = false;
        if(SafeCode == code)
        {
            correct = true;
        }
        Debug.Log(this.name + ": OnSafeCodeCheck " + correct);
        OnSafeCodeCheck.Invoke(correct);
    }

}
