using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentCollision : MonoBehaviour {

    [HideInInspector]
    public UnityEvents.UnityGameObjectCollider OnChildTriggerEnter;
    [HideInInspector]
    public UnityEvents.UnityGameObjectCollider OnChildTriggerStay;
    [HideInInspector]
    public UnityEvents.UnityGameObjectCollider OnChildTriggerExit;
    [HideInInspector]
    public UnityEvents.UnityGameObjectCollision OnChildCollisionEnter;
    [HideInInspector]
    public UnityEvents.UnityGameObjectCollision OnChildCollisionStay;
    [HideInInspector]
    public UnityEvents.UnityGameObjectCollision OnChildCollisionExit;

    private void Start()
    {
        if (OnChildCollisionEnter == null) OnChildCollisionEnter = new UnityEvents.UnityGameObjectCollision();
        if (OnChildCollisionStay== null) OnChildCollisionStay= new UnityEvents.UnityGameObjectCollision();
        if (OnChildCollisionExit == null) OnChildCollisionExit = new UnityEvents.UnityGameObjectCollision();
        if (OnChildTriggerEnter == null) OnChildTriggerEnter = new UnityEvents.UnityGameObjectCollider();
        if (OnChildTriggerStay == null) OnChildTriggerStay = new UnityEvents.UnityGameObjectCollider();
        if (OnChildTriggerExit == null) OnChildTriggerExit = new UnityEvents.UnityGameObjectCollider();

    }

}
