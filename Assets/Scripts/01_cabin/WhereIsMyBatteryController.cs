using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhereIsMyBatteryController : MonoBehaviour {


    public UnityEvents.UnityEventGameObject2 OnKeyDoorCombination;
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
        CreateKey();
        CreateBattery();
        //send key-door combination
        OnKeyDoorCombination.Invoke(_key, _doorWithBattery);
    }
	
    private void CreateKey()
    {
        var keyRandomRotation = Quaternion.Euler(new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360)));
        _key = CreateObjectInDropZone(_keyPrefab, _keyDropArea, keyRandomRotation);

    }

    private void CreateBattery()
    {
        var randomDoorIndex = Random.Range(0, 1000) % _lockerDoors.Length;
        _doorWithBattery = _lockerDoors[randomDoorIndex];
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
            var batteryRotation = Quaternion.Euler(new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360)));
            _battery = CreateObjectInDropZone(_batteryPrefab, dropZone, batteryRotation);
        }

    }

    private GameObject CreateObjectInDropZone(GameObject prefab, GameObject dropZone, Quaternion rotation)
    {
        var keyColliders = dropZone.GetComponents<Collider>();
        var randomColliderIndex = Random.Range(0, 1000) % keyColliders.Length;
        var keyCenter = keyColliders[randomColliderIndex].bounds.center;
        var keyExtends = keyColliders[randomColliderIndex].bounds.extents;
        var randomPosInArea = new Vector3(Random.Range(keyCenter.x, keyCenter.x + keyExtends.x), Random.Range(keyCenter.y, keyCenter.y + keyExtends.y), Random.Range(keyCenter.z, keyCenter.z + keyExtends.z));
        var keyRandomRotation = Quaternion.Euler(new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360)));
        return GameObject.Instantiate(prefab, randomPosInArea, rotation);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
