using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class CarPuzzle : MonoBehaviour {

   
    public GameObject CarWithoutWheels;
    public GameObject CarComplete;
    public GameObject IdCard;
    private bool _isCardGrabbed;
    private GameObject _carClone;
	// Use this for initialization
	void Start () {


        CarWithoutWheels.GetComponent<CarAssemblyPuzzle>().CarCompleted.AddListener(CarCompleted);
	}

    private void IsCardGrabbed(bool value, Transform grabCardPoint)
    {
        if(value && !_isCardGrabbed)
        {
            var snap = _carClone.GetComponentInChildren<VRTK_SnapDropZone>();
            snap.ForceSnap(IdCard);
        }

    }
    private void CarCompleted(bool value)
    {
        var transform = CarWithoutWheels.transform;
        Destroy(CarWithoutWheels);
        _carClone  = Instantiate(CarComplete, transform.position, transform.rotation);
        _carClone.GetComponent<CarController>().SetIdCard(IdCard);
        _carClone.GetComponent<CarController>().IsCardGrabbed.AddListener(IsCardGrabbed);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
