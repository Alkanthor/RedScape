using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class WhereIsMyBatteryController : MonoBehaviour {


    public UnityEvents.UnityEventGameObject2 OnKeyDoorCombination;
    public UnityEvents.UnityEventBool OnLockerDoorCheck;
    [SerializeField]
    private GameObject _batteryPrefab;
    [SerializeField]
    private GameObject _keyPrefab;
    [SerializeField]
    private GameObject _keyDropArea;
    [SerializeField] 
    private GameObject[] _lockerDoors;
    [SerializeField]
    private string _dropZoneTag;
    // Use this for initialization
    private GameObject _battery;
    private GameObject _key;
    private GameObject _doorWithBattery;
    void Start () {

        if (OnKeyDoorCombination == null) OnKeyDoorCombination = new UnityEvents.UnityEventGameObject2();
        if (OnLockerDoorCheck == null) OnLockerDoorCheck = new UnityEvents.UnityEventBool();
        CreateKey();
        CreateBattery();
        //send key-door combination
        OnKeyDoorCombination.Invoke(_key, _doorWithBattery);
    }
	
    private void CreateKey()
    {
        _key = CreateObjectInDropZone(_keyPrefab, _keyDropArea);

    }

    private void CreateBattery()
    {
        
        var randomDoorIndex = Random.Range(0, 1000) % _lockerDoors.Length;
        _doorWithBattery = _lockerDoors[randomDoorIndex];
        for (int i = 0; i< _lockerDoors.Length; ++i)
        {
            if(_doorWithBattery != _lockerDoors[i])
            {
                var lockerSnapDropZone = _lockerDoors[i].GetComponentInChildren<VRTK_SnapDropZone>();
                OnLockerDoorCheck.AddListener(_lockerDoors[i].GetComponentInChildren<LockerController>().CanOpenDoor);
                lockerSnapDropZone.ObjectSnappedToDropZone += (sender, e) =>
                {
                    OnLockerDoorCheck.Invoke(false);
                };
            }
        }
            Debug.Log("door to key: " + _doorWithBattery.name);
        GameObject dropZone = null;
        foreach(Transform child in _doorWithBattery.GetComponentsInChildren<Transform>())
        {
            if(child.tag == _dropZoneTag)
            {
                dropZone = child.gameObject;
            }
        }
        if(dropZone != null)
        {
            _battery = CreateObjectInDropZone(_batteryPrefab, dropZone, false);
        }

    }

    private GameObject CreateObjectInDropZone(GameObject prefab, GameObject dropZone, bool rotate = true)
    {

        var objectColliders = dropZone.GetComponents<Collider>();
        var randomColliderIndex = Random.Range(0, 1000) % objectColliders.Length;
        var objectCenter = objectColliders[randomColliderIndex].bounds.center;
        var objectExtends = objectColliders[randomColliderIndex].bounds.extents;
        var randomPosInArea = new Vector3(Random.Range(objectCenter.x, objectCenter.x + objectExtends.x), Random.Range(objectCenter.y, objectCenter.y + objectExtends.y), Random.Range(objectCenter.z, objectCenter.z + objectExtends.z));

        var objectRandomRotation = prefab.transform.rotation;
        if(rotate)
        {
            objectRandomRotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
        }
        var objectSpawn = GameObject.Instantiate(prefab, randomPosInArea, objectRandomRotation);
        if(objectSpawn == null)
        {
            Debug.LogWarning("object is NULL!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        Debug.Log("object generated " + objectSpawn);
        return objectSpawn;
    }
	// Update is called once per frame
	void Update () {
		
	}
}
