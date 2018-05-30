using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class LevelManagerCabin01 : MonoBehaviour {


    public static LevelManagerCabin01 Instance;

    //events
    public UnityEvents.UnityEventGameObjectBool OnSafeCodeCheck;
    public UnityEvents.UnityEventGameObjectBool OnLockerDoorCheck;
    //flags
    public bool CanOpenSafe { get; set; }
    public string SafeCode { get; set; }


    private GameObject _keyToLocker;
    private GameObject _lockerWithBatteryDoor;

    public GameObject SafeDoor;

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
        if (OnSafeCodeCheck == null) OnSafeCodeCheck = new UnityEvents.UnityEventGameObjectBool();
        if (OnLockerDoorCheck == null) OnLockerDoorCheck = new UnityEvents.UnityEventGameObjectBool();
    }
    public void SetKeyDoorCombination(GameObject key, GameObject door)
    {
        _keyToLocker = key;
        _lockerWithBatteryDoor = door;

        //add openLock listener to correct locker
        OnLockerDoorCheck.AddListener(_lockerWithBatteryDoor.GetComponentInChildren<LockerController>().CanOpenDoor);

        var lockerSnapDropZone =_lockerWithBatteryDoor.GetComponentInChildren<VRTK_SnapDropZone>();
        lockerSnapDropZone.ObjectSnappedToDropZone += (sender, e) =>
        {
            if(e.snappedObject == _keyToLocker)
            {
                OnLockerDoorCheck.Invoke(door, true);
            }
            else
            {
                OnLockerDoorCheck.Invoke(door, false);
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
        //here there is no need to check for correct door
        OnSafeCodeCheck.Invoke(SafeDoor, correct);
    }

}
