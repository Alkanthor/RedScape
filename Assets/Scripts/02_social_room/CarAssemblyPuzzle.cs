using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class CarAssemblyPuzzle : MonoBehaviour {


    public UnityEvents.UnityEventBool CarCompleted;
    public VRTK_SnapDropZone[] CarWheelDropZones;
    private Dictionary<GameObject, bool> _carWheelsSnapped;

    private void Awake()
    {
        if (CarCompleted == null) CarCompleted = new UnityEvents.UnityEventBool();
    }
    // Use this for initialization
    void Start () {
        _carWheelsSnapped = new Dictionary<GameObject, bool>();
        for(int i = 0; i < CarWheelDropZones.Length; ++i)
        {
            var snapDropZone = CarWheelDropZones[i];
            snapDropZone.ObjectSnappedToDropZone += OnSnappedDropZone;
            snapDropZone.ObjectUnsnappedFromDropZone += OnUnsnappedDropZone;
        }
        
    }

    private void OnSnappedDropZone(object sender, SnapDropZoneEventArgs e)
    {  
        SetAndCheckIsCarCompleted(e.snappedObject, true);
    }

    private void OnUnsnappedDropZone(object sender, SnapDropZoneEventArgs e)
    {
        SetAndCheckIsCarCompleted(e.snappedObject, false);
    }
    private void SetAndCheckIsCarCompleted(GameObject snappedObject, bool value)
    {
        if(_carWheelsSnapped.ContainsKey(snappedObject))
        {
            _carWheelsSnapped[snappedObject] = value;
        }
        else
        {
            _carWheelsSnapped.Add(snappedObject, value);
        }
        CheckIsCarCompleted();
    }
    private void CheckIsCarCompleted()
    {
        if(!_carWheelsSnapped.ContainsValue(false) && _carWheelsSnapped.Count == CarWheelDropZones.Length)
        {

            CarCompleted.Invoke(true);
            for (int i = 0; i < CarWheelDropZones.Length; ++i)
            {
                var snapDropZone = CarWheelDropZones[i];
                snapDropZone.ObjectSnappedToDropZone -= OnSnappedDropZone;
                snapDropZone.ObjectUnsnappedFromDropZone -= OnUnsnappedDropZone;
            }
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
