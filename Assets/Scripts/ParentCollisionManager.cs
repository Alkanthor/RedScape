using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TriggerEvent : UnityEvent<GameObject, Collider>
{

}

public class CollisionEvent : UnityEvent<GameObject, Collision>
{

}
public class ParentCollisionManager : MonoBehaviour {

    public UnityEvent<GameObject, Collider> OnChildTriggerEnterEvent;
    public UnityEvent<GameObject, Collider> OnChildTriggerStayEvent;
    public UnityEvent<GameObject, Collider> OnChildTriggerExitEvent;

    public UnityEvent<GameObject, Collision> OnChildCollisionEnterEvent;
    public UnityEvent<GameObject, Collision> OnChildCollisionStayEvent;
    public UnityEvent<GameObject, Collision> OnChildCollisionExitEvent;

    private void Awake()
    {
        if (OnChildTriggerEnterEvent == null) OnChildTriggerEnterEvent = new TriggerEvent();
        if (OnChildTriggerStayEvent == null) OnChildTriggerStayEvent = new TriggerEvent();
        if (OnChildTriggerExitEvent == null) OnChildTriggerExitEvent = new TriggerEvent();

        if (OnChildCollisionEnterEvent == null) OnChildCollisionEnterEvent = new CollisionEvent();
        if (OnChildCollisionStayEvent == null) OnChildCollisionStayEvent = new CollisionEvent();
        if (OnChildCollisionExitEvent == null) OnChildCollisionExitEvent = new CollisionEvent();


    }

}
