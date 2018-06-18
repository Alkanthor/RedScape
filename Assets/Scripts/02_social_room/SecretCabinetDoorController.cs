using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecretCabinetDoorController : MonoBehaviour {

    public GameObject TriggerLeft;
    public GameObject TriggerRight;

    private ParentCollision _parentCollision;

    [HideInInspector]
    public UnityEvents.UnityEventGameObjectBool OnIsDoorOnCorrectSide;
    //right - 0, left - 1
    private int _code;
	// Use this for initialization
	void Start () {
        if(OnIsDoorOnCorrectSide == null) OnIsDoorOnCorrectSide = new UnityEvents.UnityEventGameObjectBool();
        _parentCollision = this.GetComponent<ParentCollision>();
        _parentCollision.OnChildTriggerEnter.AddListener(OnChildTriggerEnter);
        _parentCollision.OnChildTriggerExit.AddListener(OnChildTriggerExit);
    }

    public void SetCode(int code, string doorKey)
    {
        _code = code;
        this.GetComponentInChildren<Text>().text = doorKey;
    }

    private void OnChildTriggerExit(GameObject child, Collider collider)
    {
        Debug.Log("Door exited trigger");
        OnIsDoorOnCorrectSide.Invoke(this.gameObject, false);
    }

    private void OnChildTriggerEnter(GameObject child, Collider collider)
    {
    
        Debug.Log("Door " + child.name + " entered trigger " + collider.gameObject.transform.parent.name + " and has code " + _code);
        if ((child.gameObject == TriggerLeft.gameObject && _code % 2 == 1) || (child.gameObject == TriggerRight.gameObject && _code % 2 == 0))
        {
            Debug.Log("Is good combo");
            OnIsDoorOnCorrectSide.Invoke(this.gameObject, true);
        }
        else
        {
            Debug.Log("Is not good combo");
            Debug.Log("Door " + child.name + " entered trigger " + collider.name + " and has code " + _code);
            OnIsDoorOnCorrectSide.Invoke(this.gameObject, false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
