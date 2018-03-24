using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollisionManager : MonoBehaviour {

    [SerializeField]
    private ParentCollisionManager _parentCollisionManager;

    private void OnTriggerEnter(Collider other)
    {
        _parentCollisionManager.OnChildTriggerEnterEvent.Invoke(this.gameObject, other);
    }

    private void OnTriggerStay(Collider other)
    {
        _parentCollisionManager.OnChildTriggerStayEvent.Invoke(this.gameObject, other);
    }

    private void OnTriggerExit(Collider other)
    {
        _parentCollisionManager.OnChildTriggerExitEvent.Invoke(this.gameObject, other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _parentCollisionManager.OnChildCollisionEnterEvent.Invoke(this.gameObject, collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        _parentCollisionManager.OnChildCollisionStayEvent.Invoke(this.gameObject, collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        _parentCollisionManager.OnChildCollisionExitEvent.Invoke(this.gameObject, collision);
    }


}
